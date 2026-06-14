using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Vehicles;
using PrestivaCars.Intranet.Services;
using PrestivaCars.Interfaces.Export;

namespace PrestivaCars.Intranet.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly PrestivaCarsContext _context;
        private readonly VehicleFormService _vehicleFormService;
        private readonly IVehicleExcelExportService _vehicleExcelExportService;

        public VehiclesController(PrestivaCarsContext context, VehicleFormService vehicleFormService, IVehicleExcelExportService vehicleExcelExportService)
        {
            _context = context;
            _vehicleFormService = vehicleFormService;
            _vehicleExcelExportService = vehicleExcelExportService;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var prestivaCarsContext = _context.Vehicles
                .Include(v => v.VehicleCategory)
                .Include(v => v.Brand)
                .Include(v => v.FuelType)
                .Include(v => v.TransmissionType)
                .Include(v => v.BodyType)
                .Include(v => v.VehicleColour);
            return View(await prestivaCarsContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleCategory)
                .Include(v => v.Brand)
                .Include(v => v.FuelType)
                .Include(v => v.TransmissionType)
                .Include(v => v.BodyType)
                .Include(v => v.VehicleColour)
                .FirstOrDefaultAsync(m => m.VehicleId == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            _vehicleFormService.PrepareSelectList(ViewData);
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,Model,ProductionYear,Mileage,EngineCapacity,PowerHp,Vin,RegistrationNumber,VehicleCategoryId,BrandId,FuelTypeId,TransmissionTypeId,BodyTypeId,VehicleColourId,IsActive")] Vehicle vehicle)
        {
            _vehicleFormService.RemoveNavigationValidation(ModelState);

            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            _vehicleFormService.PrepareSelectList(ViewData, vehicle);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }
            _vehicleFormService.PrepareSelectList(ViewData, vehicle);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Model,ProductionYear,Mileage,EngineCapacity,PowerHp,Vin,RegistrationNumber,VehicleCategoryId,BrandId,FuelTypeId,TransmissionTypeId,BodyTypeId,VehicleColourId,IsActive")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            _vehicleFormService.RemoveNavigationValidation(ModelState);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            _vehicleFormService.PrepareSelectList(ViewData, vehicle);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleCategory)
                .Include(v => v.Brand)
                .Include(v => v.FuelType)
                .Include(v => v.TransmissionType)
                .Include(v => v.BodyType)
                .Include(v => v.VehicleColour)
                .FirstOrDefaultAsync(m => m.VehicleId == id && m.IsActive);

            if (vehicle == null)
            {
                return NotFound();
            }

            //return partialView(vehicle);
            return PartialView("_DeleteModal", vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            var fileContent = await _vehicleExcelExportService.GenerateVehiclesExcelFileAsync();

            var fileName = $"prestiva-vehicles-export-{DateTime.Now:yyyyMMdd-HHmm}.xlsx";

            return File(
                fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        // This method checks if a vehicle with the given ID exists in the database.
        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
