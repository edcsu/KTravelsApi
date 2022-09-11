using KTravelsApi.Features.Restaurants.ViewModels;

namespace KTravelsApi.Features.Restaurants.Services;

public interface IRestaurantService
{
    Task<RestaurantViewModel> AddRestaurantAsync(
        RestaurantCreateModel createModel, 
        CancellationToken cancellationToken = default);
        
    Task<RestaurantViewModel> GetRestaurantById(Guid id, 
        CancellationToken cancellationToken = default);

    // List<HotelReviewViewModel> GetHotelReviews(CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteRestaurantReviewAsync(Guid id, 
        CancellationToken cancellationToken = default);

    Task<RestaurantViewModel> UpdateRestaurantAsync(Guid id, 
        RestaurantUpdateModel updateModel, CancellationToken cancellationToken = default);
}