using PrestivaCars.Interfaces.CMS;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Services.CMS;
using PrestivaCars.Services.Vehicles;

namespace PrestivaCars.Web
{
    public class DependencyInjectionFactory
    {
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVehicleOfferService, VehicleOfferService>();
            services.AddScoped<IVehicleCategoryService, VehicleCategoryService>();
            services.AddScoped<IPageService, PageService>();
        }
    }
}
