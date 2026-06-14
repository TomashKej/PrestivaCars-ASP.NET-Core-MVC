using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Data.Data.CMS;
using PrestivaCars.Interfaces.CMS;

namespace PrestivaCars.Web.ViewComponents
{
    /// <summary>
    /// Represents a view component for rendering the footer in the PrestivaCars web application.
    /// </summary>
    public class FooterViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FooterViewComponent"/> class.
        /// </summary>
        /// <param name="pageService"></param>
        public FooterViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        /// <summary>
        /// Invokes the view component asynchronously to render the footer content.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerAbout = await _pageService.GetPageBySlugAsync("footer-about");

            var footerPages = new List<Page>();

            var footerSlugs = new[]
            {
                "about-us",
                "faq",
                "privacy-policy",
                "terms-and-conditions",
                "cookie-policy"
            };

            foreach (var slug in footerSlugs)
            {
                var page = await _pageService.GetPageBySlugAsync(slug);

                if (page != null)
                {
                    footerPages.Add(page);
                }
            }

            ViewBag.FooterAbout = footerAbout;

            return View("Default", footerPages);
        }
    }
}