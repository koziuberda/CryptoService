namespace CryptoService.Data.Entities;

public class CryptoCurrencyDb
{
    public Guid Id { get; set; }

    public string Ticker { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;

    public PriceInfoDb? PriceInfo { get; set; } = null;
}