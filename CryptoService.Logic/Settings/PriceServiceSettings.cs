namespace CryptoService.Logic.Settings;

public class PriceServiceSettings
{
    public const string SectionName = "PriceServiceSettings";
    
    public string[] ExchangeIds { get; set; }
    public string[] AssetQuoteIds { get; set; }
    public string[] Types { get; set; }
    public bool EnableUpdates { get; set; }
}