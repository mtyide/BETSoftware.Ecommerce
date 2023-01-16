namespace BETSoftware.Domain.Models.Dtos
{
    public class OrderInDto
    {
        public int? CustomerId { get; set; }
        public bool? ShippingRequired { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderLines> Lines { get; set; }
        public bool? Active { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? ShippingTax { get; set; }
    }
}
