using AutoMapper;
using KTravelsApi.Features.Restaurants.Models;
using KTravelsApi.Features.Restaurants.ViewModels;

namespace KTravelsApi.Features.Restaurants.MapperProfiles;

public class RestaurantProfiles : Profile
{
    public RestaurantProfiles()
    {
        CreateMap<RestaurantCreateModel, Restaurant>();

        CreateMap<Restaurant, RestaurantViewModel>();

        CreateMap<RestaurantUpdateModel, Restaurant>();
    }
}