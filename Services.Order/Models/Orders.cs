using System.ComponentModel.DataAnnotations;

namespace Services.Order.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }
}
