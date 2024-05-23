using CryptoService.Core.Domain.Models;

namespace CryptoService.Logic.Services.Interfaces;

public interface ICryptoDataProvider
{
    Task<CryptoCurrency[]> GetSupportedCurrenciesAsync();
}