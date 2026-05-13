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
    public class BodyTypesController : Controller
    {
        private readonly PrestivaCarsContext _context;

        public BodyTypesController(PrestivaCarsContext context)
        {
            _context = context;
        }

        // GET: BodyTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BodyTypes.ToListAsync());
        }

        // GET: BodyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyTypes
                .FirstOrDefaultAsync(m => m.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }

        // GET: BodyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BodyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BodyTypeId,Name,Description,IsActive")] BodyType bodyType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodyType);
        }

        // GET: BodyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyTypes.FindAsync(id);
            if (bodyType == null)
            {
                return NotFound();
            }
            return View(bodyType);
        }

        // POST: BodyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BodyTypeId,Name,Description,IsActive")] BodyType bodyType)
        {
            if (id != bodyType.BodyTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodyTypeExists(bodyType.BodyTypeId))
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
            return View(bodyType);
        }

        // GET: BodyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyTypes
                .FirstOrDefaultAsync(m => m.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModal", bodyType);
        }

        // POST: BodyTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var bodyType = await _context.BodyTypes.FindAsync(id);
            if (bodyType == null)
            {
                return NotFound();
            }

            bodyType.IsActive = false;
            _context.Update(bodyType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodyTypeExists(int id)
        {
            return _context.BodyTypes.Any(e => e.BodyTypeId == id);
        }
    }
}
