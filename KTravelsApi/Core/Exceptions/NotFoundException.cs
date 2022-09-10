namespace KTravelsApi.Core.Exceptions;

[Serializable]
public class NotFoundException : ServerException
{
    public NotFoundException(string message) : base(message) { }
}