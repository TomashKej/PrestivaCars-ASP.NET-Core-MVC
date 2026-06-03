using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Web.Models;
using System.Diagnostics;

namespace PrestivaCars.Web.Controllers
{
    /// <summary>
    /// Represents the controller responsible for handling requests to the application's home pages, including the main
    /// landing page, privacy policy, and error display.
    /// </summary>
    /// <remarks>This controller provides actions for rendering the application's home-related views. It
    /// retrieves featured vehicle offers and active vehicle categories for the main page and displays privacy and error
    /// information as needed. The controller is intended to be used as the entry point for users visiting the root or
    /// informational pages of the application.</remarks>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleOfferService _vehicleOfferService;
        private readonly IVehicleCategoryService _vehicleCategoryService;

        public HomeController(
            ILogger<HomeController> logger,
            IVehicleOfferService vehicleOfferService,
            IVehicleCategoryService vehicleCategoryService)
        {
            _logger = logger;
            _vehicleOfferService = vehicleOfferService;
            _vehicleCategoryService = vehicleCategoryService;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            ViewBag.FeaturedOffers = await _vehicleOfferService.GetFeaturedOffersAsync(3);
            ViewBag.VehicleCategories = await _vehicleCategoryService.GetActiveCategoriesAsync();

            return View();
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}