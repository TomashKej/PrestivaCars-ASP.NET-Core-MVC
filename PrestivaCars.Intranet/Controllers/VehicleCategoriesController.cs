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
    public class VehicleCategoriesController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public VehicleCategoriesController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: VehicleCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleCategories.ToListAsync());
        }

        // GET: VehicleCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleCategory = await _context.VehicleCategories
                .FirstOrDefaultAsync(m => m.VehicleCategoryId == id);
            if (vehicleCategory == null)
            {
                return NotFound();
            }

            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleCategoryId,Name,Slug,Description,IconName,ImagePath,Position,IsActive")] VehicleCategory vehicleCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleCategory = await _context.VehicleCategories.FindAsync(id);
            if (vehicleCategory == null)
            {
                return NotFound();
            }
            return View(vehicleCategory);
        }

        // POST: VehicleCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleCategoryId,Name,Slug,Description,IconName,ImagePath,Position,IsActive")] VehicleCategory vehicleCategory)
        {
            if (id != vehicleCategory.VehicleCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleCategoryExists(vehicleCategory.VehicleCategoryId))
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
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleCategory = await _context.VehicleCategories
                .FirstOrDefaultAsync(m => m.VehicleCategoryId == id);
            if (vehicleCategory == null)
            {
                return NotFound();
            }

            return View(vehicleCategory);
        }

        // POST: VehicleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleCategory = await _context.VehicleCategories.FindAsync(id);
            if (vehicleCategory != null)
            {
                _context.VehicleCategories.Remove(vehicleCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleCategoryExists(int id)
        {
            return _context.VehicleCategories.Any(e => e.VehicleCategoryId == id);
        }
    }
}
