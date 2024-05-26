namespace CryptoService.Integrations.CoinApi.Models;

public class CoinApiSymbol
{
    public string SymbolId { get; set; }  = string.Empty;
    public string ExchangeId { get; set; } = string.Empty;
    public string SymbolType { get; set; } = string.Empty;
    public string AssetIdBase { get; set; } = string.Empty;
    public string AssetIdQuote { get; set; } = string.Empty;
}