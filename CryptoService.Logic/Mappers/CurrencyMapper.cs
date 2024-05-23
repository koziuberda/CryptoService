using CryptoService.Core.Domain.Models;
using CryptoService.Data.Entities;
using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Logic.Mappers;

public static class CurrencyMapper
{
    public static CryptoCurrencyDb Map(CryptoCurrency from) => new () 
        {
            Id = Guid.NewGuid(),
            Name = from.Name,
            Ticker = from.Ticker
        };
    

    public static CryptoCurrencyDb[] Map(CryptoCurrency[] from) => from.Select(Map).ToArray();

    public static CryptoCurrency Map(CoinApiCurrency from) => new()
    {
        Name = from.Name,
        Ticker = from.AssetId
    };

    public static CryptoCurrency[] Map(CoinApiCurrency[] from) => from.Select(Map).ToArray();
}