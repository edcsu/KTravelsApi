using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace KTravelsApi.Core.DependencyInjection;

public static class ApiVersioningConfig
{
    /// <summary>
    /// Custom Api versioning
    /// </summary>
    /// <param name="builder"></param>
    public static void AddApiVersioningConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader(new[] { "api-version", "v" }),
                new HeaderApiVersionReader("X-Version")
            );
        });

        builder.Services.AddVersionedApiExplorer(
            options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

            });

    }
}