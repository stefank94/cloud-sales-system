using CloudSales.Domain.Models;
using CloudSales.Domain.Repositories;
using Npgsql;

namespace CloudSales.Infrastructure.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly NpgsqlConnection _sqlConnection;

        public AccountsRepository(NpgsqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        private const int PageSize = 10;

        public async Task<IEnumerable<Account>> GetAccountsForCustomerAsync(Guid customerId, int page)
        {
            string query = "SELECT id, name FROM account WHERE customer_id = @customer_id ORDER BY name OFFSET ((@page - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY";

            var cmd = new NpgsqlCommand(query, _sqlConnection);
            cmd.Parameters.Add(new NpgsqlParameter("customer_id", customerId));
            cmd.Parameters.Add(new NpgsqlParameter("pageSize", PageSize));
            cmd.Parameters.Add(new NpgsqlParameter("page", page));

            if (_sqlConnection.State != System.Data.ConnectionState.Open)
            {
                await _sqlConnection.OpenAsync();
            }

            using var reader = await cmd.ExecuteReaderAsync();

            var accounts = new List<Account>();

            while (await reader.ReadAsync())
            {
                Guid id = reader.GetGuid(0);
                string name = reader.GetString(1);
                accounts.Add(new Account() { Id = id, Name = name });
            }

            return accounts;
        }

        public async Task<Account?> GetAccountByIdAsync(Guid accountId)
        {
            string query = "SELECT id, name FROM account WHERE id = @id";

            var cmd = new NpgsqlCommand(query, _sqlConnection);
            cmd.Parameters.Add(new NpgsqlParameter("id", accountId));

            if (_sqlConnection.State != System.Data.ConnectionState.Open)
            {
                await _sqlConnection.OpenAsync();
            }

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Guid id = reader.GetGuid(0);
                string name = reader.GetString(1);

                return new Account()
                {
                    Id = id,
                    Name = name
                };
            }

            return null;
        }
    }
}
