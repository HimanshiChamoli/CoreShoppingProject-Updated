using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Helpers;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUserPanel.Controllers
{

    public class CustomerController : Controller
    {
        ShoppingDemoooo2Context context = new ShoppingDemoooo2Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customers customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            HttpContext.Session.SetString("Logout", customer.Username);
            return RedirectToAction("Login");

        }


        public IActionResult Login()
        {
            return View();
        }
        //[Route("Login")]
        [HttpPost]

        public ActionResult Login(string Password, string Username)
        {
            var user = context.Customers.Where(x => x.Username == Username).SingleOrDefault();
            ViewBag.cust = user;
            if (user == null)
            {
                ViewBag.Error = "Invalid Credentials";
                return View("Login");
            }
            else
            {
                var userName = user.Username;
                int custId = ViewBag.cust.CustomerId;
                if (userName != null && Password != null && userName.Equals(userName)
                    && Password.Equals(user.Password))
                {
                    HttpContext.Session.SetString("uname", userName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cust", user);
                    HttpContext.Session.SetString("Logout", userName);
                    return RedirectToAction("Index", "Home", new { @id = custId });
                }
                else
                {
                    ViewBag.Error = "Invalid Credentials";
                    return View("Login");
                }
            }
        }
        public IActionResult Login1()
        {
            return View();
        }
        //[Route("Login")]
        [HttpPost]

        public ActionResult Login1(string Password, string Username)
        {
            var user = context.Customers.Where(x => x.Username == Username).SingleOrDefault();
            ViewBag.cust = user;
            if (user == null)
            {
                ViewBag.Error = "Invalid Credentials";
                return View("Login");
            }
            else
            {
                var userName = user.Username;
                int custId = ViewBag.cust.CustomerId;
                if (userName != null && Password != null && userName.Equals(userName)
                    && Password.Equals(user.Password))
                {
                    HttpContext.Session.SetString("uname", userName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cust", user);
                    HttpContext.Session.SetString("Logout", userName);
                    return RedirectToAction("Index", "Home", new { @id = custId });
                }
                else
                {
                    ViewBag.Error = "Invalid Credentials";
                    return View("Login");
                }
            }
        }
        [Route("Logout")]
        [HttpGet]

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("uname");
            HttpContext.Session.Remove("Logout");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CustomerEdit()
        {
            Customers cus = SessionHelper.GetObjectFromJson<Customers>(HttpContext.Session, "cust");
            return View(cus);
        }
        [HttpPost]
        public IActionResult CustomerEdit(int id, Customers customer)
        {
            var c = context.Customers.Where(x => x.Username == customer.Username).SingleOrDefault();
            c.FirstName = customer.FirstName;
            c.LastName = customer.LastName;
            c.Username = customer.Username;
            c.EmailId = customer.EmailId;
            c.Address = customer.Address;
            c.PhoneNo = customer.PhoneNo;
            c.Country = customer.Country;
            c.State = customer.State;
            c.Zip = customer.Zip;
            context.SaveChanges();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cust", c);
            return RedirectToAction("Index", "Home", new { @id = customer.Username });
        }
     
        public IActionResult Profile(Customers customers)
        {
            Customers prof = SessionHelper.GetObjectFromJson<Customers>(HttpContext.Session, "cust");
            //var c = context.Customers.Where(x => x.Username == customers.Username).SingleOrDefault();
            return View(prof);
        }
      

        public IActionResult OrderHistory()
        {
            Customers c = SessionHelper.GetObjectFromJson<Customers>(HttpContext.Session, "cust");
            List<Orders> ord = context.Orders.Where(x => x.CustomerId == c.CustomerId).ToList();
            ViewBag.ord = ord;
            return View();
        }
        public IActionResult orderdetail(int id)
        {
            List<OrderProducts> op = new List<OrderProducts>();
            List<Products> products = new List<Products>();
            op = context.OrderProducts.Where(x => x.OrderId == id).ToList();
            foreach (var item in op)
            {
                Products c = context.Products.Where(x => x.ProductId == item.ProductId).SingleOrDefault();
                products.Add(c);
            }
            ViewBag.p = products;
            return View();
        }
        public IActionResult Password()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Password(string oldpassword, string newpassword, string newpassword1)
        {
            Customers c = SessionHelper.GetObjectFromJson<Customers>(HttpContext.Session, "cust");
            if (oldpassword == c.Password && newpassword == newpassword1)
            {
                Customers cus = context.Customers.Where(x =>x.Username==c.Username).SingleOrDefault();
                cus.Password = newpassword;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cust", cus);
                context.SaveChanges();
            }
            else if (oldpassword != c.Password && newpassword == newpassword1)
            {

                ViewBag.Error = "Invalid Old Password";
                return View("Password");
            }
            else if (oldpassword == c.Password && newpassword != newpassword1)
            {

                ViewBag.Error = "New Password and Confirm Password  must be same ";
                return View("Password");
            }
            else
            {
                ViewBag.Error = "Invalid Credentials";
                return View("Password");
            }

            return RedirectToAction("Login","Customer");
        }
    }

}
           
            
    