namespace Lab03.Models;
public class OrderHistory
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; }
    public string ShippingAddress { get; set; }
    public string Notes { get; set; }
    public decimal TotalAmount { get; set; }
}