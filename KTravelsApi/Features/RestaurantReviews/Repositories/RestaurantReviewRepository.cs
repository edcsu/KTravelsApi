using KTravelsApi.Data;
using KTravelsApi.Features.RestaurantReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace KTravelsApi.Features.RestaurantReviews.Repositories;

public class RestaurantReviewRepository : IRestaurantReviewRepository
{
    private readonly ApplicationDbContext _context;

    public RestaurantReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RestaurantReview> AddAsync(RestaurantReview restaurantReview, 
        CancellationToken cancellationToken = default)
    {
        await _context.RestaurantReviews.AddAsync(restaurantReview, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return restaurantReview;
    }

    public async Task<bool> ExistsAsync<TE>(Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.RestaurantReviews
            .AsNoTracking()
            .AnyAsync(it => it.Id == id && it.IsDeleted == false, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.RestaurantReviews
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    public async Task<RestaurantReview?> FindAsync(Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.RestaurantReviews
            .AsNoTracking()
            .Include(it => it.Restaurant)
            .FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted == false, cancellationToken);
    }

    public async Task<RestaurantReview> UpdateAsync(RestaurantReview restaurantReview, 
        CancellationToken cancellationToken = default)
    {
        _context.RestaurantReviews.Update(restaurantReview);
        await _context.SaveChangesAsync(cancellationToken);
        return restaurantReview;
    }
}