using System.ComponentModel.DataAnnotations;

namespace Services.Product.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
