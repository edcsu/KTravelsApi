using System.Net.Mime;
using KTravelsApi.Core.Controllers;
using KTravelsApi.Features.RestaurantReviews.ViewModels;
using KTravelsApi.Features.Restaurants.Services;
using KTravelsApi.Features.Restaurants.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KTravelsApi.Features.Restaurants.Controllers;

[Route("api/restaurants")]
public class RestaurantsController : BaseController
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    /// <summary>
    /// Get List of templates
    /// </summary>
    /// <remarks>Endpoint to get a list of Templates.</remarks>
    /// <example></example>
    /// <returns></returns>
    // [SwaggerResponse(
    //     StatusCodes.Status200OK, "Operation successful",
    //     typeof(List<HotelReviewViewModel>))]
    // [HttpGet(Name = "Get List of templates")]
    // [Produces(MediaTypeNames.Application.Json)]
    // public IActionResult Get()
    // {
    //     var templatesList = _hotelReviewService.GetTemplates();
    //
    //     return Ok(templatesList);
    // }

    /// <summary>
    /// Get restaurant Details
    /// </summary>
    /// <remarks>Endpoint for getting details of a specific restaurant by unique id.</remarks>
    /// <param name="id">id</param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The details of a given restaurant",
        typeof(RestaurantReviewViewModel))]
    [HttpGet("{id:guid}", Name = "Get restaurant by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _restaurantService.GetRestaurantById(id);

        return Ok(result);
    }

    /// <summary>
    /// Add a hotel
    /// </summary>
    /// <remarks>
    /// Accepts request for creating a hotel.
    /// </remarks>
    /// <returns></returns>
    [HttpPost(Name = "AddRestaurant")]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        description: "The restaurant has been created.",
        typeof(RestaurantViewModel))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Create(
        [FromBody, SwaggerRequestBody("The restaurant request payload", Required = true)]
        RestaurantCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var viewModel = await _restaurantService.AddRestaurantAsync(createModel, cancellationToken);

        return CreatedAtAction("GetById", viewModel.Id, viewModel);
    }

    /// <summary>
    /// Edit a Hotel
    /// </summary>
    /// <remarks>Endpoint for editing a restaurant by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="updateModel"></param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The restaurant has been updated.",
        typeof(RestaurantViewModel))]
    [HttpPut("{id:guid}", Name = "Edit restaurant by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> EditById([FromRoute] Guid id, 
        [FromBody] RestaurantUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _restaurantService.UpdateRestaurantAsync(id, updateModel, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete a restaurant
    /// </summary>
    /// <remarks>Endpoint for deleting a restaurant by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The restaurant has been deleted.")]
    [HttpDelete("{id:guid}", Name = "Delete restaurant by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _restaurantService.SoftDeleteRestaurantReviewAsync(id, cancellationToken);

        return Ok(result);
    }
}