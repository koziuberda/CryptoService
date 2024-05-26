namespace CryptoService.Integrations.CoinApi.Clients.Rest.Exceptions;

public class TooManyRequestsException : CoinApiException
{
    public TooManyRequestsException()
    {
    }

    public TooManyRequestsException(string message) : base(message)
    {
    }

    public TooManyRequestsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}