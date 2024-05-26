namespace CryptoService.Integrations.CoinApi.Responses;

public class PriceUpdate
{
    // public string AssetIdBase { get; set; } = string.Empty;
    // public string AssetIdQuote { get; set; } = string.Empty;
    // public string ExchangeId { get; set; } = string.Empty;
    // public string Type { get; set; } = string.Empty;
    public string SymbolId { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime Updated { get; set; }
}