using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.CMS;
using PrestivaCars.Interfaces.CMS;
using PrestivaCars.Services.Abstraction;

namespace PrestivaCars.Services.CMS
{
    /// <summary>
    /// Service for managing banners in the CMS, providing methods to retrieve banners by placement key.
    /// </summary>
    public class BannerService : BaseService, IBannerService
    {
        public BannerService(PrestivaCarsContext context)
            : base(context)
        {
        }

        public async Task<Banner?> GetBannerByPlacementKeyAsync(string placementKey)
        {
            return await _context.Banners
                .AsNoTracking()
                .Where(b => b.IsActive && b.PlacementKey == placementKey)
                .OrderBy(b => b.Position)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<Banner>> GetBannersByPlacementKeyAsync(string placementKey)
        {
            return await _context.Banners
                .AsNoTracking()
                .Where(b => b.IsActive && b.PlacementKey == placementKey)
                .OrderBy(b => b.Position)
                .ThenBy(b => b.Title)
                .ToListAsync();
        }
    }
}