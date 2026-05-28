using PrestivaCars.Data.Data.Vehicles;

namespace PrestivaCars.Interfaces.Vehicles
{
    /// <summary>
    /// Defines operations for retrieving vehicle category information, including active categories and category details
    /// by identifier.
    /// </summary>
    public interface IVehicleCategoryService
    {
        Task<IList<VehicleCategory>> GetActiveCategoriesAsync();
        Task<VehicleCategory?> GetCategoryByIdAsync(int vehicleCategoryId);
    }
}
