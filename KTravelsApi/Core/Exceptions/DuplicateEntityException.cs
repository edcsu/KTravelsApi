namespace KTravelsApi.Core.Exceptions;

/// <summary>
/// Throw this exception when you encounter an existing record matching the one you are attempting to create
/// </summary>
[Serializable]
public class DuplicateEntityException : ServerException
{
    public DuplicateEntityException(string message) : base(message) { }
}