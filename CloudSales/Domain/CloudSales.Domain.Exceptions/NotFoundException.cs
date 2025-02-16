namespace CloudSales.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, string id) : base($"{entityName} not found with id {id}") { }
    }
    
}
