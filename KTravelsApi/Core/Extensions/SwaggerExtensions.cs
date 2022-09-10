﻿using System.Reflection;
using KTravelsApi.Core.Attributes;
using KTravelsApi.Core.Config;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.ReDoc;

namespace KTravelsApi.Core.Extensions;

public static class SwaggerExtensions
    {
        public static void CustomAddSwaggerGen(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerConfig = configuration.GetSwaggerSettings();
            var clientCredentialsOptions = configuration.GetClientCredentialsSettings();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
                {
                    Contact = new OpenApiContact
                    {
                        Name = swaggerConfig.ContactName,
                        Email = swaggerConfig.ContactEmail,
                        Url = new Uri(swaggerConfig.ContactUrl)
                    },
                    Title = swaggerConfig.Title,
                    Version = swaggerConfig.Version,
                    Description = swaggerConfig.Description,
                });

                c.AddSecurityDefinition(clientCredentialsOptions.ClientDefinition, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = clientCredentialsOptions.Description,
                    In = ParameterLocation.Header,
                    Name = clientCredentialsOptions.HeaderName,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(clientCredentialsOptions.TokenUrl),
                        }
                    },
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "client_credentials"
                                }
                            },
                            new List<string>()
                        }
                    });

                c.OperationFilter<CustomHeaderSwaggerAttribute>();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static void CustomUseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerConfig = configuration.GetSwaggerSettings();
            if (!swaggerConfig.Enabled)
            {
                return;
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerConfig.Title);
                c.InjectStylesheet("/swagger-ui/custom.css");
                c.DocumentTitle = swaggerConfig.Title;
                c.RoutePrefix = "docs";
            });
            app.UseReDoc(options =>
            {
                options.DocumentTitle = swaggerConfig.Title;
                options.SpecUrl = "/swagger/v1/swagger.json";
                options.RoutePrefix = "api-docs";
                options.ConfigObject = new ConfigObject
                {
                    ExpandResponses = "200,201,202",
                    PathInMiddlePanel = true,
                    NativeScrollbars = false,
                    DisableSearch = false,
                    HideDownloadButton = false,
                    RequiredPropsFirst = true,
                    OnlyRequiredInSamples = false,
                    HideHostname = false,
                };
            });
        }
    }