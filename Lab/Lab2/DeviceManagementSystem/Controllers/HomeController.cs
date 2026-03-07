using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceManagementSystem.Data;
using DeviceManagementSystem.ViewModels;
using System.Linq;

namespace DeviceManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalDevices = _context.Devices.Count(),
                MaintenanceCount = _context.Devices.Count(d => d.Status == Models.DeviceStatus.Maintenance),
                BrokenCount = _context.Devices.Count(d => d.Status == Models.DeviceStatus.Broken),
                Devices = _context.Devices
                            .Include(d => d.DeviceCategory)
                            .OrderByDescending(d => d.DateOfEntry)
                            .Take(5)
                            .ToList()
            };

            return View(model);
        }
    }
}
