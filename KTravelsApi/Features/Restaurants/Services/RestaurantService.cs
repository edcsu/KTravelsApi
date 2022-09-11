using AutoMapper;
using KTravelsApi.Core.Exceptions;
using KTravelsApi.Features.RestaurantReviews.Models;
using KTravelsApi.Features.RestaurantReviews.Services;
using KTravelsApi.Features.RestaurantReviews.ViewModels;
using KTravelsApi.Features.Restaurants.Models;
using KTravelsApi.Features.Restaurants.Repositories;
using KTravelsApi.Features.Restaurants.ViewModels;

namespace KTravelsApi.Features.Restaurants.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<RestaurantReviewService> _logger;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, 
        ILogger<RestaurantReviewService> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RestaurantViewModel> AddRestaurantAsync(
        RestaurantCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var restaurant = _mapper.Map<Restaurant>(createModel);

        await _restaurantRepository.AddAsync(restaurant, cancellationToken);

        var viewModel = _mapper.Map<RestaurantViewModel>(restaurant);
        return viewModel;
    }

    public async Task<RestaurantViewModel> GetRestaurantById(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.FindAsync(id, cancellationToken);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant with the id: {RestaurantId} does not exist", id);
            throw new ClientFriendlyException($"Restaurant with {id} does not exist");
        }
        return _mapper.Map<RestaurantViewModel>(restaurant);
    }

    public async Task<bool> SoftDeleteRestaurantReviewAsync(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.FindAsync(id, cancellationToken);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant with the id: {RestaurantId} does not exist", id);
            throw new ClientFriendlyException($"Restaurant with {id} does not exist");
        }

        restaurant.IsDeleted = true;
        await _restaurantRepository.UpdateAsync(restaurant, cancellationToken); 
        return true;
    }

    public async Task<RestaurantViewModel> UpdateRestaurantAsync(Guid id, 
        RestaurantUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.FindAsync(id, cancellationToken);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant with the id: {RestaurantId} does not exist", id);
            throw new ClientFriendlyException($"Restaurant with {id} does not exist");
        }

        var updatedRestaurant = _mapper.Map<Restaurant>(updateModel);
        updatedRestaurant.Id = id;
        updatedRestaurant.LastUpdated = DateTime.UtcNow;

        var result =await _restaurantRepository.UpdateAsync(updatedRestaurant, cancellationToken);
        return _mapper.Map<RestaurantViewModel>(result);
    }
}