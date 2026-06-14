using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.CMS;

namespace PrestivaCars.Web.Controllers
{
    /// <summary>
    /// Provides actions for displaying content pages based on their slug identifiers.
    /// </summary>
    /// <remarks>This controller is intended for serving static or CMS-driven pages by resolving a page slug
    /// to its corresponding content. It relies on an injected IPageService to retrieve page data. The controller is
    /// typically used in scenarios where pages are managed dynamically and accessed via user-friendly URLs.</remarks>
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        // GET: Pages/Details/about-us
        [HttpGet("pages/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var page = await _pageService.GetPageBySlugAsync(slug);

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
    }
}