using DeviceManagementSystem.Data;
using DeviceManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagementSystem.Controllers
{
    public class DeviceCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public DeviceCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        //Index and search
        public async Task<IActionResult> Index(string search)
        {
            var query = _context.DeviceCategories
                .Include(c => c.Devices)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search));
                ViewBag.Search = search;
            }

            var categories = await query.ToListAsync();
            return View(categories);
        }

        //create(get)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //create(post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceCategory category)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState INVALID");
                return View(category);
            }

            Console.WriteLine("Create category: " + category.Name);

            _context.DeviceCategories.Add(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Category created successfully!";
            return RedirectToAction(nameof(Index));
        }

        //delete(get)
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.DeviceCategories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        //edit(post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeviceCategory category)
        {
            if (id != category.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            _context.Update(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        //delete(get)
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.DeviceCategories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        //delete(post)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.DeviceCategories
                .Include(c => c.Devices)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            if (category.Devices.Any())
            {
                TempData["Error"] = "Cannot delete category that has devices!";
                return RedirectToAction(nameof(Index));
            }

            _context.DeviceCategories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
