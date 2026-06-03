using PrestivaCars.Data.Data.CMS;

namespace PrestivaCars.Interfaces.CMS
{
    /// <summary>
    /// Defines methods for retrieving pages and navigation data asynchronously.
    /// </summary>
    /// <remarks>Implementations of this interface provide access to page data, such as retrieving navigation
    /// structures or looking up pages by their unique slug. Methods are asynchronous and intended for use in scenarios
    /// where page data may be loaded from a database, API, or other external source.</remarks>
    public interface IPageService
    {
        Task<IList<Page>> GetNavigationPagesAsync();
        Task<Page?> GetPageBySlugAsync(string slug);
    }
}
