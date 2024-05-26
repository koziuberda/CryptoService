using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Integrations.CoinApi.Responses;

public class CoinApiSymbolsResponse
{
    public CoinApiSymbolsResponse(CoinApiSymbol[] symbols)
    {
        Symbols = symbols;
    }

    public CoinApiSymbol[] Symbols { get; set; }
}