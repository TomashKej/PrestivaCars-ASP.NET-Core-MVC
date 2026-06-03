using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.CMS;
using PrestivaCars.Interfaces.CMS;
using PrestivaCars.Services.Abstraction;

namespace PrestivaCars.Services.CMS
{
    /// <summary>
    /// Provides operations for retrieving and managing page data, including navigation pages and individual pages by
    /// slug.
    /// </summary>
    /// <remarks>This service is intended for use in scenarios where page content needs to be accessed or
    /// displayed, such as building navigation menus or rendering specific pages. All methods are asynchronous and
    /// interact with the underlying data context in a read-only manner.</remarks>
    public class PageService : BaseService, IPageService
    {
        public PageService(PrestivaCarsContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Asynchronously retrieves a list of active pages that are configured to appear in the navigation menu.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of pages to display in
        /// navigation, ordered by position and title. The list is empty if no such pages exist.</returns>
        public async Task<IList<Page>> GetNavigationPagesAsync()
        {
            var pages = await _context.Pages
                .AsNoTracking()
                .Where(p => p.IsActive && p.ShowInNavigation)
                .OrderBy(p => p.Position)
                .ThenBy(p => p.Title)
                .ToListAsync();

            return pages;
        }

        /// <summary>
        /// Asynchronously retrieves the active page that matches the specified slug.
        /// </summary>
        /// <remarks>The search is case-sensitive and only considers pages marked as active.</remarks>
        /// <param name="slug">The unique slug identifier of the page to retrieve. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the matching active page if
        /// found; otherwise, null.</returns>
        public async Task<Page?> GetPageBySlugAsync(string slug)
        {
            var page = await _context.Pages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IsActive && p.Slug == slug);

            return page;
        }
    }
}