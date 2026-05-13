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
    public class TransmissionTypesController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public TransmissionTypesController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: TransmissionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransmissionTypes.ToListAsync());
        }

        // GET: TransmissionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transmissionType = await _context.TransmissionTypes
                .FirstOrDefaultAsync(m => m.TransmissionTypeId == id);
            if (transmissionType == null)
            {
                return NotFound();
            }

            return View(transmissionType);
        }

        // GET: TransmissionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransmissionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransmissionTypeId,Name,Description,IsActive")] TransmissionType transmissionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transmissionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transmissionType);
        }

        // GET: TransmissionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transmissionType = await _context.TransmissionTypes.FindAsync(id);
            if (transmissionType == null)
            {
                return NotFound();
            }
            return View(transmissionType);
        }

        // POST: TransmissionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransmissionTypeId,Name,Description,IsActive")] TransmissionType transmissionType)
        {
            if (id != transmissionType.TransmissionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transmissionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransmissionTypeExists(transmissionType.TransmissionTypeId))
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
            return View(transmissionType);
        }

        // GET: TransmissionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transmissionType = await _context.TransmissionTypes
                .FirstOrDefaultAsync(m => m.TransmissionTypeId == id);
            if (transmissionType == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModal",transmissionType);
        }

        // POST: TransmissionTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var transmissionType = await _context.TransmissionTypes.FindAsync(id);
            if (transmissionType == null)
            {
                return NotFound();
            }

            transmissionType.IsActive = false;
            _context.Update(transmissionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransmissionTypeExists(int id)
        {
            return _context.TransmissionTypes.Any(e => e.TransmissionTypeId == id);
        }
    }
}
