namespace CryptoService.Data.Entities;

public class PriceInfoDb
{
    public string SymbolId { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime LastUpdated { get; set; }
    
    public string CurrencyId { get; set; }
    
    public CryptoCurrencyDb Currency { get; set; } = null!;
}