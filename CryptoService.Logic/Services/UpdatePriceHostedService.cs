using System.Collections.Concurrent;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Services.Interfaces;
using CryptoService.Logic.Mappers;
using CryptoService.Logic.Specifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoService.Logic.Services;

public class UpdatePriceHostedService : BackgroundService
{
    private static readonly string[] ExchangeIds = {"BINANCE" , "BYBIT", "COINBASE"};
    private static readonly string[] AssetQuoteIds = {"USD", "USDT"};

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ICoinApiService _coinApiService;
    private readonly ILogger<UpdatePriceHostedService> _logger;
    
    private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(3);
    private readonly ConcurrentDictionary<string, CoinApiPriceUpdate> _priceUpdatesDict = new();
    private Timer? _timer;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    
    public UpdatePriceHostedService(
        ICoinApiService coinApiService, 
        ILogger<UpdatePriceHostedService> logger, 
        IServiceScopeFactory scopeFactory)
    {
        _coinApiService = coinApiService;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{serviceName} has started.", nameof(UpdatePriceHostedService));
        var symbolIds = await InitializeDb(stoppingToken);
        await SubscribeToPriceUpdates(symbolIds.ToArray());

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
    
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(stoppingToken);
    }
    
    private async Task<List<string>> InitializeDb(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DB initialization started");
        using var scope = _scopeFactory.CreateScope();
        var assetRepository = scope.ServiceProvider.GetRequiredService<IAssetRepository>();
        var symbolsRepository = scope.ServiceProvider.GetRequiredService<ISymbolRepository>();
        
        var symbolIds = await symbolsRepository.ListAsync(new SymbolIdsSpecification(), stoppingToken);
        if (!symbolIds.Any())
        {
            _logger.LogInformation("Filling DB with supported currencies");
            
            var symbols = await GetSymbols(ExchangeIds);
            symbolIds = symbols.Select(x => x.SymbolId).ToList();
            var assetIds = symbols.Select(x => x.AssetId).ToArray();
            var assets = await GetAssets(assetIds);

            // todo db transaction
            await assetRepository.AddRangeAsync(assets, stoppingToken);
            await symbolsRepository.AddRangeAsync(symbols, stoppingToken);
            
            _logger.LogInformation("Db has been initialized");
        }
        else
        {
            _logger.LogInformation("There are already currencies in the DB. Initialization isn't required.");
        }

        return symbolIds;
    }
    
    private async Task<List<SymbolDb>> GetSymbols(string[] exchangeIds)
    {
        var symbolsResponse = await _coinApiService.GetSymbols(exchangeIds);
        var symbols = symbolsResponse.Symbols
            .Where(x => AssetQuoteIds.Contains(x.AssetIdQuote) && x.Price > 0)
            .Select(CryptoMapper.Map)
            .ToList();
        return symbols;
    }
    
    private async Task<List<AssetDb>> GetAssets(string[] assetIds)
    {
        var assetsResponse = await _coinApiService.GetAssets(assetIds);
        var assets = assetsResponse.Assets.Select(CryptoMapper.Map).ToList();
        return assets;
    }

    private async Task SubscribeToPriceUpdates(string[] symbolIds)
    {
        _coinApiService.PriceChangeCallback = priceUpdate =>
        {
            // only the newest items
            // no need to store and process outdated information
            _priceUpdatesDict.AddOrUpdate(priceUpdate.SymbolId, priceUpdate, (key, oldValue) =>
                oldValue.Updated < priceUpdate.Updated ? priceUpdate : oldValue);
        };
        
        _coinApiService.ErrorCallback = ex =>
        {
            _logger.LogError(ex, "Error occurred while receiving price updates.");
        };
        
        _coinApiService.SubscribeToPriceUpdates(symbolIds);
        
        _timer = new Timer(ProcessQueueWrapper, null, TimeSpan.Zero, _updateInterval);
    }
    
    private void ProcessQueueWrapper(object? state)
    {
        _ = ProcessQueue(); // don't panic, it's done intentionally
    }
    
    private async Task ProcessQueue()
    {
        if (!await _semaphore.WaitAsync(0))
        {
            _logger.LogInformation("Previous queue processing is still running.");
            return; // If another execution is still running, skip this one
        }

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var symbolRepository = scope.ServiceProvider.GetRequiredService<ISymbolRepository>();
        
            var updates = _priceUpdatesDict.Values.ToList();
            _priceUpdatesDict.Clear();

            var symbolDbs = updates.Select(CryptoMapper.Map).ToArray();
            await symbolRepository.UpdateRangeAsync(symbolDbs);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the queue.");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}