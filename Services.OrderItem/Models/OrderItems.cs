using System.ComponentModel.DataAnnotations;

namespace Services.OrderItem.Models
{
    public class OrderItems
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
