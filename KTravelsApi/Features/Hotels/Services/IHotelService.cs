using KTravelsApi.Features.Hotels.ViewModels;

namespace KTravelsApi.Features.Hotels.Services;

public interface IHotelService
{
    Task<HotelViewModel> AddHotelAsync(HotelCreateModel createModel, 
        CancellationToken cancellationToken = default);
        
    Task<HotelViewModel> GetHotelById(Guid id, 
        CancellationToken cancellationToken = default);

    // List<HotelViewModel> GetHotels(CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteHotelAsync(Guid id, 
        CancellationToken cancellationToken = default);

    Task<HotelViewModel> UpdateHotelAsync(Guid id, 
        HotelUpdateModel updateModel, CancellationToken cancellationToken = default);
}