using CryptoService.Integrations.CoinApi.Clients.Rest.DataModels;
using CryptoService.Integrations.CoinApi.Models;
using CryptoService.Integrations.CoinApi.Responses;
using Trade = CoinAPI.WebSocket.V1.DataModels.Trade;

namespace CryptoService.Integrations.CoinApi.Mappers;

public static class CoinApiMapper
{
    public static CoinApiCurrency Map(Asset from) => new()
    {
        AssetId = from.asset_id,
        Name = from.name
    };
    
    public static PriceUpdate Map(Trade from) => new ()
    {
        SymbolId = from.symbol_id,
        Rate = from.price,
        Updated = from.time_exchange
    };
}