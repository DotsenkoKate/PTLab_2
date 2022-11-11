﻿using Microsoft.AspNetCore.Mvc;
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
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            List<Product> products = new List<Product>();
            products = _db.Products.ToList();
            
            List<Cart> carts = new List<Cart>();
            carts = _db.Carts.ToList();
            int discount = GetDiscount((_db.Customers.FirstOrDefault(c => c.Id == Convert.ToInt32(id_customer))).Purchase);
            
            
            
            
            return View(await _db.Carts.ToListAsync());
        }
        public int GetDiscount(int purchase)
        {
            int discount = purchase / 10;
            if (discount > 10) discount = 10;
            if (discount < 1) discount = 1;
            return discount;
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
                CustomerId = id_customer,
                ProductId = item.Id,
                Quantity = 1,
            };
            
            _db.Carts.Add(cart);
            _db.SaveChanges();
        
        return RedirectToAction("Cart");
    }
        
        public IActionResult Buy(int id, int quantity)
        {
            Console.WriteLine(quantity);
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            Product? item;
            item = _db.Products.FirstOrDefault(c => c.Id == id);
            var tmp = _db.Customers.FirstOrDefault(c => c.Id == Convert.ToInt32(id_customer)).Purchase;
            int discount = GetDiscount(tmp);
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
        
            return RedirectToAction("Purchase");
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
        public ActionResult BuyItem(int id)
        {
            Purchase purchase = _db.Purchases.FirstOrDefault(c => c.Id == id);
            int tmp = purchase.Quantity;
            if (purchase != null)
            {
                _db.Purchases.Remove(purchase);
                _db.SaveChanges();
            }
            int id_customer = Convert.ToInt32(HttpContext.Session.GetString("id"));
            Customer customer = _db.Customers.FirstOrDefault(c => c.Id == id_customer);
            customer.Purchase += tmp;
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
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