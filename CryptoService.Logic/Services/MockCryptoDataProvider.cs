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
            Name = "Bitcoin",
            Ticker = "BTC"
        },
        new CryptoCurrency
        {
            Name = "Ethereum",
            Ticker = "ETH"
        },
        new CryptoCurrency
        {
            Name = "Litecoin",
            Ticker = "LTC"
        }
    };
}