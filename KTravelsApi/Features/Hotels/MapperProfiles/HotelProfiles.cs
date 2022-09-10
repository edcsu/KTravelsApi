using AutoMapper;
using KTravelsApi.Features.Hotels.Models;
using KTravelsApi.Features.Hotels.ViewModels;

namespace KTravelsApi.Features.Hotels.MapperProfiles;

public class HotelProfiles : Profile
{
    public HotelProfiles()
    {
        CreateMap<HotelCreateModel, Hotel>();

        CreateMap<Hotel, HotelViewModel>();

        CreateMap<HotelUpdateModel, Hotel>();
    }
}