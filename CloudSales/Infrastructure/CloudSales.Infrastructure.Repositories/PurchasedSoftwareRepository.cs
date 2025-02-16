using CloudSales.Domain.Enums;
using CloudSales.Domain.Models;
using CloudSales.Domain.Repositories;
using Npgsql;

namespace CloudSales.Infrastructure.Repositories
{
    public class PurchasedSoftwareRepository : IPurchasedSoftwareRepository
    {
        private readonly NpgsqlConnection _sqlConnection;

        public PurchasedSoftwareRepository(NpgsqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        private const int PageSize = 10;

        public async Task<IEnumerable<PurchasedSoftware>> GetPurchasedSoftwareForAccountAsync(Guid accountId, int page)
        {
            string query = "SELECT id, software_id, quantity, name, valid_to, state FROM purchased_software WHERE account_id = @account_id ORDER BY valid_to desc OFFSET ((@page - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY";

            var cmd = new NpgsqlCommand(query, _sqlConnection);
            cmd.Parameters.Add(new NpgsqlParameter("account_id", accountId));
            cmd.Parameters.Add(new NpgsqlParameter("pageSize", PageSize));
            cmd.Parameters.Add(new NpgsqlParameter("page", page));

            if (_sqlConnection.State != System.Data.ConnectionState.Open)
            {
                await _sqlConnection.OpenAsync();
            }

            using var reader = await cmd.ExecuteReaderAsync();

            var purchasedSoftware = new List<PurchasedSoftware>();

            while (await reader.ReadAsync())
            {
                var id = reader.GetGuid(0);
                var softwareId = reader.GetGuid(1);
                var quantity = reader.GetInt32(2);
                var name = reader.GetString(3);
                var validTo = reader.GetDateTime(4);
                var state = reader.GetInt32(5);

                purchasedSoftware.Add(new PurchasedSoftware()
                {
                    Id = id,
                    AccountId = accountId,
                    Name = name,
                    Quantity = quantity,
                    SoftwareId = softwareId,
                    ValidTo = validTo,
                    State = (PurchasedSoftwareState) state
                });
            }

            return purchasedSoftware;
        }

        public async Task InsertPurchasedSoftwareAsync(PurchasedSoftware purchasedSoftware)
        {
            string commandText = "INSERT INTO purchased_software (id, software_id, quantity, name, valid_to, account_id, state) values (@id, @software_id, @quantity, @name, @valid_to, @account_id, @state)";
            
            var cmd = new NpgsqlCommand(commandText, _sqlConnection);
            cmd.Parameters.Add(new NpgsqlParameter("id", purchasedSoftware.Id));
            cmd.Parameters.Add(new NpgsqlParameter("software_id", purchasedSoftware.SoftwareId));
            cmd.Parameters.Add(new NpgsqlParameter("quantity", purchasedSoftware.Quantity));
            cmd.Parameters.Add(new NpgsqlParameter("name", purchasedSoftware.Name));
            cmd.Parameters.Add(new NpgsqlParameter("valid_to", purchasedSoftware.ValidTo));
            cmd.Parameters.Add(new NpgsqlParameter("account_id", purchasedSoftware.AccountId));
            cmd.Parameters.Add(new NpgsqlParameter("state", (int) purchasedSoftware.State));

            if (_sqlConnection.State != System.Data.ConnectionState.Open)
            {
                await _sqlConnection.OpenAsync();
            }

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
