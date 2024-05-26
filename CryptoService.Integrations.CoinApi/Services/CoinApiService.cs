using CoinAPI.WebSocket.V1;
using CryptoService.Integrations.CoinApi.Mappers;
using CryptoService.Integrations.CoinApi.Responses;
using CryptoService.Integrations.CoinApi.Services.Interfaces;
using Trade = CoinAPI.WebSocket.V1.DataModels.Trade;

namespace CryptoService.Integrations.CoinApi.Services;

public class CoinApiService : ICoinApiService
{
    public Action<PriceUpdate>? PriceChangeCallback { get; set; }
    public Action<Exception>? ErrorCallback { get; set; }
    
    private readonly string _apiKey;
    private readonly CoinApiRestClient _restClient;
    private readonly CoinApiWsClient _webSocketClient;
    
    public CoinApiService(string apiKey)
    {
        _apiKey = apiKey;
        _restClient = new CoinApiRestClient(apiKey);
        _webSocketClient = new CoinApiWsClient();

        _webSocketClient.Error += OnError;
        _webSocketClient.TradeEvent += OnTradeEvent;
    }
    
    public async Task<CoinApiCurrenciesResponse> GetSupportedCryptocurrencies()
    {
        List<Asset> assets = await _restClient.Metadata_list_assetsAsync();
        var currencies = assets
            .Where(a => a is {type_is_crypto: true, price_usd: > 0})
            .Select(CoinApiMapper.Map)
            .ToArray();
        
        return new CoinApiCurrenciesResponse(currencies);
    }

    public void SubscribeToPriceUpdates(string[] assetIds)
    {
        var subscriptionMsg = GetTickerSubscriptionMessage(assetIds);
        _webSocketClient.SendHelloMessage(subscriptionMsg);
    }
    
    private void OnTradeEvent(object sender, Trade item)
    {
        var response = CoinApiMapper.Map(item);
        
        PriceChangeCallback?.Invoke(response);
    }

    private void OnError(object? sender, Exception ex)
    {
        ErrorCallback?.Invoke(ex);
    }

    private Hello GetTickerSubscriptionMessage(string[] assetIds) 
        => new ()
        {
            type = "subscribe",
            apikey = Guid.Parse(_apiKey),
            heartbeat = false,
            subscribe_data_type = new[] { "trade" },
            subscribe_filter_asset_id = assetIds,
            subscribe_filter_exchange_id = new []{ "BYBIT", "BINANCE", "COINBASE" }
        };
}