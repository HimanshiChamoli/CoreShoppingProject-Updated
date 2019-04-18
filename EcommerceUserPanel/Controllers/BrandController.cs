using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUserPanel.Controllers
{
    public class BrandController : Controller
    {
        //ShoppingDemoooo2Context context = new ShoppingDemoooo2Context();
        private readonly ShoppingDemoooo2Context _context;

        public BrandController(ShoppingDemoooo2Context context)
        {
            _context = context;
        }
        public ViewResult Index()
        {
            var brand = _context.Brands.ToList();
            return View(brand);
        }
        public async Task<IActionResult> ProductDisplay(int? id)
        {
            var p = _context.Products.Where(x => x.BrandId == id).ToList();
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)

        {
            if (id == null)
            {
                return BadRequest();
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)

            {
                return NotFound();
            }
            return Ok(brand);
        }
    }
}