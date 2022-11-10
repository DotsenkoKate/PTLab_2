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
        public async Task<IActionResult> Cart()
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            return View(await _db.Carts.ToListAsync());
        }
        public Product GetPriceAndNameProduct(string id_product)
        {
            Product? product = _db.Products.FirstOrDefault(c => c.Id == Convert.ToInt32(id_product));
            return product;
        }
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AddToCart(int id)
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            Product? item;
            item = _db.Products.FirstOrDefault(c => c.Id == id);
            Cart cart = new Cart
            {
                //КОСТЫЛЬ
                Id = 6,
                CustomerId = id_customer,
                ProductId = item.Id,
                Quantity = 1,
            };
            _db.Carts.Add(cart);
            _db.SaveChanges();
        
        return RedirectToAction("Cart");
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
                    HttpContext.Session.SetString("id",customer.Id.ToString());

                    return RedirectToAction("Index", customer.Id);
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