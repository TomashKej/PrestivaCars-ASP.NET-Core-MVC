using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Interfaces.CMS;

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
        private readonly IBannerService _bannerService;

        public OffersController(
            IVehicleOfferService vehicleOfferService,
            IVehicleCategoryService vehicleCategoryService,
            IBannerService bannerService)
        {
            _vehicleOfferService = vehicleOfferService;
            _vehicleCategoryService = vehicleCategoryService;
            _bannerService = bannerService;
        }

        // GET: Offers/Index
        public async Task<IActionResult> Index(string? searchTerm)
        {
            searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();

            var offers = await _vehicleOfferService.GetActiveOffersAsync(searchTerm);

            ViewBag.HeroBanner = await _bannerService.GetBannerByPlacementKeyAsync("offers-index-hero");
            ViewBag.SearchTerm = searchTerm;

            return View(offers);
        }

        // GET: Offers/Category/5
        public async Task<IActionResult> Category(int id, string? searchTerm)
        {
            searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();

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

            // Try to get a category-specific banner, if not found, fall back to a generic offers category banner
            var categoryBanner = await _bannerService.GetBannerByPlacementKeyAsync($"category-{id}-hero");

            if (categoryBanner == null)
            {
                categoryBanner = await _bannerService.GetBannerByPlacementKeyAsync("offers-category-hero");
            }

            ViewBag.HeroBanner = categoryBanner;

            return View("Index", offers);
        }

        // GET: Offers/Details/5
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