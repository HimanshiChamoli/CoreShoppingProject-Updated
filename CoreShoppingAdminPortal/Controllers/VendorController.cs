using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreShoppingAdminPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreShoppingAdminPortal.Controllers
{
    public class VendorController : Controller
    {
        ShopDataDbContext context;
        public VendorController(ShopDataDbContext _context)
        {
            context = _context;
        }

       

        public IActionResult Index()
        {
            var vendors = context.Vendors.ToList();

            return View(vendors);
        }
        [HttpGet]
        public ViewResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Vendor vendor)
        {
            context.Vendors.Add(vendor);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Vendor vend = context.Vendors.Where(x => x.VendorId == id).SingleOrDefault();
            
            return View(vend);
        }
        [HttpPost]
        public ActionResult Edit(Vendor vend, int id)
        {
            Vendor v1 = context.Vendors.Where(x => x.VendorId == id).SingleOrDefault();
            context.Entry(v1).CurrentValues.SetValues(vend);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ViewResult Details(int id)
        {
            Vendor vendor = context.Vendors.
             Where(x => x.VendorId == id).SingleOrDefault();
            return View(vendor);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Vendor auth = context.Vendors.Where(x => x.VendorId == id).
                SingleOrDefault();
            return View(auth);
        }
        [HttpPost]
        public ActionResult Delete(int id, Vendor a1)
        {
            var auth = context.Vendors.Where(x => x.VendorId == id).
                SingleOrDefault();
            context.Vendors.Remove(auth);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}