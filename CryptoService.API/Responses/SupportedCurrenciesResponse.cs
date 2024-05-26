using CryptoService.API.DTOs;

namespace CryptoService.API.Responses;

public record SupportedCurrenciesResponse(CryptoCurrencyDto[] Currencies);