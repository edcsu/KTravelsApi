using System.Net;
using System.Net.Mime;
using System.Text.Json;
using KTravelsApi.Core.Exceptions;
using KTravelsApi.Core.Helpers;

namespace KTravelsApi.Core.Middleware;

public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly ILogger<ErrorHandlingMiddleware> _iLogger;

        public ErrorHandlingMiddleware(RequestDelegate next, IConfiguration config,
            ILogger<ErrorHandlingMiddleware> iLogger)
        {
            _next = next;
            _config = config;
            _iLogger = iLogger;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var showDetailedErrors = _config.GetValue<bool>("ShowDetailedErrors");
            if (exception is not ServerException)
            {
                _iLogger.LogError(exception, "global.error");
            }

            var code = HttpStatusCode.InternalServerError;
            var message = "Unknown error, please contact administrator";
            if (showDetailedErrors)
            {
                message = exception.StackTrace;
            }
            else
            {
                switch (exception)
                {
                    case NotFoundException _:
                        code = HttpStatusCode.NotFound;
                        message = exception.Message;
                        break;

                    case ServerException _:
                        code = HttpStatusCode.BadRequest;
                        message = exception.Message;
                        break;

                    case UnauthorizedAccessException _:
                        code = HttpStatusCode.Unauthorized;
                        break;

                    case TaskCanceledException _:
                        {
                            code = HttpStatusCode.RequestTimeout;
                            message = "Timeout of 100 seconds elapsed";
                            break;
                        }

                    case OperationCanceledException _:
                        {
                            code = HttpStatusCode.BadRequest;
                            message = "Request was cancelled";
                            break;
                        }

                }
            }

            var result = JsonSerializer.Serialize(new { code, message },
                JsonHelpers.GetSerializerSettings());

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }


        public static string ParsePostgresDuplicate(string? data)
        {
            try
            {
                // Key ("Value")=(doe@skecorp.io) already exists.
                var cleanData = data?
                    .Split("=")[1]
                    .Replace("already exists.", "")
                    .Trim();

                return $"Duplicate value: {cleanData}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Duplicate value, please check your inputs";
            }
        }
    }