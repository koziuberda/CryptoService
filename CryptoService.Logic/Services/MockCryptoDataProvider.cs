using CryptoService.Core.Domain.Models;
using CryptoService.Logic.Services.Interfaces;

namespace CryptoService.Logic.Services;

public class MockCryptoDataProvider : ICryptoDataProvider
{
    public Task<CryptoCurrency[]> GetSupportedCurrenciesAsync()
    {
        return Task.FromResult(GetTestData());
    }

    private CryptoCurrency[] GetTestData() => new[]
    {
        new CryptoCurrency
        {
            Id = Guid.NewGuid(),
            Name = "Bitcoin",
            Ticker = "BTC"
        },
        new CryptoCurrency
        {
            Id = Guid.NewGuid(),
            Name = "Ethereum",
            Ticker = "ETH"
        },
        new CryptoCurrency
        {
            Id = Guid.NewGuid(),
            Name = "Litecoin",
            Ticker = "LTC"
        }
    };
}