using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Vehicles;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Services.Abstraction;

namespace PrestivaCars.Services.Vehicles
{
    /// <summary>
    /// Provides operations for retrieving and managing vehicle offers, including active, featured, and
    /// category-specific offers.
    /// </summary>
    /// <remarks>This service encapsulates business logic for querying vehicle offers from the data context.
    /// It is intended to be used by application layers that require access to vehicle offer data, such as controllers
    /// or other services. All retrieval methods return only active offers and include related vehicle details for
    /// comprehensive data access.</remarks>
    public class VehicleOfferService : BaseService, IVehicleOfferService
    {
        public VehicleOfferService(PrestivaCarsContext context)
            : base(context)
        {  
        }

        public async Task<IList<VehicleOffer>> GetActiveOffersAsync()
        {
            var offers = await GetBaseOffersQuery()
                .OrderByDescending(o => o.IsFeatured)
                .ThenByDescending(o => o.CreatedAt) 
                .ToListAsync();

            return offers;
        }

        public async Task<IList<VehicleOffer>> GetFeaturedOffersAsync(int count)
        {
            var offers = await GetBaseOffersQuery()
                .Where(o => o.IsFeatured)
                .OrderByDescending(o => o.CreatedAt)
                .Take(count)
                .ToListAsync();

            return offers;
        }

        public async Task<IList<VehicleOffer>> GetOffersByCategoryAsync(int vehicleCategoryId)
        {
            var offers = await GetBaseOffersQuery()
                .Where(o => o.Vehicle.VehicleCategoryId == vehicleCategoryId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return offers;
        }

        public async Task<VehicleOffer?> GetOfferDetailsAsync(int vehicleOfferId)
        {
            var offer = await GetBaseOffersQuery()
                .FirstOrDefaultAsync(o => o.VehicleOfferId == vehicleOfferId);

            return offer;
        }
        private IQueryable<VehicleOffer> GetBaseOffersQuery()
        {
            return _context.VehicleOffers
                .AsNoTracking()
                .Where(o => o.IsActive)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.Brand)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.VehicleCategory)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.FuelType)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.TransmissionType)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.BodyType)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.VehicleColour)
                .Include(o => o.VehicleImages);
        }
    }
}
