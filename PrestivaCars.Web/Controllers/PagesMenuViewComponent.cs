using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.CMS;

namespace PrestivaCars.Web.Controllers
{
    /// <summary>
    /// Represents a view component that renders a navigation menu based on available pages.
    /// </summary>
    /// <remarks>This view component retrieves navigation pages using the provided IPageService and renders
    /// them using the "Default" view. It is typically used to display a dynamic menu of pages in a layout or partial
    /// view.</remarks>
    public class PagesMenuViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public PagesMenuViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await _pageService.GetNavigationPagesAsync();

            return View("Default", pages);
        }
    }
}