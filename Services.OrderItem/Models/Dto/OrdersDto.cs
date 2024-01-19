namespace Services.OrderItem.Models.Dto
{
    public class OrdersDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } 
        public string Status { get; set; }
    }
}
