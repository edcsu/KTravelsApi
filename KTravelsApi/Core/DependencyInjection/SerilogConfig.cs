using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace KTravelsApi.Core.DependencyInjection;

public static class SerilogConfig
{
    /// <summary>
    /// Add Serilog config with enriches
    /// </summary>
    /// <param name="builder"></param>
    public static void AddSerilogConfig(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((ctx, services, lc) =>
        {
            lc
                .ReadFrom.Configuration(ctx.Configuration)
                .ReadFrom.Services(services)
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .Enrich.WithThreadName()
                .Enrich.WithClientAgent()
                .Enrich.WithClientIp()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithExceptionDetails(
                    new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers());
        });
    }
}