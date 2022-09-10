using AutoMapper;
using KTravelsApi.Features.RestaurantReviews.Models;
using KTravelsApi.Features.RestaurantReviews.ViewModels;

namespace KTravelsApi.Features.RestaurantReviews.MapperProfiles;

public class RestaurantReviewProfiles : Profile
{
    public RestaurantReviewProfiles()
    {
        CreateMap<RestaurantReviewCreateModel, RestaurantReview>();

        CreateMap<RestaurantReview, RestaurantReviewViewModel>();

        CreateMap<RestaurantReviewUpdateModel, RestaurantReview>();
    }
}