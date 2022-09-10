using KTravelsApi.Features.HotelReviews.Models;
using KTravelsApi.Features.Hotels.Models;
using KTravelsApi.Features.RestaurantReviews.Models;
using KTravelsApi.Features.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace KTravelsApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Hotel> Hotels { get; set; }
    
    public DbSet<Restaurant> Restaurants { get; set; }
    
    public DbSet<HotelReview> HotelReviews { get; set; }
    
    public DbSet<RestaurantReview> RestaurantReviews { get; set; }
}