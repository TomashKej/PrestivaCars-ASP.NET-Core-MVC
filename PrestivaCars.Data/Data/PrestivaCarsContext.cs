using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data.CMS;
using PrestivaCars.Data.Data.Vehicles;

namespace PrestivaCars.Data.Data
{
    public class PrestivaCarsContext : DbContext
    {
        public PrestivaCarsContext(DbContextOptions<PrestivaCarsContext> options)
            : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<VehicleOffer> VehicleOffers { get; set; }
        public DbSet<SavedOffer> SavedOffers { get; set; }
    }
}