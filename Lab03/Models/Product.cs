using System.ComponentModel.DataAnnotations;

namespace Lab03.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        public string ImgUrl { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        public List<ProductImage>? Images { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
