namespace KTravelsApi.Core.Exceptions;

[Serializable]
public class NotAuthorizedException : ServerException
{
    public NotAuthorizedException(string message) : base(message) { }
}