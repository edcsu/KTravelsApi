using KTravelsApi.Features.RestaurantReviews.ViewModels;

namespace KTravelsApi.Features.RestaurantReviews.Services;

public interface IRestaurantReviewService
{
    Task<RestaurantReviewViewModel> AddRestaurantReviewAsync(Guid hotelId,
        RestaurantReviewCreateModel createModel, 
        CancellationToken cancellationToken = default);
        
    Task<RestaurantReviewViewModel> GetRestaurantReviewById(Guid id, 
        CancellationToken cancellationToken = default);

    // List<HotelReviewViewModel> GetHotelReviews(CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteRestaurantReviewAsync(Guid id, 
        CancellationToken cancellationToken = default);

    Task<RestaurantReviewViewModel> UpdateRestaurantReviewAsync(Guid id, 
        RestaurantReviewUpdateModel updateModel, CancellationToken cancellationToken = default);
}