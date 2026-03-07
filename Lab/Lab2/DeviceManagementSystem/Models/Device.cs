using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceCode { get; set; }

        [Required]
        public int DeviceCategoryId { get; set; }

        public DeviceCategory? DeviceCategory { get; set; }

        [Required]
        public DeviceStatus Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfEntry { get; set; } = DateTime.Now;
    }

    public enum DeviceStatus
    {
        Active,
        Maintenance,
        Broken
    }
}
