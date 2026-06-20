using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Catalog;
using PrestivaCars.Data.Data.Vehicles;

namespace PrestivaCars.Intranet.Controllers
{
    /// <summary>
    /// Provides actions for managing vehicle offers, including listing, viewing details, creating, editing, and
    /// deleting offers in the application.
    /// </summary>
    /// <remarks>This controller is intended for use within an ASP.NET Core MVC application and interacts with
    /// the application's data context to perform CRUD operations on vehicle offers. All actions require appropriate
    /// user permissions as configured in the application. The controller uses asynchronous patterns for database access
    /// to improve scalability.</remarks>
    public class VehicleOffersController : Controller
    {
        private readonly PrestivaCarsContext _context;
        private readonly IConfiguration _configuration; // Configuration for accessing application settings, such as file paths and other configurable parameters.

        public VehicleOffersController(PrestivaCarsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: VehicleOffers
        public async Task<IActionResult> Index()
        {
            var vehicleOffers = await _context.VehicleOffers
                .AsNoTracking()
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.Brand)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.VehicleCategory)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return View(vehicleOffers);
        }

        // GET: VehicleOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers
                .AsNoTracking()
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
                .Include(o => o.VehicleImages)
                .FirstOrDefaultAsync(o => o.VehicleOfferId == id);

            if (vehicleOffer == null)
            {
                return NotFound();
            }

            // Pass the portal base URL to the view for constructing full image URLs
            ViewBag.PortalBaseUrl = GetPortalBaseUrl();

            return View(vehicleOffer);
        }

        // GET: VehicleOffers/Create
        public async Task<IActionResult> Create()
        {
            await PopulateVehiclesDropDownListAsync();

            var vehicleOffer = new VehicleOffer
            {
                Title = string.Empty,
                Slug = string.Empty,
                Description = string.Empty,
                Location = "London",
                IsFeatured = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Admin"
            };

            return View(vehicleOffer);
        }

        // POST: VehicleOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Slug,Description,Price,Location,IsFeatured,VehicleId,IsActive")]
            VehicleOffer vehicleOffer,
            IFormFile? mainImageFile,
            string? mainImageAltText)
        {
            if (string.IsNullOrWhiteSpace(vehicleOffer.Slug))
            {
                vehicleOffer.Slug = GenerateSlug(vehicleOffer.Title);
                ModelState.Remove(nameof(VehicleOffer.Slug));
            }

            ModelState.Remove("Vehicle");
            ModelState.Remove("VehicleImages");
            ModelState.Remove("VehicleOfferFeatures");
            ModelState.Remove("SavedOffers");

            var imageValidationMessage = ValidateOfferImage(mainImageFile);

            if (!string.IsNullOrWhiteSpace(imageValidationMessage))
            {
                ModelState.AddModelError("mainImageFile", imageValidationMessage);
            }

            if (ModelState.IsValid)
            {
                vehicleOffer.CreatedAt = DateTime.UtcNow;
                vehicleOffer.CreatedBy = "Admin";

                _context.VehicleOffers.Add(vehicleOffer);
                await _context.SaveChangesAsync();

                if (mainImageFile != null && mainImageFile.Length > 0)
                {
                    var imagePath = await SaveOfferImageAsync(mainImageFile, vehicleOffer.Slug);

                    var vehicleImage = new VehicleImage
                    {
                        ImagePath = imagePath,
                        AltText = string.IsNullOrWhiteSpace(mainImageAltText)
                            ? vehicleOffer.Title
                            : mainImageAltText,
                        Position = 1,
                        IsMain = true,
                        VehicleOfferId = vehicleOffer.VehicleOfferId,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin"
                    };

                    _context.VehicleImages.Add(vehicleImage);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateVehiclesDropDownListAsync(vehicleOffer.VehicleId);

            return View(vehicleOffer);
        }

        // GET: VehicleOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers
                .Include(o => o.VehicleImages)
                .FirstOrDefaultAsync(o => o.VehicleOfferId == id);

            if (vehicleOffer == null)
            {
                return NotFound();
            }

            var mainImage = vehicleOffer.VehicleImages
                .Where(i => i.IsActive)
                .OrderByDescending(i => i.IsMain)
                .ThenBy(i => i.Position)
                .FirstOrDefault();

            ViewBag.MainImagePath = mainImage?.ImagePath ?? string.Empty;
            ViewBag.MainImageAltText = mainImage?.AltText ?? string.Empty;
            ViewBag.PortalBaseUrl = GetPortalBaseUrl();

            await PopulateVehiclesDropDownListAsync(vehicleOffer.VehicleId);

            return View(vehicleOffer);
        }

        // POST: VehicleOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("VehicleOfferId,Title,Slug,Description,Price,Location,IsFeatured,VehicleId,IsActive")]
            VehicleOffer vehicleOffer,
            string? mainImagePath,
            string? mainImageAltText,
            IFormFile? mainImageFile)
        {
            if (id != vehicleOffer.VehicleOfferId)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(vehicleOffer.Slug))
            {
                vehicleOffer.Slug = GenerateSlug(vehicleOffer.Title);
                ModelState.Remove(nameof(VehicleOffer.Slug));
            }

            ModelState.Remove(nameof(VehicleOffer.Vehicle));
            ModelState.Remove(nameof(VehicleOffer.SavedOffers));
            ModelState.Remove(nameof(VehicleOffer.VehicleImages));

            var imageValidationMessage = ValidateOfferImage(mainImageFile);

            if (!string.IsNullOrWhiteSpace(imageValidationMessage))
            {
                ModelState.AddModelError("mainImageFile", imageValidationMessage);
            }

            if (ModelState.IsValid)
            {
                var offerFromDb = await _context.VehicleOffers
                    .Include(o => o.VehicleImages)
                    .FirstOrDefaultAsync(o => o.VehicleOfferId == id);

                if (offerFromDb == null)
                {
                    return NotFound();
                }

                offerFromDb.Title = vehicleOffer.Title;
                offerFromDb.Slug = vehicleOffer.Slug;
                offerFromDb.Description = vehicleOffer.Description;
                offerFromDb.Price = vehicleOffer.Price;
                offerFromDb.Location = vehicleOffer.Location;
                offerFromDb.IsFeatured = vehicleOffer.IsFeatured;
                offerFromDb.VehicleId = vehicleOffer.VehicleId;
                offerFromDb.IsActive = vehicleOffer.IsActive;
                offerFromDb.UpdatedAt = DateTime.UtcNow;
                offerFromDb.UpdatedBy = "Admin";

                await UpdateMainOfferImageAsync(
                    offerFromDb,
                    mainImagePath,
                    mainImageAltText,
                    mainImageFile
                );

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MainImagePath = mainImagePath ?? string.Empty;
            ViewBag.MainImageAltText = mainImageAltText ?? string.Empty;
            ViewBag.PortalBaseUrl = GetPortalBaseUrl();

            await PopulateVehiclesDropDownListAsync(vehicleOffer.VehicleId);

            return View(vehicleOffer);
        }
        // GET: VehicleOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers
                .AsNoTracking()
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.Brand)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.VehicleCategory)
                .FirstOrDefaultAsync(o => o.VehicleOfferId == id && o.IsActive);

            if (vehicleOffer == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModal", vehicleOffer);
        }

        // POST: VehicleOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleOffer = await _context.VehicleOffers.FindAsync(id);

            if (vehicleOffer != null)
            {
                vehicleOffer.IsActive = false;
                vehicleOffer.DeletedAt = DateTime.UtcNow;
                vehicleOffer.DeletedBy = "Admin";

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete(List<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var offers = await _context.VehicleOffers
                .Where(o => selectedIds.Contains(o.VehicleOfferId) && o.IsActive)
                .ToListAsync();

            foreach (var offer in offers)
            {
                offer.IsActive = false;
                offer.DeletedAt = DateTime.UtcNow;
                offer.DeletedBy = "Admin";
            }

            if (offers.Any())
            {
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleOffers/Restore/5
        [HttpGet]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers
                .AsNoTracking()
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.Brand)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.VehicleCategory)
                .FirstOrDefaultAsync(o => o.VehicleOfferId == id && !o.IsActive);

            if (vehicleOffer == null)
            {
                return NotFound();
            }

            return PartialView("_RestoreModal", vehicleOffer);
        }

        // POST: VehicleOffers/Restore/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var vehicleOffer = await _context.VehicleOffers.FindAsync(id);

            if (vehicleOffer != null && !vehicleOffer.IsActive)
            {
                vehicleOffer.IsActive = true;
                vehicleOffer.DeletedAt = null;
                vehicleOffer.DeletedBy = string.Empty;
                vehicleOffer.UpdatedAt = DateTime.UtcNow;
                vehicleOffer.UpdatedBy = "Admin";

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: VehicleOffers/BulkRestore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkRestore(List<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var offers = await _context.VehicleOffers
                .Where(o => selectedIds.Contains(o.VehicleOfferId) && !o.IsActive)
                .ToListAsync();

            foreach (var offer in offers)
            {
                offer.IsActive = true;
                offer.DeletedAt = null;
                offer.DeletedBy = string.Empty;
                offer.UpdatedAt = DateTime.UtcNow;
                offer.UpdatedBy = "Admin";
            }

            if (offers.Any())
            {
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleOffers/PopulateVehiclesDropDownList
        private async Task PopulateVehiclesDropDownListAsync(int? selectedVehicleId = null)
        {
            var vehicles = await _context.Vehicles
                .AsNoTracking()
                .Include(v => v.Brand)
                .Include(v => v.VehicleCategory)
                .Where(v => v.IsActive)
                .OrderBy(v => v.Brand.Name)
                .ThenBy(v => v.Model)
                .Select(v => new
                {
                    v.VehicleId,
                    DisplayName = v.Brand.Name + " " + v.Model + " (" + v.ProductionYear + ") - " + v.VehicleCategory.Name
                })
                .ToListAsync();

            ViewData["VehicleId"] = new SelectList(vehicles, "VehicleId", "DisplayName", selectedVehicleId);
        }

        /// <summary>
        /// Generates a URL-friendly slug from the specified string.
        /// </summary>
        /// <remarks>The generated slug is lowercase, with spaces and slashes replaced by hyphens, and
        /// periods and commas removed.</remarks>
        /// <param name="value">The input string to convert to a slug. Can be null or whitespace.</param>
        /// <returns>A slugified version of the input string, suitable for use in URLs. Returns an empty string if the input is
        /// null or whitespace.</returns>
        private static string GenerateSlug(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value
                .Trim()
                .ToLower()
                .Replace(" ", "-")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("/", "-");
        }

        /// <summary>
        /// Validates an uploaded image file for offer submissions, checking file size and allowed formats.
        /// </summary>
        /// <param name="imageFile">The image file to validate. Can be null. Only files with .jpg, .jpeg, .png, or .webp extensions and a
        /// maximum size of 5 MB are considered valid.</param>
        /// <returns>A string containing an error message if the image file is invalid; otherwise, null if the file passes all
        /// validation checks.</returns>
        private static string? ValidateOfferImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            const long maxFileSize = 5 * 1024 * 1024;

            if (imageFile.Length > maxFileSize)
            {
                return "The image file cannot be larger than 5 MB.";
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return "Only JPG, JPEG, PNG and WEBP files are allowed.";
            }

            return null;
        }

        /// <summary>
        /// Saves the specified offer image to the server and returns the relative URL path to the saved image.
        /// </summary>
        /// <remarks>The method creates the target directory if it does not exist. The file name is
        /// generated using the provided slug and a unique identifier to prevent naming conflicts.</remarks>
        /// <param name="imageFile">The image file to be saved. Must be a valid, non-null file representing the offer image.</param>
        /// <param name="offerSlug">The slug used to generate a unique and descriptive file name for the image. If null or whitespace, a default
        /// value is used.</param>
        /// <returns>A relative URL path to the saved offer image file, suitable for use in web content.</returns>
        private async Task<string> SaveOfferImageAsync(IFormFile imageFile, string offerSlug)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            var safeSlug = string.IsNullOrWhiteSpace(offerSlug)
                ? "vehicle-offer"
                : GenerateSlug(offerSlug);

            var fileName = $"{safeSlug}-{Guid.NewGuid():N}{extension}";

            var currentProjectPath = Directory.GetCurrentDirectory();
            var solutionPath = Directory.GetParent(currentProjectPath)?.FullName ?? currentProjectPath;

            var webOffersDirectory = Path.Combine(solutionPath,"PrestivaCars.Web", "wwwroot", "content", "offers");

            Directory.CreateDirectory(webOffersDirectory);

            var filePath = Path.Combine(webOffersDirectory, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return $"/content/offers/{fileName}";
        }

        /// <summary>
        /// Updates the main image of a vehicle offer, either by saving a new uploaded image or updating the existing main image's path and alt text.
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="mainImagePath"></param>
        /// <param name="mainImageAltText"></param>
        /// <param name="mainImageFile"></param>
        /// <returns></returns>
        private async Task UpdateMainOfferImageAsync(VehicleOffer offer, string? mainImagePath, string? mainImageAltText, IFormFile? mainImageFile)
        {
            if (mainImageFile != null && mainImageFile.Length > 0)
            {
                mainImagePath = await SaveOfferImageAsync(mainImageFile, offer.Slug);
            }

            if (string.IsNullOrWhiteSpace(mainImagePath))
            {
                return;
            }

            mainImagePath = NormalizeImagePath(mainImagePath);

            var currentMainImage = offer.VehicleImages
                .Where(i => i.IsActive)
                .OrderByDescending(i => i.IsMain)
                .ThenBy(i => i.Position)
                .FirstOrDefault();

            foreach (var image in offer.VehicleImages)
            {
                image.IsMain = false;
            }

            if (currentMainImage == null)
            {
                var newImage = new VehicleImage
                {
                    ImagePath = mainImagePath,
                    AltText = string.IsNullOrWhiteSpace(mainImageAltText)
                        ? offer.Title
                        : mainImageAltText,
                    Position = 1,
                    IsMain = true,
                    VehicleOfferId = offer.VehicleOfferId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Admin"
                };

                _context.VehicleImages.Add(newImage);
            }
            else
            {
                currentMainImage.ImagePath = mainImagePath;
                currentMainImage.AltText = string.IsNullOrWhiteSpace(mainImageAltText)
                    ? offer.Title
                    : mainImageAltText;
                currentMainImage.Position = 1;
                currentMainImage.IsMain = true;
                currentMainImage.IsActive = true;
                currentMainImage.UpdatedAt = DateTime.UtcNow;
                currentMainImage.UpdatedBy = "Admin";
            }
        }

        /// <summary>
        /// Normalizes the image path to ensure it is in a consistent format for use in the application.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private static string NormalizeImagePath(string imagePath)
        {
            imagePath = imagePath.Trim();

            if (imagePath.StartsWith("~/"))
            {
                imagePath = imagePath.Replace("~/", "/");
            }

            if (!imagePath.StartsWith("/") &&
                !imagePath.StartsWith("http://") &&
                !imagePath.StartsWith("https://"))
            {
                imagePath = "/" + imagePath;
            }

            return imagePath;
        }

        /// <summary>
        /// Retrieves the base URL of the portal from the application configuration settings.
        /// </summary>
        /// <returns></returns>
        private string GetPortalBaseUrl()
        {
            return _configuration["PortalSettings:PortalBaseUrl"]?.TrimEnd('/') ?? string.Empty;
        }

    }
}