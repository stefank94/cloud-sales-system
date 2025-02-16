using CloudSales.Domain.Enums;

namespace CloudSales.Domain.Models
{
    public class PurchasedSoftware
    {
        public Guid Id { get; set; }
        public Guid SoftwareId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime ValidTo { get; set; }
        public Guid AccountId { get; set; }
        public PurchasedSoftwareState State { get; set; } = PurchasedSoftwareState.Active;
    }
}
