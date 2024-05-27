using CoinAPI.WebSocket.V1;
using CoinAPI.WebSocket.V1.DataModels;
using CryptoService.Integrations.CoinApi.Clients.Rest;
using CryptoService.Integrations.CoinApi.Clients.Rest.DataModels;
using CryptoService.Integrations.CoinApi.Mappers;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Responses;
using CryptoService.Integrations.CoinApi.Services.Interfaces;

namespace CryptoService.Integrations.CoinApi.Services;

public class CoinApiService : ICoinApiService
{
    public Action<CoinApiPriceUpdate>? PriceChangeCallback { get; set; }
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
        _webSocketClient.OHLCVEvent += OnOHLCVEvent;
    }

    public async Task<CoinApiAssetsResponse> GetAssets(string[] assetIds)
    {
        List<Asset> assets = await _restClient.Metadata_list_assetsAsync(assetIds);
        var assetDtos = assets.Select(CoinApiMapper.Map).ToArray();

        return new CoinApiAssetsResponse(assetDtos);
    }

    public async Task<CoinApiSymbolsResponse> GetSymbols(string[] exchangeIds)
    {
        List<Symbol> symbols = await _restClient.Metadata_list_symbols_exchangesAsync(exchangeIds);
        var symbolDtos = symbols.Select(CoinApiMapper.Map).ToArray();
        return new CoinApiSymbolsResponse(symbolDtos);
    }

    public void SubscribeToPriceUpdates(string[] symbolIds)
    {
        var subscriptionMsg = GetOHLCVSubscriptionMessage(symbolIds);
        _webSocketClient.SendHelloMessage(subscriptionMsg);
    }

    private void OnError(object? sender, Exception ex)
    {
        ErrorCallback?.Invoke(ex);
    }

    private string[] GetExactSymbolIds(string[] symbolIds)
    {
        return symbolIds.Select(x => string.Concat(x, "$")).ToArray();
    }

    private void OnOHLCVEvent(object sender, OHLCV item)
    {
        var response = CoinApiMapper.Map(item);

        PriceChangeCallback?.Invoke(response);
    }

    private Hello GetOHLCVSubscriptionMessage(string[] symbolIds) =>
        new()
        {
            type = "subscribe",
            apikey = Guid.Parse(_apiKey),
            heartbeat = false,
            subscribe_data_type = new[] {"ohlcv"},
            subscribe_filter_symbol_id = GetExactSymbolIds(symbolIds),
            subscribe_filter_period_id = new[] {"5SEC", "1MIN"}
        };
}