using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Vehicles;

namespace PrestivaCars.Intranet.Controllers
{
    public class VehicleOffersController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public VehicleOffersController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: VehicleOffers
        public async Task<IActionResult> Index()
        {
            var prestivaCarsContext = _context.VehicleOffers.Include(v => v.Vehicle);
            return View(await prestivaCarsContext.ToListAsync());
        }

        // GET: VehicleOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.VehicleOfferId == id);
            if (vehicleOffer == null)
            {
                return NotFound();
            }

            return View(vehicleOffer);
        }

        // GET: VehicleOffers/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Brand");
            return View();
        }

        // POST: VehicleOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleOfferId,Title,Slug,Description,Price,Location,IsFeatured,VehicleId,IsActive")] VehicleOffer vehicleOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Brand", vehicleOffer.VehicleId);
            return View(vehicleOffer);
        }

        // GET: VehicleOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers.FindAsync(id);
            if (vehicleOffer == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Brand", vehicleOffer.VehicleId);
            return View(vehicleOffer);
        }

        // POST: VehicleOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleOfferId,Title,Slug,Description,Price,Location,IsFeatured,VehicleId,IsActive")] VehicleOffer vehicleOffer)
        {
            if (id != vehicleOffer.VehicleOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleOfferExists(vehicleOffer.VehicleOfferId))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Brand", vehicleOffer.VehicleId);
            return View(vehicleOffer);
        }

        // GET: VehicleOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOffer = await _context.VehicleOffers
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.VehicleOfferId == id);
            if (vehicleOffer == null)
            {
                return NotFound();
            }

            return View(vehicleOffer);
        }

        // POST: VehicleOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleOffer = await _context.VehicleOffers.FindAsync(id);
            if (vehicleOffer != null)
            {
                _context.VehicleOffers.Remove(vehicleOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleOfferExists(int id)
        {
            return _context.VehicleOffers.Any(e => e.VehicleOfferId == id);
        }
    }
}
