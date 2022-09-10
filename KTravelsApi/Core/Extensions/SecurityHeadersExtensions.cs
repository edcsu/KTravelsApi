namespace KTravelsApi.Core.Extensions;

public static class SecurityHeadersExtensions
{
    /// <summary>
    /// Add security headers to ASP.NET Core endpoints
    /// </summary>
    /// <param name="builder">IApplication Builder extension</param>    
    /// <returns></returns>
    public static IApplicationBuilder UseCustomSecurityHeaders(this IApplicationBuilder builder)
    {
        var policyCollection = new HeaderPolicyCollection()
            .AddFrameOptionsDeny()
            .AddXssProtectionBlock()
            .AddContentTypeOptionsNoSniff()
            .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAgeInSeconds: 60 * 60 * 24 * 365) // maxage = one year in seconds
            .AddReferrerPolicyStrictOriginWhenCrossOrigin()
            .RemoveServerHeader()
            .AddContentSecurityPolicy(cspBuilder =>
            {
                cspBuilder.AddObjectSrc().None();
                cspBuilder.AddFormAction().Self();
                cspBuilder.AddFrameAncestors().None();
            })
            .AddCrossOriginOpenerPolicy(policyBuilder =>
            {
                policyBuilder.SameOrigin();
            })
            .AddCrossOriginEmbedderPolicy(policyBuilder =>
            {
                policyBuilder.RequireCorp();
            })
            .AddCrossOriginResourcePolicy(policyBuilder =>
            {
                policyBuilder.SameOrigin();
            });

        return builder.UseSecurityHeaders(policyCollection);
    }
}