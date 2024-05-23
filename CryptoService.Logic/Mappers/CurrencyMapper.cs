using CryptoService.Core.Domain.Models;
using CryptoService.Data.Entities;

namespace CryptoService.Logic.Mappers;

public static class CurrencyMapper
{
    public static CryptoCurrencyDb Map(CryptoCurrency from) => new () 
        {
            Id = from.Id,
            Name = from.Name,
            Ticker = from.Ticker
        };
    

    public static CryptoCurrencyDb[] Map(CryptoCurrency[] from) => from.Select(Map).ToArray();
}