using CryptoService.API.DTOs;
using CryptoService.API.Responses;
using CryptoService.Core.Domain.Models;

namespace CryptoService.API.Mappers;

public static class ApiCryptoMapper
{
    public static CryptoCurrencyDto Map(CryptoCurrency old) => new(old.Ticker, old.Name);

    public static PriceInfoDto Map(PriceInfo old) => new PriceInfoDto(old.AssetId, old.AssetQuoteId, old.ExchangeId,
        old.Type, old.Price, old.LastUpdated);

    public static CryptoCurrencyWithPricesDto MapWithPrices(CryptoCurrency old) =>
        new(old.Ticker, old.Name, old.Prices?.Select(Map).ToList());
}