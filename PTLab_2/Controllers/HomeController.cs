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
        public IActionResult Cart(string id)
        {
            return View();
        }
        
        
        
        public IActionResult Privacy()
        {
            return View();
        }
        
        
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            try
            {
                Customer? customer;
                customer = _db.Customers.FirstOrDefault(c => c.Login == login);
                if (customer.Password == password)
                {
                    return RedirectToAction("Index", customer);
                }
                else throw new Exception();
            }
            catch (Exception e)
            {
                ViewData["Message"] = "Пользователь не найден!";
                return View();
            }
            
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
} 