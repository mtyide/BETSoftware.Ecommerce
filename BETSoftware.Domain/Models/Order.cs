using System.ComponentModel.DataAnnotations;

namespace BETSoftware.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? CustomerId { get; set; }
        public bool? ShippingRequired { get; set; }
        [MaxLength(500)]
        public string ShippingAddress { get; set; }
        public List<OrderLines> Lines { get; set; }
        public bool? Active { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? ShippingTax { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
