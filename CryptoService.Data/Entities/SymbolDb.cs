namespace CryptoService.Data.Entities;

public class SymbolDb
{
    public string SymbolId { get; set; } = string.Empty;
    
    public decimal? Price { get; set; }
    
    public DateTime LastUpdated { get; set; }

    public string AssetId { get; set; } = string.Empty;
    
    public AssetDb Asset { get; set; } = null!;
}