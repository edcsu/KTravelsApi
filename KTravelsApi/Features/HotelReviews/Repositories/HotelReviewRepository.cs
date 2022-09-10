using KTravelsApi.Data;
using KTravelsApi.Features.HotelReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace KTravelsApi.Features.HotelReviews.Repositories;

public class HotelReviewRepository : IHotelReviewRepository
{
    private readonly ApplicationDbContext _context;

    public HotelReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HotelReview> AddAsync(HotelReview hotelReview, 
        CancellationToken cancellationToken = default)
    {
        await _context.HotelReviews.AddAsync(hotelReview, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return hotelReview;
    }

    public async Task<bool> ExistsAsync<TE>(Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .AsNoTracking()
            .AnyAsync(it => it.Id == id && it.IsDeleted == false, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.HotelReviews
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    public async Task<HotelReview?> FindAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.HotelReviews
            .AsNoTracking()
            .Include(it => it.Hotel)
            .FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted == false, cancellationToken);
    }

    public async Task<HotelReview> UpdateAsync(HotelReview hotelReview, 
        CancellationToken cancellationToken = default)
    {
        _context.HotelReviews.Update(hotelReview);
        await _context.SaveChangesAsync(cancellationToken);
        return hotelReview;
    }
}