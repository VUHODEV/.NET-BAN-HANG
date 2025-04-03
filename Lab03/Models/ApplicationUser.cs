using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab03.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        public string? Address { get; set; }
        public string? Age { get; set; }
    }
}
