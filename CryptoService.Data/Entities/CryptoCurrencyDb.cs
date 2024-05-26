namespace CryptoService.Data.Entities;

public class CryptoCurrencyDb
{
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;

    public List<PriceInfoDb>? Prices { get; set; } = null;
}