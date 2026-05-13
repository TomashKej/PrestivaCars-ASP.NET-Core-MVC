using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PrestivaCars.Data.Data;
using PrestivaCars.Data.Data.Vehicles;

namespace PrestivaCars.Intranet.Services
{
    public class VehicleFormService
    {
        private readonly PrestivaCarsContext _context;

        public VehicleFormService(PrestivaCarsContext context)
        { 
            _context = context;
        }

        public void PrepareSelectList(ViewDataDictionary viewData, Vehicle? vehicle = null)
        {
            viewData["VehicleCategoryId"] = new SelectList(
                _context.VehicleCategories.Where(x => x.IsActive),
                "VehicleCategoryId",
                "Name",
                vehicle?.VehicleCategoryId
            );

            viewData["BrandId"] = new SelectList(
                _context.Brands.Where(x => x.IsActive),
                "BrandId",
                "Name",
                vehicle?.BrandId
            );

            viewData["FuelTypeId"] = new SelectList(
                _context.FuelTypes.Where(x => x.IsActive),
                "FuelTypeId",
                "Name",
                vehicle?.FuelTypeId
            );

            viewData["TransmissionTypeId"] = new SelectList(
                _context.TransmissionTypes.Where(x => x.IsActive),
                "TransmissionTypeId",
                "Name",
                vehicle?.TransmissionTypeId
            );

            viewData["BodyTypeId"] = new SelectList(
                _context.BodyTypes.Where(x => x.IsActive),
                "BodyTypeId",
                "Name",
                vehicle?.BodyTypeId
            );

            viewData["VehicleColourId"] = new SelectList(
                _context.VehicleColours.Where(x => x.IsActive),
                "VehicleColourId",
                "ColourName",
                vehicle?.VehicleColourId
            );
        }

        public void RemoveNavigationValidation(ModelStateDictionary modelState)
        {
            modelState.Remove(nameof(Vehicle.VehicleCategory));
            modelState.Remove(nameof(Vehicle.Brand));
            modelState.Remove(nameof(Vehicle.FuelType));
            modelState.Remove(nameof(Vehicle.TransmissionType));
            modelState.Remove(nameof(Vehicle.BodyType));
            modelState.Remove(nameof(Vehicle.VehicleColour));
            modelState.Remove(nameof(Vehicle.VehicleOffer));
        }
    }
}
