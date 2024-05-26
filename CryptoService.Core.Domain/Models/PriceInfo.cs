namespace CryptoService.Core.Domain.Models;

public class PriceInfo
{
    public string AssetId { get; set; } = string.Empty;
    public string AssetQuoteId { get; set; } = string.Empty;
    
    public string ExchangeId { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
    
    public decimal? Price { get; set; }
    
    public DateTime LastUpdated { get; set; }
}