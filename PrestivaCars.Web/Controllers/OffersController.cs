using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.Vehicles;

namespace PrestivaCars.Web.Controllers
{
    /// <summary>
    /// Provides actions for displaying vehicle offers and offer details to users.
    /// </summary>
    /// <remarks>This controller handles requests related to viewing active vehicle offers, browsing offers by
    /// category, and viewing details for a specific offer. It relies on injected services to retrieve offer and
    /// category data. All actions return views suitable for rendering in an MVC application.</remarks>
    public class OffersController : Controller
    {
        private readonly IVehicleOfferService _vehicleOfferService;
        private readonly IVehicleCategoryService _vehicleCategoryService;

        public OffersController(
            IVehicleOfferService vehicleOfferService,
            IVehicleCategoryService vehicleCategoryService)
        {
            _vehicleOfferService = vehicleOfferService;
            _vehicleCategoryService = vehicleCategoryService;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var offers = await _vehicleOfferService.GetActiveOffersAsync(searchTerm);

            ViewBag.SearchTerm = searchTerm;

            return View(offers);
        }

        public async Task<IActionResult> Category(int id, string? searchTerm)
        {
            var category = await _vehicleCategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = id;
            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryDescription = category.Description;
            ViewBag.SearchTerm = searchTerm;

            var offers = await _vehicleOfferService.GetOffersByCategoryAsync(id, searchTerm);

            return View(offers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var offer = await _vehicleOfferService.GetOfferDetailsAsync(id);

            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }
    }
}