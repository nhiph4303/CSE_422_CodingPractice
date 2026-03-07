using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    // Định nghĩa Role
    public enum UserRole
    {
        [Display(Name = "Employee")]
        Employee = 0,

        [Display(Name = "Administrator")]
        Admin = 1
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        // Thuộc tính Role mới
        [Required]
        [Display(Name = "System Role")]
        public UserRole Role { get; set; } = UserRole.Employee; // Mặc định là nhân viên
    }
}