namespace CryptoService.Integrations.CoinApi.Clients.Rest;

public static class CoinApiEndpointUrls
{
    public static string Assets() => "/v1/assets";
    public static string Symbols() => "/v1/symbols";
    public static string Symbols(string exchangeId) => $"/v1/symbols/{exchangeId}";
    
    public static string Symbols(string[] exchangeIds)
    {
        string joinedIds = string.Join(",", exchangeIds);
        return $"/v1/symbols/{joinedIds}";
    }
}