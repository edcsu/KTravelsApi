using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using KTravelsApi.Core.Config;
using KTravelsApi.Core.DependencyInjection;
using KTravelsApi.Core.Extensions;
using KTravelsApi.Data;
using KTravelsApi.Features.HotelReviews.Repositories;
using KTravelsApi.Features.HotelReviews.Services;
using KTravelsApi.Features.Hotels.Repositories;
using KTravelsApi.Features.Hotels.Services;
using KTravelsApi.Features.RestaurantReviews.Repositories;
using KTravelsApi.Features.RestaurantReviews.Services;
using KTravelsApi.Features.Restaurants.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateBootstrapLogger();

Log.Information("Starting up Env:{Environment}", environment);

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    // Add services to the container.

    builder.AddSerilogConfig();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    
    builder.Services.AddCustomHealthChecks(configuration);

    // enable cache
    builder.Services.AddMemoryCache();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;

            // serialize enums as strings in api responses (e.g. Role)
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services.AddFluentValidationAutoValidation(options =>
    {
        options.DisableDataAnnotationsValidation = true;
    });

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.CustomAddSwaggerGen(configuration);
    builder.AddApiVersioningConfig();

    var auth = configuration.GetAuthServiceSettings();
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = auth.Authority;
            options.Audience = auth.ApiName;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });
    
    builder.Services.AddTransient<IHotelReviewRepository, HotelReviewRepository>();
    builder.Services.AddTransient<IHotelRepository, HotelRepository>();
    builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();
    builder.Services.AddTransient<IRestaurantReviewRepository, RestaurantReviewRepository>();
    
    builder.Services.AddTransient<IHotelService, HotelService>();
    builder.Services.AddTransient<IHotelReviewService, HotelReviewService>();
    builder.Services.AddTransient<IRestaurantReviewService, RestaurantReviewService>();

    var app = builder.Build();

    app.UseCustomSecurityHeaders();

    if (app.Environment.IsDevelopment())
    {
    }

    app.UseCustomErrorHandling();

    app.UseSerilogRequestLogging();

    app.CustomUseSwagger(configuration);

    app.UseHttpsRedirection();

    app.UseRouting();

    //app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseFileServer();

    Initializer.PopulateDb(app);
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        endpoints.MapHealthChecks("/health",
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }
        );

        if (configuration.ShowHealthCheckUi())
        {
            //map healthcheck ui endpoint - default is /healthchecks-ui/
            endpoints.MapHealthChecksUI(setup =>
            {
                setup.AddCustomStylesheet("wwwroot/css/health.css");
            });
        }
    });


    app.Run();
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}