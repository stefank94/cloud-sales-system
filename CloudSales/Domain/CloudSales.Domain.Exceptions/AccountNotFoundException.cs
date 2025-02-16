namespace CloudSales.Domain.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(Guid accountId) : base("Account", accountId.ToString()) { }
    }
}
