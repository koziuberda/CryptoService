using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Responses;

namespace CryptoService.Integrations.CoinApi.Services.Interfaces;

public interface ICoinApiService
{
    Task<CoinApiAssetsResponse> GetAssets(string[] assetIds);

    Task<CoinApiSymbolsResponse> GetSymbols(string[] exchangeIds);

    void SubscribeToPriceUpdates(string[] symbolIds);
    
    Action<CoinApiPriceUpdate>? PriceChangeCallback { get; set; }
    
    Action<Exception>? ErrorCallback { get; set; }
}