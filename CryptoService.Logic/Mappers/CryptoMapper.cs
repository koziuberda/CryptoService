using CryptoService.Core.Domain.Models;
using CryptoService.Data.Entities;
using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Logic.Mappers;

public static class CryptoMapper
{
    public static PriceInfo Map(SymbolDb old)
    {
        // "{exchange_id}_{SPOT_or_PERP}_{asset_id_base}_{asset_id_quote}"
        var symbolIdParts = old.SymbolId.Split("_");
        var exchangeId = symbolIdParts[0];
        var type = symbolIdParts[1];
        var quote = symbolIdParts[3];
        return new PriceInfo
        {
            AssetId = old.AssetId,
            ExchangeId = exchangeId,
            Type = type,
            AssetQuoteId = quote,
            Price = old.Price,
            LastUpdated = old.LastUpdated
        };
    }
    
    public static CryptoCurrency Map(AssetDb from) => new()
    {
        Ticker = from.Id,
        Name = from.Name,
        Prices = from.Symbols?.Select(Map).ToList()
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