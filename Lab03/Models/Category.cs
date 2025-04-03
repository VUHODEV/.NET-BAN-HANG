using System.ComponentModel.DataAnnotations;

namespace Lab03.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        public ICollection<Product>? Products { get; set; }
    }
}
