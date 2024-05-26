namespace CryptoService.Data.Entities;

public class AssetDb
{
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;

    public List<SymbolDb>? Symbols { get; set; } = null;
}