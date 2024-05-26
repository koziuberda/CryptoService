namespace CryptoService.Core.Domain.Models;

public class CryptoCurrency
{
    public string Ticker { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;

    public List<PriceInfo>? Prices { get; set; } = null;
}