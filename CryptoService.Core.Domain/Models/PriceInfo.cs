namespace CryptoService.Core.Domain.Models;

public class PriceInfo
{
    public Guid Id { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime LastUpdated { get; set; }
    
    public Guid CurrencyId { get; set; }
}