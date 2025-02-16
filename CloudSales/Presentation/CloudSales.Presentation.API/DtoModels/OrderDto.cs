namespace CloudSales.Presentation.API.DtoModels
{
    public class OrderDto
    {
        public Guid SoftwareId { get; set; }
        public int Quantity { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
