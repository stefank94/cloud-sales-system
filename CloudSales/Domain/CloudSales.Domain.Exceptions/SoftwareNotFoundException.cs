namespace CloudSales.Domain.Exceptions
{
    public class SoftwareNotFoundException : NotFoundException
    {
        public SoftwareNotFoundException(Guid softwareId) : base("Software", softwareId.ToString()) { }
    }
}
