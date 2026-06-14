using PrestivaCars.Data.Data.CMS;

namespace PrestivaCars.Interfaces.CMS
{
    /// <summary>
    /// Interface for banner service operations.
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Gets a list of banners by placement key asynchronously.
        /// </summary>
        /// <param name="placementKey"></param>
        /// <returns></returns>
        Task<IList<Banner>> GetBannersByPlacementKeyAsync(string placementKey);

        /// <summary>
        /// Gets a single banner by placement key asynchronously.
        /// </summary>
        /// <param name="placementKey"></param>
        /// <returns></returns>
        Task<Banner?> GetBannerByPlacementKeyAsync(string placementKey);
    }
}
