using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data.Catalog;
using PrestivaCars.Data.Data.Cms;
using PrestivaCars.Data.Data.CMS;
using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Users;
using PrestivaCars.Data.Data.Vehicles;
using System.Reflection;

namespace PrestivaCars.Data.Data
{
    /// <summary>
    /// The folowing model represents the database context for the PrestivaCars application.
    /// It includes DbSet properties for the Page, Vehicle, VehicleCategory, VehicleOffer, and SavedOffer entities.
    /// The purpose of this context is to manage the connection to the database and provide access to the data through the defined DbSet properties.
    /// </summary>
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
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }
        public DbSet<VehicleOfferFeature> VehicleOfferFeatures { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Banner> Banners { get; set; }

        /// <summary>
        /// The following methid overrides the SaveChanges method of the DbContext class to automatically set audit fields (CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) for entities that inherit from the BaseEntity class.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        /// <summary>
        /// The following method overrides the SaveChangesAsync method of the DbContext class to automatically set audit fields (CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) for entities that inherit from the BaseEntity class when saving changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;
                var currentUser = "Admin";

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = currentUser;
                    entry.Entity.IsActive = true;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = currentUser;

                    // Prevent modification of CreatedAt and CreatedBy fields when updating an existing entity
                    entry.Property(p => p.CreatedAt).IsModified = false;
                    entry.Property(p => p.CreatedBy).IsModified = false;
                }
            }
        }

    }
}