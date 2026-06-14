using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Interfaces.CMS;

namespace PrestivaCars.Web.ViewComponents
{
    /// <summary>
    /// Represents a view component for rendering the navigation bar in the PrestivaCars web application.
    /// </summary>
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavbarViewComponent"/> class.
        /// </summary>
        /// <param name="pageService">The page service used to retrieve navigation pages.</param>
        public NavbarViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        /// <summary>
        /// Invokes the view component asynchronously to retrieve navigation pages and render the default view.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await _pageService.GetNavigationPagesAsync();

            return View("Default", pages);
        }
    }
}