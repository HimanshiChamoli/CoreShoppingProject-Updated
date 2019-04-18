using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Helpers;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EcommerceUserPanel.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        //ShoppingDemoooo2Context context = new ShoppingDemoooo2Context();
        private readonly ShoppingDemoooo2Context _context;

        public CartController(ShoppingDemoooo2Context context)
        {
            _context = context;
        }
        [Route("index")]
        public IActionResult Index()
        {
            ;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            foreach(var it in cart)
            {
                if(it.Quantity==it.products.ProductQty)
                {
                    ViewBag.j = it.products.ProductId;
                }
            }
            int i = 0;
            if (cart != null)
            {

                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {
                    ViewBag.cart = cart;
                    ViewBag.total = cart.Sum(item => item.products.ProductPrice * item.Quantity);
                    if (SessionHelper.GetObjectFromJson<Customers>(HttpContext.Session, "cust") == null)
                    {
                        ViewBag.i = 0;
                    }
                    else
                    {
                        ViewBag.i = 1;
                    }
                    return View();
                }

            }
            return View("GoBack");
        }

        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            
            var detail= _context.Products.Find(id);
            var cid = _context.Products.Find(id);
            ViewBag.cname = _context.Categories.Find(cid.ProductCategoryId);
            return View(detail);
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            int i = 0;
            ViewBag.i = i;
            var checkout = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.checkout = checkout;
            ViewBag.total = checkout.Sum(item => item.products.ProductPrice * item.Quantity);
            string cust = HttpContext.Session.GetString("uname");
            Customers cus = _context.Customers.Where(x => x.Username == cust).SingleOrDefault();
            ViewBag.cus = cus;
            ViewBag.totalitem = checkout.Count();
            TempData["total"] = ViewBag.total;

            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Customers customer,string stripeEmail,string stripeToken)
        {
            //int custId = int.Parse(TempData["cust"].ToString());
            //Customers customers = context.Customers.Where(x => x.CustomerId == custId).SingleOrDefault();
            //ViewBag.Customers = customers;
            //if (ModelState.IsValid)
            //{
            var c = _context.Customers.Where(x => x.Username == customer.Username).SingleOrDefault();
            c.FirstName = customer.FirstName;
            c.LastName= customer.LastName;
            c.Username = customer.Username;
            c.EmailId = customer.EmailId;
            c.Address = customer.Address;
            c.PhoneNo = customer.PhoneNo;
            c.Country = customer.Country;
            c.State = customer.State;
            c.Zip = customer.Zip;
            _context.SaveChanges();
            var price = TempData["total"];
            Orders order = new Orders()
                {
                    OrderPrice = Convert.ToSingle(price),
                    OrderDate = DateTime.Now,
                    CustomerId = c.CustomerId
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            TempData["orderId"] = order.OrderId;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                List<OrderProducts> OrderProducts = new List<OrderProducts>();
                for (int i = 0; i < cart.Count; i++)
                {
                    OrderProducts orderProducts = new OrderProducts()
                    {
                        OrderId = order.OrderId,
                        ProductId = cart[i].products.ProductId,
                        Quantity = cart[i].Quantity
                    };
                    OrderProducts.Add(orderProducts);

                }
                OrderProducts.ForEach(n => _context.OrderProducts.Add(n));
                _context.SaveChanges();
                TempData["cust"] = c.CustomerId;
            var customers = new CustomerService();
            var charges = new ChargeService();
            var Amount = TempData["total"];
            var orders = TempData["orderId"];
            var custt = TempData["cust"];
            var customer1 = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer1.Id
            });

            Payments payment = new Payments();
            {
                payment.PaymentStripeId = charge.PaymentMethodId;
                payment.Amount = Convert.ToInt32(Amount);
                payment.Date = System.DateTime.Now;
                payment.CardNo = Convert.ToInt32(charge.PaymentMethodDetails.Card.Last4);
                payment.OrderId = Convert.ToInt32(orders);
                payment.CustomerId = Convert.ToInt32(custt);
            }

            _context.Add<Payments>(payment);
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return RedirectToAction("Invoice");
            }
        public IActionResult PaymentIndex()
        {
            var checkout = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.checkout = checkout;
            ViewBag.total = checkout.Sum(item => item.products.ProductPrice * item.Quantity);
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [Route("Buy/{id}")]
        public IActionResult Buy(int id)
        {
            Products prod = _context.Products.Find(id);
            if(prod.ProductQty<1){
                return RedirectToAction("OutOfStock", "cart", new { @id = id });
            }
           if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { products = _context.Products.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item
                    {
                        products = _context.Products.Find(id),
                        Quantity = 1
                    });
                }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            
                return RedirectToAction("Index","Home");
            }
        [Route("outofstock/{id}")]
        public IActionResult OutOfStock(int id)
        {
            var detail = _context.Products.Find(id);
            var cid = _context.Products.Find(id);
            ViewBag.cname = _context.Categories.Find(cid.ProductCategoryId);
            return View(detail);
        }
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            int j = int.Parse(HttpContext.Session.GetString("cartitem"));
            int i = 0;
            foreach (var item in cart)
            {
                i++;
            }
            if (i != 0)
            {
                j--;
                HttpContext.Session.SetString("cartitem", j.ToString());
            }
            else
            {
                HttpContext.Session.Remove("cartitem");
                return View("GoBack");
            }


            //SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");

        }
        [Route("GoBack")]
        public IActionResult GoBack()
        {
            return View();
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].products.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [Route("Invoice")]
        public IActionResult Invoice()
        {
            int custId = int.Parse(TempData["cust"].ToString());
            Customers customers = _context.Customers.Where(x => x.CustomerId == custId).SingleOrDefault();
            ViewBag.Cust = customers;


            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            foreach(var Item1 in cart)
            {
                Products p = _context.Products.Find(Item1.products.ProductId);
                p.ProductQty = p.ProductQty - Item1.Quantity;
                _context.SaveChanges();
            }
            ViewBag.total = cart.Sum(item => item.products.ProductPrice * item.Quantity);
            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            HttpContext.Session.Remove("cartitem");
            return View();

        }
        [Route("Plus")]
        [HttpGet]
        public IActionResult Plus(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            else
            {
                cart.Add(new Item
                {
                    products = _context.Products.Find(id),
                    Quantity = 1
                });

            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        [Route("Minus")]
        [HttpGet]
        public IActionResult Minus(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantity != 1)
                {
                    cart[index].Quantity--;
                }

                else
                    return RedirectToAction("Remove", "Cart", new { @id = id });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
    }
}   
       
  