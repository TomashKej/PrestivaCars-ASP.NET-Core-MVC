using PrestivaCars.Interfaces.CMS;
using PrestivaCars.Interfaces.Vehicles;
using PrestivaCars.Services.CMS;
using PrestivaCars.Services.Vehicles;

namespace PrestivaCars.Web
{
    /// <summary>
    /// Provides a central place for registering application services used by the PrestivaCars.Web project.
    /// </summary>
    public class DependencyInjectionFactory
    {
        /// <summary>
        /// Adds scoped service dependencies to the dependency injection container.
        /// Each interface is mapped to its concrete service implementation.
        /// </summary>
        /// <param name="services">The service collection used by the application.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVehicleOfferService, VehicleOfferService>();
            services.AddScoped<IVehicleCategoryService, VehicleCategoryService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IBannerService, BannerService>();
        }
    }
}
