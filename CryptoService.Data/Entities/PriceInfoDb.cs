namespace CryptoService.Data.Entities;

public class PriceInfoDb
{
    public Guid Id { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime LastUpdated { get; set; }
    
    public Guid CurrencyId { get; set; }
    
    public CryptoCurrencyDb CryptoCurrency { get; set; } = null!;
}