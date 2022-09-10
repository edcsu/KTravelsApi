using KTravelsApi.Core.ViewModels;

namespace KTravelsApi.Features.HotelReviews.ViewModels;

public record HotelReviewViewModel : BaseViewModel
{
    public string Author { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Body { get; set; } = default!;
    
    public Guid HotelId { get; set; }
    
};