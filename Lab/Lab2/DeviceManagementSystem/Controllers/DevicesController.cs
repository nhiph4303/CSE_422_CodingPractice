using DeviceManagementSystem.Data;
using DeviceManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace DeviceManagementSystem.Controllers
{
    public class DevicesController : Controller
    {
        private readonly AppDbContext _context;

        public DevicesController(AppDbContext context)
        {
            _context = context;
        }

        //Index-search-filter
        public async Task<IActionResult> Index(
            string search,
            DeviceStatus? status,
            int? categoryId
        )
        {
            var query = _context.Devices
                .Include(d => d.DeviceCategory)
                .AsQueryable();

            //search by name-code
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d =>
                    d.DeviceName.Contains(search) ||
                    d.DeviceCode.Contains(search)
                );
            }

            //Filter status
            if (status.HasValue)
            {
                query = query.Where(d => d.Status == status.Value);
            }

            //Filter category
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(d => d.DeviceCategoryId == categoryId);
            }

            var devices = await query
                .OrderByDescending(d => d.DateOfEntry)
                .ToListAsync();

            //filter dropdowns
            ViewBag.Categories = new SelectList(
                _context.DeviceCategories.ToList(),
                "Id",
                "Name",
                categoryId
            );

            ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(DeviceStatus))
                    .Cast<DeviceStatus>()
                    .Select(s => new { Id = s, Name = s.ToString() }),
                "Id",
                "Name",
                status
            );

            ViewBag.Search = search;

            return View(devices);
        }

        //create(get)
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                _context.DeviceCategories.ToList(),
                "Id",
                "Name"
            );

            return View(new Device());
        }

        //create(post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    _context.DeviceCategories.ToList(),
                    "Id",
                    "Name",
                    device.DeviceCategoryId
                );
                return View(device);
            }

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Device added successfully!";
            return RedirectToAction(nameof(Index));
        }

        //Edit(get)
        public async Task<IActionResult> Edit(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            ViewBag.Categories = new SelectList(
                _context.DeviceCategories.ToList(),
                "Id",
                "Name",
                device.DeviceCategoryId
            );

            return View(device);
        }

        //edit(post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Device device)
        {
            if (id != device.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    _context.DeviceCategories.ToList(),
                    "Id",
                    "Name",
                    device.DeviceCategoryId
                );
                return View(device);
            }

            _context.Update(device);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Device updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        //delete(post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Device deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
