using Microsoft.AspNetCore.Mvc;
using Pigga_WebApplication.DAL;
using System.Diagnostics;

namespace Pigga_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

       

        public IActionResult Index()
        {
            return View(_context.Chefss.ToList());
        }

       
    }
}
