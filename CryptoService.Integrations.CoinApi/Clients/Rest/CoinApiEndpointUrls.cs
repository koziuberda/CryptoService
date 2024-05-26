namespace CryptoService.Integrations.CoinApi.Clients.Rest;

public static class CoinApiEndpointUrls
{
    public static string Assets(string[] assetIds) => $"/v1/assets/{string.Join(",", assetIds)}";
    
    public static string Symbols(string[] exchangeIds)
    {
        string joinedIds = string.Join(",", exchangeIds);
        return $"/v1/symbols/?filter_exchange_id={joinedIds}";
    }
}