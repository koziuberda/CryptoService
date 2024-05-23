using CryptoService.Core.Domain.Models;
using CryptoService.Integrations.CoinApi.Services.Interfaces;
using CryptoService.Logic.Mappers;
using CryptoService.Logic.Services.Interfaces;

namespace CryptoService.Logic.Services;

public class CryptoDataProvider : ICryptoDataProvider
{
    private readonly ICoinApiService _coinApiService;
    
    public CryptoDataProvider(ICoinApiService coinApiService)
    {
        _coinApiService = coinApiService;
    }

    public async Task<CryptoCurrency[]> GetSupportedCurrenciesAsync()
    {
        var response = await _coinApiService.GetSupportedCryptocurrencies();
        return CurrencyMapper.Map(response.Currencies);
    }
}