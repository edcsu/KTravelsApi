namespace KTravelsApi.Core.ViewModels;

public class BasicError
{
    /// <summary>
    /// Error object
    /// </summary>
    public Error Error { get; set; } = default!;
}

public record Error
{
    /// <summary>
    /// Error code
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; set; } = default!;
}