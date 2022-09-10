namespace KTravelsApi.Core.Config;

public class AuthSettings
{
    public const string SectionName = "Authentication";

    public string Authority { get; init; } = default!;

    public string ApiName { get; init; } = default!;

    public string ClientId { get; init; } = default!;

    public string ClientSecret { get; init; } = default!;

    public string AccessScope { get; init; } = default!;
}