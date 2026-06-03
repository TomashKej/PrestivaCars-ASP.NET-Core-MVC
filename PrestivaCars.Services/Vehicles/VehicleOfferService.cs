using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Vehicles;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Services.Abstraction;

namespace PrestivaCars.Services.Vehicles
{
    /// <summary>
    /// Provides operations for retrieving and managing vehicle offers, including searching, filtering by category, and
    /// accessing featured or detailed offer information.
    /// </summary>
    /// <remarks>This service encapsulates business logic related to vehicle offers and is intended to be used
    /// by application layers that require access to active, featured, or categorized vehicle offers. All retrieval
    /// methods return only active offers. The service is not thread-safe and should be used according to the
    /// application's dependency injection and lifetime management strategy.</remarks>
    public class VehicleOfferService : BaseService, IVehicleOfferService
    {
        public VehicleOfferService(PrestivaCarsContext context)
            : base(context)
        {  
        }

        /// <summary>
        /// Asynchronously retrieves a list of active vehicle offers, optionally filtered by a search term.
        /// </summary>
        /// <remarks>The returned offers are ordered with featured offers first, followed by the most
        /// recently created offers. The search is case-insensitive and matches across multiple offer and vehicle
        /// fields.</remarks>
        /// <param name="searchTerm">An optional search term used to filter offers by title, description, location, or vehicle attributes. If
        /// null or empty, all active offers are returned.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of active vehicle offers
        /// matching the search criteria. The list is empty if no offers match.</returns>
        public async Task<IList<VehicleOffer>> GetActiveOffersAsync(string? searchTerm = null)
        {
            var query = GetBaseOffersQuery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var normalizedSearchTerm = searchTerm.Trim().ToLower();

                query = query.Where(o =>
                    o.Title.ToLower().Contains(normalizedSearchTerm) ||
                    o.Description.ToLower().Contains(normalizedSearchTerm) ||
                    (o.Location != null && o.Location.ToLower().Contains(normalizedSearchTerm)) ||
                    o.Vehicle.Model.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.Brand.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.VehicleCategory.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.FuelType.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.TransmissionType.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.BodyType.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.VehicleColour.ColourName.ToLower().Contains(normalizedSearchTerm));
            }

            var offers = await query
                .OrderByDescending(o => o.IsFeatured)
                .ThenByDescending(o => o.CreatedAt)
                .ToListAsync();

            return offers;
        }

        /// <summary>
        /// Asynchronously retrieves a list of the most recently created featured vehicle offers.
        /// </summary>
        /// <param name="count">The maximum number of featured offers to return. Must be greater than zero.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of featured vehicle
        /// offers, ordered by creation date in descending order. The list may be empty if no featured offers are
        /// available.</returns>
        public async Task<IList<VehicleOffer>> GetFeaturedOffersAsync(int count)
        {
            var offers = await GetBaseOffersQuery()
                .Where(o => o.IsFeatured)
                .OrderByDescending(o => o.CreatedAt)
                .Take(count)
                .ToListAsync();

            return offers;
        }

        /// <summary>
        /// Asynchronously retrieves a list of vehicle offers that belong to the specified vehicle category and
        /// optionally match a search term.
        /// </summary>
        /// <remarks>The returned offers are ordered with featured offers first, followed by the most
        /// recently created offers. The search is case-insensitive and matches across multiple offer and vehicle
        /// fields.</remarks>
        /// <param name="vehicleCategoryId">The unique identifier of the vehicle category to filter offers by.</param>
        /// <param name="searchTerm">An optional search term to filter offers by title, description, location, or related vehicle attributes. If
        /// null or empty, no search filtering is applied.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of vehicle offers
        /// matching the specified category and search criteria. The list is empty if no offers are found.</returns>
        public async Task<IList<VehicleOffer>> GetOffersByCategoryAsync(int vehicleCategoryId, string? searchTerm = null)
        {
            var query = GetBaseOffersQuery()
                .Where(o => o.Vehicle.VehicleCategoryId == vehicleCategoryId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var normalizedSearchTerm = searchTerm.Trim().ToLower();

                query = query.Where(o =>
                    o.Title.ToLower().Contains(normalizedSearchTerm) ||
                    o.Description.ToLower().Contains(normalizedSearchTerm) ||
                    (o.Location != null && o.Location.ToLower().Contains(normalizedSearchTerm)) ||
                    o.Vehicle.Model.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.Brand.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.VehicleCategory.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.FuelType.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.TransmissionType.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.BodyType.Name.ToLower().Contains(normalizedSearchTerm) ||
                    o.Vehicle.VehicleColour.ColourName.ToLower().Contains(normalizedSearchTerm));
            }

            var offers = await query
                .OrderByDescending(o => o.IsFeatured)
                .ThenByDescending(o => o.CreatedAt)
                .ToListAsync();

            return offers;
        }

        /// <summary>
        /// Asynchronously retrieves the details of a vehicle offer by its unique identifier.
        /// </summary>
        /// <param name="vehicleOfferId">The unique identifier of the vehicle offer to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="VehicleOffer"/>
        /// if found; otherwise, <see langword="null"/>.</returns>
        public async Task<VehicleOffer?> GetOfferDetailsAsync(int vehicleOfferId)
        {
            var offer = await GetBaseOffersQuery()
                .FirstOrDefaultAsync(o => o.VehicleOfferId == vehicleOfferId);

            return offer;
        }
        /// <summary>
        /// Creates a query for retrieving active vehicle offers with related vehicle and image data included.
        /// </summary>
        /// <remarks>The returned query is configured for read-only access and includes all relevant
        /// navigation properties for each offer. The query is not executed until enumerated, allowing for further
        /// composition or filtering.</remarks>
        /// <returns>An <see cref="IQueryable{VehicleOffer}"/> representing active vehicle offers, including related vehicle,
        /// brand, category, fuel type, transmission type, body type, color, and images.</returns>
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
