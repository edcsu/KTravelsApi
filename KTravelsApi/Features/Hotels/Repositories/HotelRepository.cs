using KTravelsApi.Data;
using KTravelsApi.Features.Hotels.Models;
using Microsoft.EntityFrameworkCore;

namespace KTravelsApi.Features.Hotels.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly ApplicationDbContext _context;

    public HotelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Hotel> AddAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        await _context.Hotels.AddAsync(hotel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return hotel;
    }

    public async Task<bool> ExistsAsync<TE>(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .AsNoTracking()
            .AnyAsync(it => it.Id == id && it.IsDeleted == false, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    public async Task<Hotel?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .AsNoTracking()
            .Include(it => it.Reviews)
            .FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted == false, cancellationToken);
    }

    public async Task<Hotel> UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync(cancellationToken);
        return hotel;
    }
}