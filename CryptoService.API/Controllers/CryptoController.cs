using CryptoService.API.Mappers;
using CryptoService.API.Requests;
using CryptoService.API.Responses;
using CryptoService.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    private readonly ILogger<CryptoController> _logger;
    private readonly ICryptoDataService _cryptoDataService;

    public CryptoController(
        ILogger<CryptoController> logger, 
        ICryptoDataService cryptoDataService)
    {
        _logger = logger;
        _cryptoDataService = cryptoDataService;
    }

    /// <summary>
    /// Get supported assets
    /// </summary>
    /// <response code="200"></response>
    [HttpGet("get-supported-assets")]
    [ProducesResponseType(typeof(SupportedCurrenciesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSupportedAssets()
    {
        var currencies = await _cryptoDataService.GetSupportedCurrenciesAsync();
        var currenciesDtos = currencies.Select(ApiCryptoMapper.Map).ToArray();
        return Ok(new SupportedCurrenciesResponse(currenciesDtos));
    }
    
    /// <summary>
    /// Get prices
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("get-price-info")]
    [ProducesResponseType(typeof(CurrenciesWithPricesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPricesByIds(GetCurrenciesWithPricesRequest request)
    {
        var currencies = await _cryptoDataService.GetAssetsWithPriceInfo(request.Tickers.ToArray());
        var currenciesDtos = currencies.Select(ApiCryptoMapper.MapWithPrices).ToArray();
        return Ok(new CurrenciesWithPricesResponse(currenciesDtos));
    }
}