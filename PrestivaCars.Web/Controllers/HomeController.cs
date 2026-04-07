using Microsoft.AspNetCore.Mvc;
using PrestivaCars.Data.Data;
using PrestivaCars.Web.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PrestivaCars.Web.Controllers
{
    /// <summary>
    /// The following code defines a HomeController class that inherits from the Controller base class. 
    /// It uses dependency injection to receive an instance of the PrestivaCarsContext, 
    /// which is the Entity Framework Core database context for the application. 
    /// The controller has three action methods: Index, Privacy, and Error. The Index method retrieves a list of pages from the database and returns a view with the 
    /// selected page based on the provided id parameter. 
    /// The Privacy method simply returns a view, while the Error method returns a view with an error model containing the request ID.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly PrestivaCarsContext _context;
        public HomeController(PrestivaCarsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.PageModel = await _context.Pages
                .OrderBy(p => p.Position)
                .ToListAsync();

            var page = id.HasValue
                ? await _context.Pages.FirstOrDefaultAsync(p => p.PageId == id.Value)
                : await _context.Pages.FirstOrDefaultAsync();

            return View(page);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
