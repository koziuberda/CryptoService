using CryptoService.API.DTOs;

namespace CryptoService.API.Responses;

public record CurrenciesWithPricesResponse(CryptoCurrencyWithPricesDto[] Currencies);