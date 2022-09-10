namespace KTravelsApi.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// ClientFriendlyException
/// </summary>
public class ServerException : Exception
{
    /// <inheritdoc />
    protected ServerException(string message) : base(message)
    {
    }
}

public class ClientFriendlyException : ServerException
{
    public ClientFriendlyException(string message) : base(message) { }
}