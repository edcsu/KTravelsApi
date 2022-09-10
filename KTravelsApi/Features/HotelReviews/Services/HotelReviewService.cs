using AutoMapper;
using KTravelsApi.Core.Exceptions;
using KTravelsApi.Features.HotelReviews.Models;
using KTravelsApi.Features.HotelReviews.Repositories;
using KTravelsApi.Features.HotelReviews.ViewModels;
using KTravelsApi.Features.Hotels.Models;
using KTravelsApi.Features.Hotels.Repositories;

namespace KTravelsApi.Features.HotelReviews.Services;

public class HotelReviewService : IHotelReviewService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly ILogger<HotelReviewService> _logger;
    private readonly IMapper _mapper;

    public HotelReviewService(IHotelReviewRepository hotelReviewRepository, 
        ILogger<HotelReviewService> logger, IMapper mapper, 
        IHotelRepository hotelRepository)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _logger = logger;
        _mapper = mapper;
        _hotelRepository = hotelRepository;
    }

    public async Task<HotelReviewViewModel> AddHotelReviewAsync(Guid hotelId, 
        HotelReviewCreateModel createModel,
        CancellationToken cancellationToken = default)
    {
        var review = _mapper.Map<HotelReview>(createModel);
        var hotelExists = await _hotelRepository.ExistsAsync<Hotel>(hotelId, cancellationToken);
        if (hotelExists is false)
        {
            _logger.LogError("Tried to create a review for a hotel with the Id: {HotelId}", hotelId);
            throw new DuplicateEntityException($"Hotel with id {hotelId} does not exist");
        }

        await _hotelReviewRepository.AddAsync(review, cancellationToken);

        var viewModel = _mapper.Map<HotelReviewViewModel>(review);
        return viewModel;
    }

    public async Task<HotelReviewViewModel> GetHotelReviewById(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var review = await _hotelReviewRepository.FindAsync(id, cancellationToken);
        if (review is null)
        {
            _logger.LogError("Hotel review with the id: {HotelReviewId} does not exist", id);
            throw new ClientFriendlyException($"Hotel review with {id} does not exist");
        }
        return _mapper.Map<HotelReviewViewModel>(review);
    }

    // public List<HotelReviewViewModel> GetHotelReviews(CancellationToken cancellationToken = default)
    // {
    //     var reviews = _hotelReviewRepository.GetAll();
    //     return _mapper.Map<List<HotelReviewViewModel>>(reviews);
    // }

    public async Task<bool> SoftDeleteHotelReviewAsync(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var hotelReview = await _hotelReviewRepository.FindAsync(id, cancellationToken);
        if (hotelReview is null)
        {
            _logger.LogError("Hotel review with the id: {HotelReviewId} does not exist", id);
            throw new ClientFriendlyException($"Hotel review with {id} does not exist");
        }

        hotelReview.IsDeleted = true;
        await _hotelReviewRepository.UpdateAsync(hotelReview, cancellationToken); 
        return true;
    }

    public async Task<HotelReviewViewModel> UpdateHotelReviewAsync(Guid id, 
        HotelReviewUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        var hotelReview = await _hotelReviewRepository.FindAsync(id, cancellationToken);
        if (hotelReview is null)
        {
            _logger.LogError("Hotel review with the id: {HotelReviewId} does not exist", id);
            throw new ClientFriendlyException($"Hotel review with {id} does not exist");
        }

        var updatedTemplate = _mapper.Map<HotelReview>(updateModel);
        updatedTemplate.Id = id;

        var result =await _hotelReviewRepository.UpdateAsync(updatedTemplate, cancellationToken);
        return _mapper.Map<HotelReviewViewModel>(result);
    }
}