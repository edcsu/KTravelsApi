using AutoMapper;
using KTravelsApi.Features.HotelReviews.Models;
using KTravelsApi.Features.HotelReviews.ViewModels;

namespace KTravelsApi.Features.HotelReviews.MapperProfiles;

public class HotelReviewProfiles : Profile
{
    public HotelReviewProfiles()
    {
        CreateMap<HotelReviewCreateModel, HotelReview>();

        CreateMap<HotelReview, HotelReviewViewModel>();

        CreateMap<HotelReviewUpdateModel, HotelReview>();
    }
}