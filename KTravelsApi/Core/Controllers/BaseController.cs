using System.Net.Mime;
using KTravelsApi.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KTravelsApi.Core.Controllers;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerResponse(
    StatusCodes.Status400BadRequest,
    "The request data is invalid",
    typeof(StandardError))]
[SwaggerResponse(
    StatusCodes.Status401Unauthorized,
    "Not authorized to access the endpoint",
    typeof(BasicError))]
[SwaggerResponse(
    StatusCodes.Status403Forbidden,
    "Refused access to the endpoint",
    typeof(BasicError))]
[SwaggerResponse(
    StatusCodes.Status404NotFound,
    "Request does not exist",
    typeof(BasicError))]
[SwaggerResponse(
    StatusCodes.Status500InternalServerError,
    "The server encountered an unexpected error",
    typeof(BasicError))]
public class BaseController : ControllerBase
{
}