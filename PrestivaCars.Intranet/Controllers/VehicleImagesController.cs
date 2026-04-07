using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Catalog;

namespace PrestivaCars.Intranet.Controllers
{
    public class VehicleImagesController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public VehicleImagesController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: VehicleImages
        public async Task<IActionResult> Index()
        {
            var prestivaCarsContext = _context.VehicleImages.Include(v => v.VehicleOffer);
            return View(await prestivaCarsContext.ToListAsync());
        }

        // GET: VehicleImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleImage = await _context.VehicleImages
                .Include(v => v.VehicleOffer)
                .FirstOrDefaultAsync(m => m.VehicleImageId == id);
            if (vehicleImage == null)
            {
                return NotFound();
            }

            return View(vehicleImage);
        }

        // GET: VehicleImages/Create
        public IActionResult Create()
        {
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy");
            return View();
        }

        // POST: VehicleImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleImageId,ImagePath,AltText,Position,IsMain,VehicleOfferId,IsActive")] VehicleImage vehicleImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", vehicleImage.VehicleOfferId);
            return View(vehicleImage);
        }

        // GET: VehicleImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleImage = await _context.VehicleImages.FindAsync(id);
            if (vehicleImage == null)
            {
                return NotFound();
            }
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", vehicleImage.VehicleOfferId);
            return View(vehicleImage);
        }

        // POST: VehicleImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleImageId,ImagePath,AltText,Position,IsMain,VehicleOfferId,IsActive")] VehicleImage vehicleImage)
        {
            if (id != vehicleImage.VehicleImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleImageExists(vehicleImage.VehicleImageId))
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
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", vehicleImage.VehicleOfferId);
            return View(vehicleImage);
        }

        // GET: VehicleImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleImage = await _context.VehicleImages
                .Include(v => v.VehicleOffer)
                .FirstOrDefaultAsync(m => m.VehicleImageId == id);
            if (vehicleImage == null)
            {
                return NotFound();
            }

            return View(vehicleImage);
        }

        // POST: VehicleImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleImage = await _context.VehicleImages.FindAsync(id);
            if (vehicleImage != null)
            {
                _context.VehicleImages.Remove(vehicleImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleImageExists(int id)
        {
            return _context.VehicleImages.Any(e => e.VehicleImageId == id);
        }
    }
}
