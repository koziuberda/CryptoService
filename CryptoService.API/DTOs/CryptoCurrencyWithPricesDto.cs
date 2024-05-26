namespace CryptoService.API.DTOs;

public record CryptoCurrencyWithPricesDto(string Ticker, string Name, List<PriceInfoDto>? Prices);