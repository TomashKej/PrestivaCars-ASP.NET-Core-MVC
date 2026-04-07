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
    public class VehicleFeaturesController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public VehicleFeaturesController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: VehicleFeatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleFeatures.ToListAsync());
        }

        // GET: VehicleFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleFeature = await _context.VehicleFeatures
                .FirstOrDefaultAsync(m => m.VehicleFeatureId == id);
            if (vehicleFeature == null)
            {
                return NotFound();
            }

            return View(vehicleFeature);
        }

        // GET: VehicleFeatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleFeatureId,Name,IconName,Position,IsActive")] VehicleFeature vehicleFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleFeature);
        }

        // GET: VehicleFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleFeature = await _context.VehicleFeatures.FindAsync(id);
            if (vehicleFeature == null)
            {
                return NotFound();
            }
            return View(vehicleFeature);
        }

        // POST: VehicleFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleFeatureId,Name,IconName,Position,IsActive")] VehicleFeature vehicleFeature)
        {
            if (id != vehicleFeature.VehicleFeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleFeatureExists(vehicleFeature.VehicleFeatureId))
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
            return View(vehicleFeature);
        }

        // GET: VehicleFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleFeature = await _context.VehicleFeatures
                .FirstOrDefaultAsync(m => m.VehicleFeatureId == id);
            if (vehicleFeature == null)
            {
                return NotFound();
            }

            return View(vehicleFeature);
        }

        // POST: VehicleFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleFeature = await _context.VehicleFeatures.FindAsync(id);
            if (vehicleFeature != null)
            {
                _context.VehicleFeatures.Remove(vehicleFeature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleFeatureExists(int id)
        {
            return _context.VehicleFeatures.Any(e => e.VehicleFeatureId == id);
        }
    }
}
