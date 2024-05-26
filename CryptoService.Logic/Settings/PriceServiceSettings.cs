namespace CryptoService.Logic.Settings;

public class PriceServiceSettings
{
    public string[] ExchangeIds = {"BINANCE" , "BYBIT", "COINBASE"};
    public string[] AssetQuoteIds = {"USD", "USDT"};
    public string[] Types = {"SPOT", "PERPETUAL"};
    public bool EnableUpdates = false;
}