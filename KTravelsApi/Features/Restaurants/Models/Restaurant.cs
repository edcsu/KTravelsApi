using KTravelsApi.Core.Models;
using KTravelsApi.Features.RestaurantReviews.Models;

namespace KTravelsApi.Features.Restaurants.Models;

public class Restaurant : BaseModel
{
    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? Keywords { get; set; }
    public string? PhoneNumber { get; set; }
    public string? RestaurantEmail { get; set; }
    public string? Address { get; set; }
    public string? Opens { get; set; }
    public string? Closes { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Area { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public double? Lat { get; set; }
    public double? Lng { get; set; }
    public int Price { get; set; }
    public string? ReserveUrl { get; set; }
    public string? MobileReserveUrl { get; set; }
    public string? ImageUrl { get; set; }

    public List<RestaurantReview> Reviews { get; set; } = default!;
}