namespace KTravelsApi.Features.HotelReviews.ViewModels;

public record HotelReviewCreateModel
{
    public string Author { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Body { get; set; } = default!;
}