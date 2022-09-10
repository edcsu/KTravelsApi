namespace KTravelsApi.Core.Extensions;

public static class HealthChecksExtensions
{
    public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration config)
    {
        //adding health check services to container
        services.AddHealthChecks();

        //adding healthchecks UI
        services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(60); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
                opt.DisableDatabaseMigrations(); // disable migrations so that we can use the AddHealthChecksUI

                opt.AddHealthCheckEndpoint("KTravels API", "/health"); //map health check api
                opt.SetHeaderText("KTravels Health Check UI"); // set UI header text
            })
            .AddInMemoryStorage();
    }
}