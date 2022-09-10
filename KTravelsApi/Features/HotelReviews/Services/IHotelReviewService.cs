using KTravelsApi.Features.HotelReviews.ViewModels;

namespace KTravelsApi.Features.HotelReviews.Services;

public interface IHotelReviewService
{
    Task<HotelReviewViewModel> AddHotelReviewAsync(Guid hotelId,
        HotelReviewCreateModel createModel, 
        CancellationToken cancellationToken = default);
        
    Task<HotelReviewViewModel> GetHotelReviewById(Guid id, 
        CancellationToken cancellationToken = default);

    // List<HotelReviewViewModel> GetHotelReviews(CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteHotelReviewAsync(Guid id, 
        CancellationToken cancellationToken = default);

    Task<HotelReviewViewModel> UpdateHotelReviewAsync(Guid id, 
        HotelReviewUpdateModel updateModel, CancellationToken cancellationToken = default);
}