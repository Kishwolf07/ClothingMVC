using System.Diagnostics;
using ClothingMVC.Models;
using ClothingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; 

namespace ClothingMVC.Controllers
{
    [Authorize] // Forces redirect to Login if the user is not signed in
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Dashboard metrics
            ViewBag.TotalProductTypes = await _context.Products.CountAsync();
            ViewBag.TotalStock = await _context.Products.SumAsync(p => p.Quantity);

            // Data for Chart.js by Brand
            var brandData = await _context.Products
                .GroupBy(p => p.Brand)
                .Select(g => new {
                    Brand = g.Key.ToString(),
                    Total = g.Sum(p => p.Quantity)
                })
                .ToListAsync();

            ViewBag.BrandNames = brandData.Select(b => b.Brand).ToList();
            ViewBag.BrandTotals = brandData.Select(b => b.Total).ToList();

            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}