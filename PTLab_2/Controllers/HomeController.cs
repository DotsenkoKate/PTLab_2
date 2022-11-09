using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTLab_2.Models;
using System.Diagnostics;

namespace PTLab_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  Hardware_storeContext _db;
        public HomeController(ILogger<HomeController> logger)
        {
            _db = new Hardware_storeContext();
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Products.ToListAsync());
        }
        public IActionResult LoginPage()
        {
            return View();
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