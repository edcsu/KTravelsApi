using AutoMapper;
using KTravelsApi.Core.Exceptions;
using KTravelsApi.Features.RestaurantReviews.Models;
using KTravelsApi.Features.RestaurantReviews.Repositories;
using KTravelsApi.Features.RestaurantReviews.ViewModels;
using KTravelsApi.Features.Restaurants.Models;
using KTravelsApi.Features.Restaurants.Repositories;

namespace KTravelsApi.Features.RestaurantReviews.Services;

public class RestaurantReviewService : IRestaurantReviewService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRestaurantReviewRepository _restaurantReviewRepository;
    private readonly ILogger<RestaurantReviewService> _logger;
    private readonly IMapper _mapper;

    public RestaurantReviewService(IRestaurantRepository restaurantRepository, 
        IRestaurantReviewRepository restaurantReviewRepository, 
        ILogger<RestaurantReviewService> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantReviewRepository = restaurantReviewRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RestaurantReviewViewModel> AddRestaurantReviewAsync(Guid hotelId, 
        RestaurantReviewCreateModel createModel,
        CancellationToken cancellationToken = default)
    {
        var review = _mapper.Map<RestaurantReview>(createModel);
        var restaurantExists = await _restaurantRepository.ExistsAsync<Restaurant>(hotelId, cancellationToken);
        if (restaurantExists is false)
        {
            _logger.LogError("Tried to create a review for a restaurant with the Id: {HotelId}", hotelId);
            throw new ClientFriendlyException($"Restaurant with id {hotelId} does not exist");
        }

        await _restaurantReviewRepository.AddAsync(review, cancellationToken);

        var viewModel = _mapper.Map<RestaurantReviewViewModel>(review);
        return viewModel;
    }

    public async Task<RestaurantReviewViewModel> GetRestaurantReviewById(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var restaurantReview = await _restaurantReviewRepository.FindAsync(id, cancellationToken);
        if (restaurantReview is null)
        {
            _logger.LogError("Restaurant review with the id: {RestaurantReviewId} does not exist", id);
            throw new ClientFriendlyException($"Restaurant review with {id} does not exist");
        }
        return _mapper.Map<RestaurantReviewViewModel>(restaurantReview);
    }

    public async Task<bool> SoftDeleteRestaurantReviewAsync(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var restaurantReview = await _restaurantReviewRepository.FindAsync(id, cancellationToken);
        if (restaurantReview is null)
        {
            _logger.LogError("Restaurant review with the id: {RestaurantReviewId} does not exist", id);
            throw new ClientFriendlyException($"Restaurant review with {id} does not exist");
        }

        restaurantReview.IsDeleted = true;
        await _restaurantReviewRepository.UpdateAsync(restaurantReview, cancellationToken); 
        return true;
    }

    public async Task<RestaurantReviewViewModel> UpdateRestaurantReviewAsync(Guid id, 
        RestaurantReviewUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        var restaurantReview = await _restaurantReviewRepository.FindAsync(id, cancellationToken);
        if (restaurantReview is null)
        {
            _logger.LogError("Restaurant review with the id: {RestaurantReviewId} does not exist", id);
            throw new ClientFriendlyException($"Restaurant review with {id} does not exist");
        }

        var updatedReview = _mapper.Map<RestaurantReview>(updateModel);
        updatedReview.Id = id;
        updatedReview.LastUpdated = DateTime.UtcNow;

        var result =await _restaurantReviewRepository.UpdateAsync(updatedReview, cancellationToken);
        return _mapper.Map<RestaurantReviewViewModel>(result);
    }
}