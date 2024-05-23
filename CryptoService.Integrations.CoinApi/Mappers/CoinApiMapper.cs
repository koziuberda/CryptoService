using CoinAPI.REST.V1;
using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Integrations.CoinApi.Mappers;

public static class CoinApiMapper
{
    public static CoinApiCurrency Map(Asset from) => new()
    {
        AssetId = from.asset_id,
        Name = from.name
    };
}