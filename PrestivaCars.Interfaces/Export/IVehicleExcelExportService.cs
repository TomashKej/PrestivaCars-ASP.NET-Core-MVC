namespace PrestivaCars.Interfaces.Export
{
    /// <summary>
    /// Interface for exporting vehicle data to an Excel file.
    /// </summary>
    public interface IVehicleExcelExportService
    {
        /// <summary>
        /// Generates an Excel file containing vehicle data.
        /// </summary>
        /// <returns>A byte array representing the Excel file.</returns>
        Task<byte[]> GenerateVehiclesExcelFileAsync();
    }
}