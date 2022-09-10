using AutoMapper;
using KTravelsApi.Core.Exceptions;
using KTravelsApi.Features.HotelReviews.Services;
using KTravelsApi.Features.Hotels.Models;
using KTravelsApi.Features.Hotels.Repositories;
using KTravelsApi.Features.Hotels.ViewModels;

namespace KTravelsApi.Features.Hotels.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly ILogger<HotelReviewService> _logger;
    private readonly IMapper _mapper;

    public HotelService(IHotelRepository hotelRepository, 
        ILogger<HotelReviewService> logger, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<HotelViewModel> AddHotelAsync(HotelCreateModel createModel, 
        CancellationToken cancellationToken = default)
    {
        var hotel = _mapper.Map<Hotel>(createModel);

        await _hotelRepository.AddAsync(hotel, cancellationToken);

        var viewModel = _mapper.Map<HotelViewModel>(hotel);
        return viewModel;
    }

    public async Task<HotelViewModel> GetHotelById(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var hotel = await _hotelRepository.FindAsync(id, cancellationToken);
        if (hotel is null)
        {
            _logger.LogError("Hotel with the id: {HotelId} does not exist", id);
            throw new ClientFriendlyException($"Hotel with {id} does not exist");
        }
        
        return _mapper.Map<HotelViewModel>(hotel);
    }

    public async Task<bool> SoftDeleteHotelAsync(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var hotel = await _hotelRepository.FindAsync(id, cancellationToken);
        if (hotel is null)
        {
            _logger.LogError("Hotel with the id: {HotelId} does not exist", id);
            throw new ClientFriendlyException($"Hotel with {id} does not exist");
        }

        hotel.IsDeleted = true;
        await _hotelRepository.UpdateAsync(hotel, cancellationToken); 
        return true;
    }

    public async Task<HotelViewModel> UpdateHotelAsync(Guid id, 
        HotelUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var hotel = await _hotelRepository.FindAsync(id, cancellationToken);
        if (hotel is null)
        {
            _logger.LogError("Hotel with the id: {HotelId} does not exist", id);
            throw new ClientFriendlyException($"Hotel with {id} does not exist");
        }

        var updatedHotel = _mapper.Map<Hotel>(updateModel);
        updatedHotel.Id = id;

        var result =await _hotelRepository.UpdateAsync(updatedHotel, cancellationToken);
        return _mapper.Map<HotelViewModel>(result);
    }
}