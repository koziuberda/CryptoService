namespace CryptoService.API.DTOs;

public record PriceInfoDto(
    string AssetId, 
    string AssetQuoteId, 
    string ExchangeId, 
    string Type, 
    decimal? Price, 
    DateTime LastUpdated);