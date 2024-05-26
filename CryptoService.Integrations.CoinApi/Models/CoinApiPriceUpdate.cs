namespace CryptoService.Integrations.CoinApi.Models;

public class CoinApiPriceUpdate
{
    // public string AssetIdBase { get; set; } = string.Empty;
    // public string AssetIdQuote { get; set; } = string.Empty;
    // public string ExchangeId { get; set; } = string.Empty;
    // public string Type { get; set; } = string.Empty;
    public string SymbolId { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime Updated { get; set; }
}