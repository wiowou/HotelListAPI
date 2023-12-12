using Microsoft.EntityFrameworkCore;

namespace HotelListAPI.Data;

public class HotelListingDbContext : DbContext
{
  public HotelListingDbContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<Hotel> Hotels { get; set; }
  public DbSet<Country> Countries { get; set; }

  // use this to seed a dev database with initial values
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Country>().HasData(
      new Country
      {
        Id = 1,
        Name = "United States",
        ShortName = "US"
      },
      new Country
      {
        Id = 2,
        Name = "Great Britain",
        ShortName = "GB"
      },
      new Country
      {
        Id = 3,
        Name = "France",
        ShortName = "Fr"
      }
    );

    modelBuilder.Entity<Hotel>().HasData(
      new Hotel
      {
        Id = 1,
        Name = "Sandals Resort and Spa",
        Address = "Negril",
        CountryId = 1,
        Rating = 4.5
      },
      new Hotel
      {
        Id = 2,
        Name = "Comfort Suites",
        Address = "George Town",
        CountryId = 3,
        Rating = 4.3
      }
    );
  }
}
