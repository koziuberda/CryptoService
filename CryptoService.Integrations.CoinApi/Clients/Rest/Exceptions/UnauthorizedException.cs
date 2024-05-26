namespace CryptoService.Integrations.CoinApi.Clients.Rest.Exceptions;

public class UnauthorizedException : CoinApiException
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}