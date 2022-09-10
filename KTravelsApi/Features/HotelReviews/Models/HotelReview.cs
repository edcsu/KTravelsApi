using System.ComponentModel.DataAnnotations.Schema;
using KTravelsApi.Core.Models;
using KTravelsApi.Features.Hotels.Models;

namespace KTravelsApi.Features.HotelReviews.Models;

public class HotelReview : BaseModel
{
    public string Author { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Body { get; set; } = default!;
    
    public Guid HotelId { get; set; }

    public Hotel Hotel { get; set; } = default!;
}