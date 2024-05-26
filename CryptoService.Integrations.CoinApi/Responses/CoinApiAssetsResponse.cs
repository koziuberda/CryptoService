using CryptoService.Integrations.CoinApi.Models;

namespace CryptoService.Integrations.CoinApi.Responses;

public class CoinApiAssetsResponse
{
    public CoinApiAssetsResponse(CoinApiAsset[] assets)
    {
        Assets = assets;
    }

    public CoinApiAsset[] Assets { get; set; }
}