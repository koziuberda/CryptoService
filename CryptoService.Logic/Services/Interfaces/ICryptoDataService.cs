using CryptoService.Core.Domain.Models;

namespace CryptoService.Logic.Services.Interfaces;

public interface ICryptoDataService
{
    Task<CryptoCurrency[]> GetSupportedCurrenciesAsync();
}