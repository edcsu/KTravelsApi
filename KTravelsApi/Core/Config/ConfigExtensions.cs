namespace KTravelsApi.Core.Config;

public static class ConfigExtensions
{
    public static AuthSettings GetAuthServiceSettings(this IConfiguration configuration)
    {
        return configuration.GetSection(AuthSettings.SectionName)
            .Get<AuthSettings>();
    }

    public static SwaggerSettings GetSwaggerSettings(this IConfiguration configuration)
    {
        return configuration.GetSection(SwaggerSettings.SectionName)
            .Get<SwaggerSettings>();
    }

    public static ClientCredentialsSettings GetClientCredentialsSettings(this IConfiguration configuration)
    {
        return configuration.GetSection(ClientCredentialsSettings.SectionName)
            .Get<ClientCredentialsSettings>();
    }

    public static bool ShowHealthCheckUi(this IConfiguration config)
    {
        return config.GetValue<bool>("ShowHealthCheckUI");
    }
}