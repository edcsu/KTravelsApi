using KTravelsApi.Core.ViewModels;

namespace KTravelsApi.Features.RestaurantReviews.ViewModels;

public record RestaurantReviewViewModel : BaseViewModel
{
    public string Author { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Body { get; set; } = default!;
    
    public Guid HotelId { get; set; }
};