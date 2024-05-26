using System.Collections.Concurrent;
using System.Text.Json;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Services.Interfaces;
using CryptoService.Logic.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoService.Logic.Services;

public class UpdatePriceHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ICoinApiService _coinApiService;
    private readonly ILogger<UpdatePriceHostedService> _logger;
    
    private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(3);
    private readonly ConcurrentDictionary<string, CoinApiPriceUpdate> _priceUpdatesDict = new();
    private Timer? _timer;
    
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
        var currencies = await InitializeDb(stoppingToken);
        var assetIds = currencies.Select(x => x.Id).ToArray();
        await SubscribeToPriceUpdates(assetIds);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
    
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(stoppingToken);
    }
    
    private async Task<List<AssetDb>> InitializeDb(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DB initialization started");
        using var scope = _scopeFactory.CreateScope();
        var currencyRepository = scope.ServiceProvider.GetRequiredService<ICryptoCurrencyRepository>();
        
        var currencies = await currencyRepository.ListAsync(stoppingToken);
        if (!currencies.Any())
        {
            _logger.LogInformation("Filling DB with supported currencies");
            var response = await _coinApiService.GetSupportedCryptocurrencies();
            currencies = response.Currencies.Select(CurrencyMapper.Map).ToList();
            await currencyRepository.AddRangeAsync(currencies, stoppingToken);
            _logger.LogInformation("Db has been initialized");
        }
        else
        {
            _logger.LogInformation("There are already currencies in the DB. Initialization isn't required.");
        }

        return currencies;
    }

    private async Task SubscribeToPriceUpdates(string[] assetIds)
    {
        _coinApiService.PriceChangeCallback = priceUpdate =>
        {
            _priceUpdatesDict.AddOrUpdate(priceUpdate.SymbolId, priceUpdate, (key, oldValue) =>
                oldValue.Updated < priceUpdate.Updated ? priceUpdate : oldValue);
        };
        
        _coinApiService.ErrorCallback = ex =>
        {
            _logger.LogError(ex, "Error occurred while receiving price updates.");
        };
        
        _coinApiService.SubscribeToPriceUpdates(assetIds);
        
        _timer = new Timer(ProcessQueue, null, TimeSpan.Zero, _updateInterval);
    }
    
    private async void ProcessQueue(object? state)
    {
        using var scope = _scopeFactory.CreateScope();
        var currencyRepository = scope.ServiceProvider.GetRequiredService<ICryptoCurrencyRepository>();
        
        var updates = _priceUpdatesDict.Values.ToList();
        _priceUpdatesDict.Clear();

        // todo better all list at once
        foreach (var priceUpdate in updates)
        {
            await HandlePriceUpdate(currencyRepository, priceUpdate);
        }
    }
    
    private async Task HandlePriceUpdate(
        ICryptoCurrencyRepository currencyRepository, 
        CoinApiPriceUpdate coinApiPriceUpdate)
    {
        var dbEntity = CurrencyMapper.Map(coinApiPriceUpdate);
        var jsonStr = JsonSerializer.Serialize(dbEntity);
        _logger.LogInformation(jsonStr);
        return;
    }
}