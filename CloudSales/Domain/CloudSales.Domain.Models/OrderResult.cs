namespace CloudSales.Domain.Models
{
    public class OrderResult
    {
        public Guid OrderId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
