using System.Net;
using CryptoService.Integrations.CoinApi.Clients.Rest.DataModels;
using CryptoService.Integrations.CoinApi.Clients.Rest.Exceptions;
using Newtonsoft.Json;

namespace CryptoService.Integrations.CoinApi.Clients.Rest;

public class CoinApiRestClient
{
    private string apikey;
    public string DateFormat => "yyyy-MM-ddTHH:mm:ss.fff";
    private string WebUrl = "https://rest.coinapi.io";

    public CoinApiRestClient(string apikey)
    {
        this.apikey = apikey;
        this.WebUrl = WebUrl.TrimEnd('/');
    }

    public CoinApiRestClient(string apikey, string url)
    {
        this.apikey = apikey;
        this.WebUrl = url.TrimEnd('/');
    }

    private async Task<T> GetData<T>(string url)
    {
        try
        {
            using (var handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var client = new HttpClient(handler, false))
                {
                    client.DefaultRequestHeaders.Add("X-CoinAPI-Key", apikey);

                    HttpResponseMessage response = await client.GetAsync(WebUrl + url).ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                        await RaiseError(response).ConfigureAwait(false);

                    return await Deserialize<T>(response).ConfigureAwait(false);
                }
            }
        }
        catch (CoinApiException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new CoinApiException("Unexpected error", e);
        }
    }

    private static async Task RaiseError(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var message = (await DeserializeFromString<ErrorMessage>(responseString).ConfigureAwait(false))?.message ??
                      responseString;

        switch ((int) response.StatusCode)
        {
            case 400:
                throw new BadRequestException(message);
            case 401:
                throw new UnauthorizedException(message);
            case 403:
                throw new ForbiddenException(message);
            case 429:
                throw new TooManyRequestsException(message);
            case 550:
                throw new NoDataException(message);
            default:
                throw new CoinApiException(message);
        }
    }

    private static async Task<T> DeserializeFromString<T>(string responseString)
    {
        var data = JsonConvert.DeserializeObject<T>(responseString);
        return data;
    }

    private static async Task<T> Deserialize<T>(HttpResponseMessage responseMessage)
    {
        var responseString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<T>(responseString);
        return data;
    }

    public Task<List<Asset>> Metadata_list_assetsAsync()
    {
        return GetData<List<Asset>>(CoinApiEndpointUrls.Assets());
    }

    public Task<List<Symbol>> Metadata_list_symbolsAsync()
    {
        return GetData<List<Symbol>>(CoinApiEndpointUrls.Symbols());
    }

    public Task<List<Symbol>> Metadata_list_symbols_exchangeAsync(string exchangeId)
    {
        return GetData<List<Symbol>>(CoinApiEndpointUrls.Symbols(exchangeId));
    }
    
    public Task<List<Symbol>> Metadata_list_symbols_exchangesAsync(string[] exchangeIds)
    {
        return GetData<List<Symbol>>(CoinApiEndpointUrls.Symbols(exchangeIds));
    }
}