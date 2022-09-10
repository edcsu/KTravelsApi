using KTravelsApi.Core.Models;
using KTravelsApi.Features.Hotels.Models;
using KTravelsApi.Features.Restaurants.Models;

namespace KTravelsApi.Features.RestaurantReviews.Models;

public class RestaurantReview: BaseModel
{
    public string Author { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Body { get; set; } = default!;
    
    public Guid RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; } = default!;
}