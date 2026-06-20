using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.CMS;

namespace PrestivaCars.Intranet.Controllers
{
    public class BannersController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public BannersController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banners.ToListAsync());
        }

        // GET: Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.BannerId == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerId,PlacementKey,CompanyNameLabel,Title,Subtitle,ButtonText,ButtonUrl,ImagePath,Position,IsActive")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerId,PlacementKey,CompanyNameLabel,Title,Subtitle,ButtonText,ButtonUrl,ImagePath,Position,IsActive")] Banner banner)
        {
            if (id != banner.BannerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.BannerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.BannerId == id && m.IsActive);

            if (banner == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModal", banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners.FindAsync(id);

            if (banner != null)
            {
                _context.Banners.Remove(banner);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Banners/BulkDelete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete(List<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var banners = await _context.Banners
                .Where(b => selectedIds.Contains(b.BannerId) && b.IsActive)
                .ToListAsync();

            if (banners.Any())
            {
                _context.Banners.RemoveRange(banners);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Banners/Restore/5
        [HttpGet]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(b => b.BannerId == id && !b.IsActive);

            if (banner == null)
            {
                return NotFound();
            }

            return PartialView("_RestoreModal", banner);
        }

        // POST: Banners/Restore/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var banner = await _context.Banners.FindAsync(id);

            if (banner != null && !banner.IsActive)
            {
                banner.IsActive = true;
                banner.DeletedAt = null;
                banner.DeletedBy = string.Empty;
                banner.UpdatedAt = DateTime.Now;
                banner.UpdatedBy = "System";

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Banners/BulkRestore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkRestore(List<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var banners = await _context.Banners
                .Where(b => selectedIds.Contains(b.BannerId) && !b.IsActive)
                .ToListAsync();

            foreach (var banner in banners)
            {
                banner.IsActive = true;
                banner.DeletedAt = null;
                banner.DeletedBy = string.Empty;
                banner.UpdatedAt = DateTime.Now;
                banner.UpdatedBy = "System";
            }

            if (banners.Any())
            {
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.BannerId == id);
        }
    }
}
