using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KTravelsApi.Data;

/// <summary>
/// Initialise database
/// </summary>
public static class Initializer
{
    public static void PopulateDb(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!);
    }

    /// <summary>
    /// Seed data to the database
    /// </summary>
    /// <param name="context"></param>
    private static void SeedData(DbContext context)
    {
        Log.Information("Trying to apply migrations to Database");
        context.Database.Migrate();
    }
}