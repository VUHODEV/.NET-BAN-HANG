namespace Lab03.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
