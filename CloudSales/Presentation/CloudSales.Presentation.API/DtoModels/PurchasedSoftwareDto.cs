using CloudSales.Domain.Enums;

namespace CloudSales.Presentation.API.DtoModels
{
    public class PurchasedSoftwareDto
    {
        public Guid Id { get; set; }
        public Guid SoftwareId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public DateTime ValidTo { get; set; }
        public PurchasedSoftwareState State { get; set; }
    }
}
