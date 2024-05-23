using CoinAPI.REST.V1;
using CoinAPI.WebSocket.V1;
using CryptoService.Integrations.CoinApi.Mappers;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Responses;
using CryptoService.Integrations.CoinApi.Services.Interfaces;

namespace CryptoService.Integrations.CoinApi.Services;

public class CoinApiService : ICoinApiService
{
    private readonly CoinApiRestClient _restClient;
    private readonly CoinApiWsClient _webSocketClient;
    
    public CoinApiService(string apiKey)
    {
        _restClient = new CoinApiRestClient(apiKey);
        _webSocketClient = new CoinApiWsClient(apiKey);
    }
    
    public async Task<CoinApiCurrenciesResponse> GetSupportedCryptocurrencies()
    {
        List<Asset> assets = await _restClient.Metadata_list_assetsAsync();
        var currencies = assets
            .Where(a => a.type_is_crypto)
            .Select(CoinApiMapper.Map)
            .ToArray();
        
        return new CoinApiCurrenciesResponse(currencies);
    }
}