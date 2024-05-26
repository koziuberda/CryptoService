using CryptoService.Core.Domain.Models;
using CryptoService.Data.Entities;
using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Logic.Mappers;

public static class CryptoMapper
{
    public static CryptoCurrency Map(AssetDb from) => new()
    {
        Ticker = from.Id,
        Name = from.Name
    };

    public static AssetDb Map(CoinApiAsset from) => new()
    {
        Id = from.AssetId,
        Name = from.Name
    };

    // todo better call new and old instead of from and to
    public static SymbolDb Map(CoinApiPriceUpdate from)
    {
        // "{exchange_id}_{SPOT_or_PERP}_{asset_id_base}_{asset_id_quote}"
        var symbolParts = from.SymbolId.Split("_");
        return new SymbolDb
        {
            SymbolId = from.SymbolId,
            AssetId = symbolParts[2],
            Price = from.Rate,
            LastUpdated = from.Updated
        };
    }

    public static SymbolDb Map(CoinApiSymbol old) => new()
    {
        SymbolId = old.SymbolId,
        AssetId = old.AssetIdBase,
        LastUpdated = old.Timestamp,
        Price = old.Price
    };
}