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
    public class SavedOffersController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public SavedOffersController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: SavedOffers
        public async Task<IActionResult> Index()
        {
            var prestivaCarsContext = _context.SavedOffers.Include(s => s.VehicleOffer);
            return View(await prestivaCarsContext.ToListAsync());
        }

        // GET: SavedOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedOffer = await _context.SavedOffers
                .Include(s => s.VehicleOffer)
                .FirstOrDefaultAsync(m => m.SavedOfferId == id);
            if (savedOffer == null)
            {
                return NotFound();
            }

            return View(savedOffer);
        }

        // GET: SavedOffers/Create
        public IActionResult Create()
        {
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "Title");
            return View();
        }

        // POST: SavedOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SavedOfferId,UserId,VehicleOfferId,IsActive")] SavedOffer savedOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savedOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", savedOffer.VehicleOfferId);
            return View(savedOffer);
        }

        // GET: SavedOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedOffer = await _context.SavedOffers.FindAsync(id);
            if (savedOffer == null)
            {
                return NotFound();
            }
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", savedOffer.VehicleOfferId);
            return View(savedOffer);
        }

        // POST: SavedOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SavedOfferId,UserId,VehicleOfferId,IsActive")] SavedOffer savedOffer)
        {
            if (id != savedOffer.SavedOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedOfferExists(savedOffer.SavedOfferId))
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
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", savedOffer.VehicleOfferId);
            return View(savedOffer);
        }

        // GET: SavedOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedOffer = await _context.SavedOffers
                .Include(s => s.VehicleOffer)
                .FirstOrDefaultAsync(m => m.SavedOfferId == id);
            if (savedOffer == null)
            {
                return NotFound();
            }

            return View(savedOffer);
        }

        // POST: SavedOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedOffer = await _context.SavedOffers.FindAsync(id);
            if (savedOffer != null)
            {
                _context.SavedOffers.Remove(savedOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedOfferExists(int id)
        {
            return _context.SavedOffers.Any(e => e.SavedOfferId == id);
        }
    }
}
