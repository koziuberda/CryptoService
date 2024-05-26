using CryptoService.Core.Domain.Models;
using CryptoService.Data.Repositories.Interfaces;
using CryptoService.Logic.Mappers;
using CryptoService.Logic.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CryptoService.Logic.Services;

public class CryptoDataProvider : ICryptoDataProvider
{
    private readonly ICryptoCurrencyRepository _currencyRepository;
    private readonly ILogger<CryptoDataProvider> _logger;
    
    public CryptoDataProvider(
        ICryptoCurrencyRepository currencyRepository, 
        ILogger<CryptoDataProvider> logger)
    {
        _currencyRepository = currencyRepository;
        _logger = logger;
    }

    public async Task<CryptoCurrency[]> GetSupportedCurrenciesAsync()
    {
        var currencies = await _currencyRepository.ListAsync();
        return currencies.Select(CurrencyMapper.Map).ToArray();
    }
}