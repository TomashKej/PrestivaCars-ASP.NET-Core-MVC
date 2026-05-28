using PrestivaCars.Data.Data;

namespace PrestivaCars.Services.Abstraction
{

    /// <summary>
    /// Provides a base class for service implementations that require access to the PrestivaCarsContext data context.
    /// </summary>
    /// <remarks>This class is intended to be inherited by application services that interact with the data
    /// layer. It supplies a protected data context for use by derived classes. Instances of this class should not be
    /// created directly.</remarks>

    public abstract class BaseService
    {
        protected readonly PrestivaCarsContext _context;

        protected BaseService(PrestivaCarsContext context) 
        { 
            _context = context;
        }
    }
}
