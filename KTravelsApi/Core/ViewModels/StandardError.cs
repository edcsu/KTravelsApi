namespace KTravelsApi.Core.ViewModels;

public class StandardError
{
    public string Type { get; set; } = default!;

    public string Title { get; set; } = default!;

    public int Status { get; set; } = default!;

    public string TraceId { get; set; } = default!;

    public Errors Errors { get; set; } = default!;
}

public class Errors
{
    public List<string> Property { get; set; } = default!;
}