using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTLab_2.Models;
using System.Diagnostics;


namespace PTLab_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  StoreContext _db;
        public HomeController(ILogger<HomeController> logger)
        {
            _db = new StoreContext();
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Products.ToListAsync());
        }
        public async Task<IActionResult> Cart()
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            var result = from purchase in _db.Purchases
                join customer in _db.Customers on purchase.CustomerId equals customer.Id
                join product in _db.Products on purchase.ProductId equals product.Id
                where (customer.Id == id_customer & product.Id == purchase.ProductId) 
                select new Cart()
                {
                    Price = product.Price,
                    ProductName = product.Name,
                    Quantity = purchase.Quantity,
                    Discount = GetDiscount(customer.Purchase)
                };

            return View(await result.ToListAsync());
        }
        
        public IActionResult AddToCart(int id, int quantity)
        {
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            if (quantity <= 0) quantity = 1;
            Purchase purchase = new Purchase
            {
                CustomerId = id_customer,
                Quantity = quantity,
                ProductId = id,

            };
            _db.Purchases.Add(purchase);
            _db.SaveChanges();
        
            return RedirectToAction("Cart");
        }
        
        public ActionResult Buy()
        {   
           int idCustomer = Convert.ToInt32(HttpContext.Session.GetString("id"));
           Customer customer = _db.Customers.FirstOrDefault(c => c.Id == idCustomer);
           var result = _db.Purchases.Where(p => p.CustomerId == idCustomer).ToArray();
           int sum = 0;
           foreach (var pur in result)
           {
               sum += pur.Quantity;
               _db.Purchases.Remove(pur); 
               _db.SaveChanges();
           }
           
            customer.Purchase += sum;
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
                Customer? customer = _db.Customers.FirstOrDefault(c => c.Login == login);
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
        
        //Функция подсчёта размера скидки
        public static int GetDiscount(int purchase)
        {
            int discount = purchase / 100;
            if (discount > 15) discount = 15;
            if (discount < 1) discount = 1;
            return discount;
        }
    }
} 