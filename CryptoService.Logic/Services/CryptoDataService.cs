using CryptoService.Core.Domain.Models;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using CryptoService.Logic.Mappers;
using CryptoService.Logic.Services.Interfaces;
using CryptoService.Logic.Specifications;
using Microsoft.Extensions.Logging;

namespace CryptoService.Logic.Services;

public class CryptoDataService : ICryptoDataService
{
    private readonly IAssetRepository _assetRepository;
    private readonly ILogger<CryptoDataService> _logger;
    
    public CryptoDataService(
        IAssetRepository assetRepository,
        ILogger<CryptoDataService> logger)
    {
        _assetRepository = assetRepository;
        _logger = logger;
    }

    public async Task<CryptoCurrency[]> GetSupportedCurrenciesAsync()
    {
        var currencies = await _assetRepository.ListAsync();
        return currencies.Select(CryptoMapper.Map).ToArray();
    }

    public async Task<CryptoCurrency[]> GetAssetsWithPriceInfo(string[] assetIds)
    {
        var symbols = await _assetRepository.ListAsync(new AssetsWithSymbolsByIdsSpecification(assetIds));
        return symbols.Select(CryptoMapper.Map).ToArray();
    }
}