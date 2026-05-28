using PrestivaCars.Data.Data.Vehicles;

namespace PrestivaCars.Interfaces.Vehicles
{
    /// <summary>
    /// Defines methods for retrieving and managing vehicle offers, including active, featured, and category-specific
    /// offers, as well as offer details.
    /// </summary>
    /// <remarks>Implementations of this interface provide asynchronous operations for accessing vehicle offer
    /// data, supporting scenarios such as listing available offers or retrieving detailed information for a specific
    /// offer. Methods return collections or details of offers, and may return empty collections or null if no matching
    /// offers are found.</remarks>
    public interface IVehicleOfferService
    {
        Task<IList<VehicleOffer>> GetActiveOffersAsync();                                  
        Task<IList<VehicleOffer>> GetFeaturedOffersAsync(int count);
        Task<IList<VehicleOffer>> GetOffersByCategoryAsync(int vehicleCategoryId);
        Task<VehicleOffer?> GetOfferDetailsAsync(int id);
    }
}
