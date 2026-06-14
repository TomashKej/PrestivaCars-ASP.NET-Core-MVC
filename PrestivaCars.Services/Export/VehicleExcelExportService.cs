using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using PrestivaCars.Data.Data;
using PrestivaCars.Interfaces.Export;
using PrestivaCars.Services.Abstraction;

namespace PrestivaCars.Services.Export
{
    /// <summary>
    /// Service responsible for generating Excel files containing vehicle data.
    /// </summary>
    public class VehicleExcelExportService : BaseService, IVehicleExcelExportService
    {
        public VehicleExcelExportService(PrestivaCarsContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Generates an Excel file containing vehicle data from the database.
        /// </summary>
        /// <returns>A byte array representing the Excel file.</returns>
        public async Task<byte[]> GenerateVehiclesExcelFileAsync()
        {
            var vehicles = await _context.Vehicles
                .AsNoTracking()
                .Include(v => v.VehicleCategory)
                .Include(v => v.Brand)
                .Include(v => v.FuelType)
                .Include(v => v.TransmissionType)
                .Include(v => v.BodyType)
                .Include(v => v.VehicleColour)
                .OrderBy(v => v.Brand.Name)
                .ThenBy(v => v.Model)
                .ThenByDescending(v => v.ProductionYear)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Vehicles");

            worksheet.Cell(1, 1).Value = "PrestivaCars - Vehicles Export";
            worksheet.Range(1, 1, 1, 16).Merge();

            worksheet.Cell(2, 1).Value = "Generated at:";
            worksheet.Cell(2, 2).Value = DateTime.Now.ToString("dd MMM yyyy HH:mm");

            const int headerRow = 4;

            worksheet.Cell(headerRow, 1).Value = "ID";
            worksheet.Cell(headerRow, 2).Value = "Brand";
            worksheet.Cell(headerRow, 3).Value = "Model";
            worksheet.Cell(headerRow, 4).Value = "Category";
            worksheet.Cell(headerRow, 5).Value = "Production Year";
            worksheet.Cell(headerRow, 6).Value = "Mileage";
            worksheet.Cell(headerRow, 7).Value = "Engine Capacity";
            worksheet.Cell(headerRow, 8).Value = "Power HP";
            worksheet.Cell(headerRow, 9).Value = "Fuel Type";
            worksheet.Cell(headerRow, 10).Value = "Transmission";
            worksheet.Cell(headerRow, 11).Value = "Body Type";
            worksheet.Cell(headerRow, 12).Value = "Colour";
            worksheet.Cell(headerRow, 13).Value = "VIN";
            worksheet.Cell(headerRow, 14).Value = "Registration";
            worksheet.Cell(headerRow, 15).Value = "Status";
            worksheet.Cell(headerRow, 16).Value = "Created At";

            var currentRow = headerRow + 1;

            foreach (var vehicle in vehicles)
            {
                worksheet.Cell(currentRow, 1).Value = vehicle.VehicleId;
                worksheet.Cell(currentRow, 2).Value = vehicle.Brand?.Name ?? "N/A";
                worksheet.Cell(currentRow, 3).Value = vehicle.Model;
                worksheet.Cell(currentRow, 4).Value = vehicle.VehicleCategory?.Name ?? "N/A";
                worksheet.Cell(currentRow, 5).Value = vehicle.ProductionYear;
                worksheet.Cell(currentRow, 6).Value = vehicle.Mileage;
                worksheet.Cell(currentRow, 7).Value = vehicle.EngineCapacity;
                worksheet.Cell(currentRow, 8).Value = vehicle.PowerHp;
                worksheet.Cell(currentRow, 9).Value = vehicle.FuelType?.Name ?? "N/A";
                worksheet.Cell(currentRow, 10).Value = vehicle.TransmissionType?.Name ?? "N/A";
                worksheet.Cell(currentRow, 11).Value = vehicle.BodyType?.Name ?? "N/A";
                worksheet.Cell(currentRow, 12).Value = vehicle.VehicleColour?.ColourName ?? "N/A";
                worksheet.Cell(currentRow, 13).Value = vehicle.Vin;
                worksheet.Cell(currentRow, 14).Value = vehicle.RegistrationNumber;
                worksheet.Cell(currentRow, 15).Value = vehicle.IsActive ? "Active" : "Inactive";
                worksheet.Cell(currentRow, 16).Value = vehicle.CreatedAt.ToString("dd MMM yyyy HH:mm");

                currentRow++;
            }

            FormatWorksheet(worksheet, headerRow);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream.ToArray();
        }

        private static void FormatWorksheet(IXLWorksheet worksheet, int headerRow)
        {
            var usedRange = worksheet.RangeUsed();

            if (usedRange != null)
            {
                usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Row(1).Style.Font.FontSize = 16;
            worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Row(headerRow).Style.Font.Bold = true;
            worksheet.Row(headerRow).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(headerRow).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Columns().AdjustToContents();

            worksheet.Column(6).Style.NumberFormat.Format = "#,##0";
            worksheet.Column(7).Style.NumberFormat.Format = "#,##0";
            worksheet.Column(8).Style.NumberFormat.Format = "#,##0";

            worksheet.SheetView.FreezeRows(headerRow);
        }
    }
}