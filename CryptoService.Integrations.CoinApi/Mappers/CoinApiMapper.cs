﻿using CoinAPI.WebSocket.V1.DataModels;
using CryptoService.Integrations.CoinApi.Clients.Rest.DataModels;
using CryptoService.Integrations.CoinApi.Models;
using Trade = CoinAPI.WebSocket.V1.DataModels.Trade;

namespace CryptoService.Integrations.CoinApi.Mappers;

public static class CoinApiMapper
{
    public static CoinApiAsset Map(Asset from) => new()
    {
        AssetId = from.asset_id,
        Name = from.name
    };
    
    public static CoinApiPriceUpdate Map(OHLCV from) => new ()
    {
        SymbolId = from.symbol_id,
        Rate = from.price_close!.Value,
        Updated = from.time_close!.Value
    };

    public static CoinApiSymbol Map(Symbol from) => new()
    {
        SymbolId = from.symbol_id,
        SymbolType = from.symbol_type,
        ExchangeId = from.exchange_id,
        AssetIdBase = from.asset_id_base,
        AssetIdQuote = from.asset_id_quote,
        Timestamp = DateTime.Now.ToUniversalTime(),
        Price = from.price
    };
}