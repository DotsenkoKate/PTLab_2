using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTLab_2.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
           /* int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            List<Product> products = new List<Product>();
            products = _db.Products.ToList();
            
            List<Cart> carts = new List<Cart>();
            carts = _db.Carts.ToList();
            int discount = GetDiscount((_db.Customers.FirstOrDefault(c => c.Id == Convert.ToInt32(id_customer))).Purchase);
            List <Purchase> Test = new List<Purchase>();
            Test = Usercart();*/
            return View(await _db.Purchases.ToListAsync());
        }
        
        
        public int GetDiscount(int purchase)
        {
            int discount = purchase / 100;
            if (discount > 15) discount = 15;
            if (discount < 1) discount = 1;
            return discount;
        }
       public List <Purchase> Usercart()
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            List<Purchase> ListCart = new List<Purchase>();
            using (_db = new Hardware_storeContext())
            {
                var cart = _db.Purchases.Where(p => p.CustomerId == id_customer);
                foreach (Purchase user in cart)
                {
                    ListCart.Add(user);
                    Console.WriteLine(ListCart);
                }
            }
            
            return ListCart;
        }

        public IActionResult AddToCart(int id)
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            Product? item;
            item = _db.Products.FirstOrDefault(c => c.Id == id);
            Cart cart = new Cart
            {
                CustomerId = id_customer,
                ProductId = item.Id,
                Quantity = 1,
            };
            
            _db.Carts.Add(cart);
            _db.SaveChanges();
        
        return RedirectToAction("Cart");
    }
        
        public IActionResult Buy2(int id, int quantity)
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            Product? item;
            item = _db.Products.FirstOrDefault(c => c.Id == id);
            var tmp = _db.Customers.FirstOrDefault(c => c.Id == Convert.ToInt32(id_customer)).Purchase;
            int discount = GetDiscount(tmp);
            if (quantity < 0) quantity = 1;
            Purchase purchase = new Purchase
            {
                CustomerId = id_customer,
                Quantity = quantity,
                ProductName = item.Name,
                Price = item.Price,
                Discount = discount,
            };
            _db.Purchases.Add(purchase);
            _db.SaveChanges();
        
            return RedirectToAction("Cart");
        }
        
        public ActionResult BuyItem2(int id)
        {
            
           int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
           Customer customer = _db.Customers.FirstOrDefault(c => c.Id == id_customer);

           Purchase purchase = _db.Purchases.FirstOrDefault(c => c.Id == id);
            int tmp = purchase.Quantity;
            if (purchase != null)
            {
                _db.Purchases.Remove(purchase);
                _db.SaveChanges();
            }
            customer.Purchase += tmp;
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Login()
        {
            return View();
        }
        public  IActionResult Purchase()
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            var res =_db.Purchases.FirstOrDefault(c => c.CustomerId == Convert.ToInt32(id_customer));
            List<Purchase> purchase = new List<Purchase>();
            purchase.Add(res);
            return View(purchase);
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