using System.ComponentModel.DataAnnotations;

namespace Services.Customer.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
