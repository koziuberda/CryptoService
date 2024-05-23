using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Integrations.CoinApi.Responses;

public class CoinApiCurrenciesResponse
{
    public CoinApiCurrenciesResponse(CoinApiCurrency[] currencies)
    {
        Currencies = currencies;
    }

    public CoinApiCurrency[] Currencies { get; set; }
}