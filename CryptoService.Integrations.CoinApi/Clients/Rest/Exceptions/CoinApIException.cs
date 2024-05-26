﻿namespace CryptoService.Integrations.CoinApi.Clients.Rest.Exceptions;

public class CoinApiException : Exception
{
    public CoinApiException()
    {
    }

    public CoinApiException(string message) : base(message)
    {
    }

    public CoinApiException(string message, Exception innerException) : base(message, innerException)
    {
    }
}