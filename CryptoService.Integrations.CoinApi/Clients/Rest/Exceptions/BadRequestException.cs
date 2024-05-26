namespace CryptoService.Integrations.CoinApi.Clients.Rest.Exceptions;

public class BadRequestException : CoinApiException
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}