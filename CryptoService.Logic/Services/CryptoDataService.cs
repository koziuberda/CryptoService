using CryptoService.Core.Domain.Models;
using CryptoService.Data.Repositories.Interfaces;
using CryptoService.Logic.Mappers;
using CryptoService.Logic.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CryptoService.Logic.Services;

public class CryptoDataService : ICryptoDataService
{
    private readonly IAssetRepository _currencyRepository;
    private readonly ILogger<CryptoDataService> _logger;
    
    public CryptoDataService(
        IAssetRepository currencyRepository, 
        ILogger<CryptoDataService> logger)
    {
        _currencyRepository = currencyRepository;
        _logger = logger;
    }

    public async Task<CryptoCurrency[]> GetSupportedCurrenciesAsync()
    {
        var currencies = await _currencyRepository.ListAsync();
        return currencies.Select(CryptoMapper.Map).ToArray();
    }
}