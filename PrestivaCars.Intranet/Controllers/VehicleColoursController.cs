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
    public class VehicleColoursController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public VehicleColoursController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: VehicleColours
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleColours.ToListAsync());
        }

        // GET: VehicleColours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleColour = await _context.VehicleColours
                .FirstOrDefaultAsync(m => m.VehicleColourId == id);
            if (vehicleColour == null)
            {
                return NotFound();
            }

            return View(vehicleColour);
        }

        // GET: VehicleColours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleColours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleColourId,ColourName,IsActive")] VehicleColour vehicleColour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleColour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleColour);
        }

        // GET: VehicleColours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleColour = await _context.VehicleColours.FindAsync(id);
            if (vehicleColour == null)
            {
                return NotFound();
            }
            return View(vehicleColour);
        }

        // POST: VehicleColours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleColourId,ColourName,IsActive")] VehicleColour vehicleColour)
        {
            if (id != vehicleColour.VehicleColourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleColour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleColourExists(vehicleColour.VehicleColourId))
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
            return View(vehicleColour);
        }

        // GET: VehicleColours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleColour = await _context.VehicleColours
                .FirstOrDefaultAsync(m => m.VehicleColourId == id);
            if (vehicleColour == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModal",vehicleColour);
        }

        // POST: VehicleColours/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleColour = await _context.VehicleColours.FindAsync(id);
            if (vehicleColour == null)
            {
                return NotFound();
            }

            vehicleColour.IsActive = false;
            _context.Update(vehicleColour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleColourExists(int id)
        {
            return _context.VehicleColours.Any(e => e.VehicleColourId == id);
        }
    }
}
