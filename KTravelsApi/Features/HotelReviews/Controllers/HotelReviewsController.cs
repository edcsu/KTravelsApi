using System.Net.Mime;
using KTravelsApi.Core.Controllers;
using KTravelsApi.Features.HotelReviews.Services;
using KTravelsApi.Features.HotelReviews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KTravelsApi.Features.HotelReviews.Controllers;

[Route("api/hotelReviews")]
public class HotelReviewsController : BaseController
{
    private readonly IHotelReviewService _hotelReviewService;

    public HotelReviewsController(IHotelReviewService hotelReviewService)
    {
        _hotelReviewService = hotelReviewService;
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
    /// Get hotel review Details
    /// </summary>
    /// <remarks>Endpoint for getting details of a specific hotel review by unique id.</remarks>
    /// <param name="id">id</param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The details of a given hotel review",
        typeof(HotelReviewViewModel))]
    [HttpGet("{id:guid}", Name = "Get hotel review by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _hotelReviewService.GetHotelReviewById(id);

        return Ok(result);
    }

    /// <summary>
    /// Make a hotel review
    /// </summary>
    /// <remarks>
    /// Accepts request for creating a hotel review.
    /// </remarks>
    /// <returns></returns>
    [HttpPost(template:"{hotelId:guid}", Name = "AddLicenseRequest")]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        description: "The template has been created.",
        typeof(HotelReviewViewModel))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Create([FromRoute] Guid hotelId,
        [FromBody, SwaggerRequestBody("The hotel review request payload", Required = true)]
        HotelReviewCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var viewModel = await _hotelReviewService.AddHotelReviewAsync(hotelId, createModel, cancellationToken);

        return CreatedAtAction("GetById", viewModel.Id, viewModel);
    }

    /// <summary>
    /// Edit a HotelReview
    /// </summary>
    /// <remarks>Endpoint for editing a template by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="updateModel"></param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The request has been verified.",
        typeof(HotelReviewViewModel))]
    [HttpPut("{id:guid}", Name = "Edit template by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> EditById([FromRoute] Guid id, 
        [FromBody] HotelReviewUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _hotelReviewService.UpdateHotelReviewAsync(id, updateModel, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete a HotelReview
    /// </summary>
    /// <remarks>Endpoint for deleting a template by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The request has been deleted.")]
    [HttpDelete("{id:guid}", Name = "Delete template by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _hotelReviewService.SoftDeleteHotelReviewAsync(id, cancellationToken);

        return Ok(result);
    }
}