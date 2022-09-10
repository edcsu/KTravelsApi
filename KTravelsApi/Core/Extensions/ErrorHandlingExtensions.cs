using KTravelsApi.Core.Middleware;

namespace KTravelsApi.Core.Extensions;

public static class ErrorHandlingExtensions
{
    /// <summary>
    /// Insert error handling middle-ware
    /// </summary>
    /// <param name="builder">IApplication Builder extension</param>    
    /// <returns></returns>
    public static IApplicationBuilder UseCustomErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}