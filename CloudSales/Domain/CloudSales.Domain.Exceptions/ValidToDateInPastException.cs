namespace CloudSales.Domain.Exceptions
{
    public class ValidToDateInPastException : ValidationException
    {
        public ValidToDateInPastException() : base("Valid to date must be in the future")
        {
        }
    }
}
