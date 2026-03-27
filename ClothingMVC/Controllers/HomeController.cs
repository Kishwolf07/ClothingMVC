using System.Diagnostics;
using ClothingMVC.Models;
using ClothingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ClothingMVC.Controllers
{
    [Authorize]
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
            var activeProducts = _context.Products.Where(p => p.Status == ProductStatus.Active);

            ViewBag.TotalProductTypes = await activeProducts.CountAsync();
            ViewBag.TotalStock = await activeProducts.SumAsync(p => p.Quantity);

            var brandData = await activeProducts
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

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var logs = await _context.ActivityLogs
                .OrderByDescending(l => l.Timestamp)
                .Take(50)
                .ToListAsync();

            // Get IDs of products that are currently Inactive
            ViewBag.InactiveProductIds = await _context.Products
                .Where(p => p.Status == ProductStatus.Inactive)
                .Select(p => p.Id)
                .ToListAsync();

            return View(logs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}