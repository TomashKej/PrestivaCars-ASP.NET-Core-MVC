using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data.Catalog;
using PrestivaCars.Data.Data.CMS;
using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Users;
using PrestivaCars.Data.Data.Vehicles;


namespace PrestivaCars.Data.Data
{
    /// <summary>
    /// Represents the main database context for the PrestivaCars application.
    /// This class is responsible for managing database access and exposing DbSet properties
    /// for application entities such as vehicles, offers, categories, users, banners and catalogue data.
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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<VehicleColour> VehicleColours { get; set; }

        /// <summary>
        /// Saves changes to the database and automatically updates audit fields
        /// before the changes are written.
        /// </summary>
        /// <returns>The number of records affected in the database.</returns>
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }


        /// <summary>
        /// Saves changes to the database asynchronously and automatically updates audit fields
        /// before the changes are written.
        /// </summary>
        /// <param name="cancellationToken">
        /// Token used to cancel the asynchronous save operation if needed.
        /// </param>
        /// <returns>
        /// A task containing the number of records affected in the database.
        /// </returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Automatically sets audit fields for all tracked entities that inherit from BaseEntity.
        /// This method handles creation, update and soft delete information before saving changes.
        /// </summary>
        /// <remarks>
        /// For new entities, it sets CreatedAt, CreatedBy and marks the entity as active.
        /// For updated entities, it sets UpdatedAt and UpdatedBy while protecting CreatedAt and CreatedBy.
        /// For deleted entities, it changes the operation from a physical delete to a soft delete by
        /// setting IsActive to false and filling DeletedAt and DeletedBy.
        /// </remarks>
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

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entry.Entity.IsActive = false;
                    entry.Entity.DeletedAt = now;
                    entry.Entity.DeletedBy = currentUser;

                    entry.Property(p => p.CreatedAt).IsModified = false;
                    entry.Property(p => p.CreatedBy).IsModified = false;
                }
            }
        }

    }
}