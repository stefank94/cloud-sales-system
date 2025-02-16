namespace CloudSales.Domain.Models
{
    public class Order
    {
        public Guid SoftwareId { get; set; }
        public int Quantity { get; set; }
        public DateTime ValidTo { get; set; }
        public Guid AccountId { get; set; }
    }
}
