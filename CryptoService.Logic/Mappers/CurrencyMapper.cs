using CryptoService.Core.Domain.Models;
using CryptoService.Data.Entities;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Responses;

namespace CryptoService.Logic.Mappers;

public static class CurrencyMapper
{
    public static CryptoCurrency Map(CryptoCurrencyDb from) => new()
    {
        Ticker = from.Id,
        Name = from.Name
    };

    public static CryptoCurrencyDb Map(CoinApiCurrency from) => new()
    {
        Id = from.AssetId,
        Name = from.Name
    };

    public static PriceInfoDb Map(PriceUpdate from)
    {
        // "{exchange_id}_{SPOT_or_PERP}_{asset_id_base}_{asset_id_quote}"
        var symbolParts = from.SymbolId.Split("_");
        return new PriceInfoDb
        {
            SymbolId = from.SymbolId,
            CurrencyId = symbolParts[2],
            Price = from.Rate,
            LastUpdated = from.Updated
        };
    }
}