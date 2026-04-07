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
    public class VehicleOfferFeaturesController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public VehicleOfferFeaturesController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: VehicleOfferFeatures
        public async Task<IActionResult> Index()
        {
            var prestivaCarsContext = _context.VehicleOfferFeatures.Include(v => v.VehicleFeature).Include(v => v.VehicleOffer);
            return View(await prestivaCarsContext.ToListAsync());
        }

        // GET: VehicleOfferFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOfferFeature = await _context.VehicleOfferFeatures
                .Include(v => v.VehicleFeature)
                .Include(v => v.VehicleOffer)
                .FirstOrDefaultAsync(m => m.VehicleOfferFeatureId == id);
            if (vehicleOfferFeature == null)
            {
                return NotFound();
            }

            return View(vehicleOfferFeature);
        }

        // GET: VehicleOfferFeatures/Create
        public IActionResult Create()
        {
            ViewData["VehicleFeatureId"] = new SelectList(_context.VehicleFeatures, "VehicleFeatureId", "CreatedBy");
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy");
            return View();
        }

        // POST: VehicleOfferFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleOfferFeatureId,VehicleOfferId,VehicleFeatureId,IsActive")] VehicleOfferFeature vehicleOfferFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleOfferFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleFeatureId"] = new SelectList(_context.VehicleFeatures, "VehicleFeatureId", "CreatedBy", vehicleOfferFeature.VehicleFeatureId);
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", vehicleOfferFeature.VehicleOfferId);
            return View(vehicleOfferFeature);
        }

        // GET: VehicleOfferFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOfferFeature = await _context.VehicleOfferFeatures.FindAsync(id);
            if (vehicleOfferFeature == null)
            {
                return NotFound();
            }
            ViewData["VehicleFeatureId"] = new SelectList(_context.VehicleFeatures, "VehicleFeatureId", "CreatedBy", vehicleOfferFeature.VehicleFeatureId);
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", vehicleOfferFeature.VehicleOfferId);
            return View(vehicleOfferFeature);
        }

        // POST: VehicleOfferFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleOfferFeatureId,VehicleOfferId,VehicleFeatureId,IsActive")] VehicleOfferFeature vehicleOfferFeature)
        {
            if (id != vehicleOfferFeature.VehicleOfferFeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleOfferFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleOfferFeatureExists(vehicleOfferFeature.VehicleOfferFeatureId))
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
            ViewData["VehicleFeatureId"] = new SelectList(_context.VehicleFeatures, "VehicleFeatureId", "CreatedBy", vehicleOfferFeature.VehicleFeatureId);
            ViewData["VehicleOfferId"] = new SelectList(_context.VehicleOffers, "VehicleOfferId", "CreatedBy", vehicleOfferFeature.VehicleOfferId);
            return View(vehicleOfferFeature);
        }

        // GET: VehicleOfferFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleOfferFeature = await _context.VehicleOfferFeatures
                .Include(v => v.VehicleFeature)
                .Include(v => v.VehicleOffer)
                .FirstOrDefaultAsync(m => m.VehicleOfferFeatureId == id);
            if (vehicleOfferFeature == null)
            {
                return NotFound();
            }

            return View(vehicleOfferFeature);
        }

        // POST: VehicleOfferFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleOfferFeature = await _context.VehicleOfferFeatures.FindAsync(id);
            if (vehicleOfferFeature != null)
            {
                _context.VehicleOfferFeatures.Remove(vehicleOfferFeature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleOfferFeatureExists(int id)
        {
            return _context.VehicleOfferFeatures.Any(e => e.VehicleOfferFeatureId == id);
        }
    }
}
