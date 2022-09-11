using System.Net.Mime;
using KTravelsApi.Core.Controllers;
using KTravelsApi.Features.HotelReviews.ViewModels;
using KTravelsApi.Features.Hotels.Services;
using KTravelsApi.Features.Hotels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KTravelsApi.Features.Hotels.Controllers;

[Route("api/hotels")]
public class HotelsController : BaseController
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
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
    /// Get hotel Details
    /// </summary>
    /// <remarks>Endpoint for getting details of a specific hotel by unique id.</remarks>
    /// <param name="id">id</param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The details of a given hotel",
        typeof(HotelReviewViewModel))]
    [HttpGet("{id:guid}", Name = "Get hotel by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _hotelService.GetHotelById(id);

        return Ok(result);
    }

    /// <summary>
    /// Add a hotel
    /// </summary>
    /// <remarks>
    /// Accepts request for creating a hotel.
    /// </remarks>
    /// <returns></returns>
    [HttpPost(Name = "AddLicenseRequest")]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        description: "The hotel has been created.",
        typeof(HotelViewModel))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Create(
        [FromBody, SwaggerRequestBody("The hotel review request payload", Required = true)]
        HotelCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var viewModel = await _hotelService.AddHotelAsync(createModel, cancellationToken);

        return CreatedAtAction("GetById", viewModel.Id, viewModel);
    }

    /// <summary>
    /// Edit a Hotel
    /// </summary>
    /// <remarks>Endpoint for editing a hotel by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="updateModel"></param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The request has been updated.",
        typeof(HotelViewModel))]
    [HttpPut("{id:guid}", Name = "Edit hotel by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> EditById([FromRoute] Guid id, 
        [FromBody] HotelUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _hotelService.UpdateHotelAsync(id, updateModel, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete a Hotel
    /// </summary>
    /// <remarks>Endpoint for deleting a hotel by unique id.</remarks>
    /// <param name="id">id</param>
    /// <param name="cancellationToken"></param>
    /// <example>8754b7cb-d0fc-4499-8a1a-ebfb721cf0fc</example>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK,
        description: "The hotel has been deleted.")]
    [HttpDelete("{id:guid}", Name = "Delete hotel by Id")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return BadRequest(new { Message = "Invalid request Id" });

        var result = await _hotelService.SoftDeleteHotelAsync(id, cancellationToken);

        return Ok(result);
    }
}