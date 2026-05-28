using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Vehicles;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Services.Abstraction;

namespace PrestivaCars.Services.Vehicles
{
    /// <summary>
    /// Provides operations for retrieving and managing vehicle categories within the application.
    /// </summary>
    /// <remarks>This service offers methods to access active vehicle categories and retrieve category details
    /// by identifier. It is intended to be used as part of the application's business logic layer for vehicle category
    /// management.</remarks>
    public class VehicleCategoryService : BaseService, IVehicleCategoryService
    {
        public VehicleCategoryService(PrestivaCarsContext context)
            : base(context)
        { 
        }

        public async Task<IList<VehicleCategory>> GetActiveCategoriesAsync()
        {
            var categories = await _context.VehicleCategories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.Position)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<VehicleCategory?> GetCategoryByIdAsync(int vehicleCategoryId)
        {
            var category = await _context.VehicleCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IsActive && c.VehicleCategoryId == vehicleCategoryId);

            return category;
        }
    }
}
