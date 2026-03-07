using System.Collections.Generic;
using DeviceManagementSystem.Models;

namespace DeviceManagementSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalDevices { get; set; }
        public int MaintenanceCount { get; set; }
        public int BrokenCount { get; set; }

        public List<Device> Devices { get; set; }
    }
}
