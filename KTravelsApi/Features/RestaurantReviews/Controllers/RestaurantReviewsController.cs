using System.Net.Mime;
using KTravelsApi.Core.Controllers;
using KTravelsApi.Features.HotelReviews.Services;
using KTravelsApi.Features.HotelReviews.ViewModels;
using KTravelsApi.Features.RestaurantReviews.Services;
using KTravelsApi.Features.RestaurantReviews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KTravelsApi.Features.HotelReviews.Controllers;

[Route("api/restaurantReviews")]
public class RestaurantReviewsController : BaseController
{
    private readonly IRestaurantReviewService _restaurantReviewService; 

    public RestaurantReviewsController(IRestaurantReviewService restaurantReviewService)
    {
        _restaurantReviewService = restaurantReviewService;
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
    /// Get restaurant review Details
    /// </summary>
    /// <remarks>Endpoint for getting details of a specific restaurant review by unique id.</remarks>
    /// <param name="id">id</param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The details of a given restaurant review",
        typeof(RestaurantReviewViewModel))]
    [HttpGet("{id:guid}", Name = "Get restaurant review by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid restaurant review Id" });

        var result = await _restaurantReviewService.GetRestaurantReviewById(id);

        return Ok(result);
    }

    /// <summary>
    /// Make a restaurant review
    /// </summary>
    /// <remarks>
    /// Accepts request for creating a reataurant review.
    /// </remarks>
    /// <returns></returns>
    [HttpPost(template:"{restaurantId:guid}", Name = "AddRestaurantReview")]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        description: "The restaurant review has been created.",
        typeof(HotelReviewViewModel))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Create([FromRoute] Guid hotelId,
        [FromBody, SwaggerRequestBody("The restaurant review request payload", Required = true)]
        RestaurantReviewCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var viewModel = await _restaurantReviewService.AddRestaurantReviewAsync(hotelId, createModel, cancellationToken);

        return CreatedAtAction("GetById", viewModel.Id, viewModel);
    }

    /// <summary>
    /// Edit a restaurant review
    /// </summary>
    /// <remarks>Endpoint for editing a restaurant review by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="updateModel"></param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The restaurant review has been edited.",
        typeof(RestaurantReviewViewModel))]
    [HttpPut("{id:guid}", Name = "Edit a restaurant review by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> EditById([FromRoute] Guid id, 
        [FromBody] RestaurantReviewUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _restaurantReviewService.UpdateRestaurantReviewAsync(id, updateModel, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete a restaurant review
    /// </summary>
    /// <remarks>Endpoint for deleting a restaurant review by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The restaurant review has been deleted.")]
    [HttpDelete("{id:guid}", Name = "Delete restaurant review by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid restaurant review Id" });

        var result = await _restaurantReviewService.SoftDeleteRestaurantReviewAsync(id, cancellationToken);

        return Ok(result);
    }
}