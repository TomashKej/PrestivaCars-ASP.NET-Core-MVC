using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.Vehicles;

namespace PrestivaCars.Web.Controllers
{
    public class VehicleCategoriesMenuViewComponent : ViewComponent
    {
        private readonly IVehicleCategoryService _vehicleCategoryService;

        public VehicleCategoriesMenuViewComponent(IVehicleCategoryService vehicleCategoryService)
        {
            _vehicleCategoryService = vehicleCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _vehicleCategoryService.GetActiveCategoriesAsync();

            return View("Default", categories);
        }
    }
}