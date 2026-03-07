using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class DeviceCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Device>? Devices { get; set; }
    }
}
