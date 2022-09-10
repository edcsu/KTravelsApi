using KTravelsApi.Data;
using KTravelsApi.Features.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace KTravelsApi.Features.Restaurants.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _context;

    public RestaurantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Restaurant> AddAsync(Restaurant restaurant, 
        CancellationToken cancellationToken = default)
    {
        await _context.Restaurants.AddAsync(restaurant, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return restaurant;
    }

    public async Task<bool> ExistsAsync<TE>(Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .AsNoTracking()
            .AnyAsync(it => it.Id == id && it.IsDeleted == false, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    public async Task<Restaurant?> FindAsync(Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .AsNoTracking()
            .Include(it => it.Reviews)
            .FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted == false, cancellationToken);
    }

    public async Task<Restaurant> UpdateAsync(Restaurant restaurant, 
        CancellationToken cancellationToken = default)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync(cancellationToken);
        return restaurant;
    }
}