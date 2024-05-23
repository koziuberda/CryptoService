using CoinAPI.REST.V1;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Responses;

namespace CryptoService.Integrations.CoinApi.Services.Interfaces;

public interface ICoinApiService
{
    Task<CoinApiCurrenciesResponse> GetSupportedCryptocurrencies();
}