using CryptoService.Integrations.CoinApi.Responses;

namespace CryptoService.Integrations.CoinApi.Services.Interfaces;

public interface ICoinApiService
{
    Task<CoinApiCurrenciesResponse> GetSupportedCryptocurrencies();

    void SubscribeToPriceUpdates(string[] assetIds);
    
    Action<PriceUpdate>? PriceChangeCallback { get; set; }
    
    Action<Exception>? ErrorCallback { get; set; }
}