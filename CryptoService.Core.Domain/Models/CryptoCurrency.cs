namespace CryptoService.Core.Domain.Models;

public class CryptoCurrency
{
    public Guid Id { get; set; }

    public string Ticker { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
}