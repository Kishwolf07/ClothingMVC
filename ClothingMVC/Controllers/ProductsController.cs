using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClothingMVC.Data;
using ClothingMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ClothingMVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Where(p => p.Status == ProductStatus.Active)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return PartialView(product);
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            ModelState.Remove("ImagePath");

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    string path = Path.Combine(wwwRootPath, "images", fileName);

                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    product.ImagePath = fileName;
                }

                product.Status = ProductStatus.Active;
                _context.Add(product);
                await _context.SaveChangesAsync(); // Save first to get the ID

                _context.ActivityLogs.Add(new Activitylog
                {
                    AdminEmail = User.Identity.Name,
                    Action = "Create",
                    EntityName = product.Name,
                    Details = $"Created {product.Name} (ID: {product.Id}).",
                    Timestamp = DateTime.Now
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return PartialView(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.Id) return NotFound();

            ModelState.Remove("imageFile");
            ModelState.Remove("ImagePath");

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                        string path = Path.Combine(wwwRootPath, "images", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        product.ImagePath = fileName;
                    }

                    _context.Update(product);

                    _context.ActivityLogs.Add(new Activitylog
                    {
                        AdminEmail = User.Identity.Name,
                        Action = "Update",
                        EntityName = product.Name,
                        Details = $"Updated {product.Name} (ID: {product.Id}) details.",
                        Timestamp = DateTime.Now
                    });

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return PartialView(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Status = ProductStatus.Inactive;
                _context.Update(product);

                _context.ActivityLogs.Add(new Activitylog
                {
                    AdminEmail = User.Identity.Name,
                    Action = "Delete (Soft)",
                    EntityName = product.Name,
                    Details = $"Product {product.Name} (ID: {id}) was set to INACTIVE.",
                    Timestamp = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Prevent redundant restoration if already active
            if (product.Status == ProductStatus.Active)
                return RedirectToAction("Privacy", "Home");

            product.Status = ProductStatus.Active;
            _context.Update(product);

            _context.ActivityLogs.Add(new Activitylog
            {
                AdminEmail = User.Identity.Name,
                Action = "Restore",
                EntityName = product.Name,
                Details = $"Restored {product.Name} (ID: {id}) to ACTIVE status.",
                Timestamp = DateTime.Now
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Privacy", "Home");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}