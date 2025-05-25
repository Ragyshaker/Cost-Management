using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using Microsoft.Data.SqlClient;


namespace ERPtask.Repositrories
{
    public class CostEntryRepository : ICostEntryRepository
    {
        private readonly string _connectionString;

        public CostEntryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CostEntry> GetAll()
        {
            var costEntries = new List<CostEntry>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM CostEntries", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        costEntries.Add(new CostEntry
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Description = reader.GetString(reader.GetOrdinal("Description"))
                        });
                    }
                }
            }
            return costEntries;
        }

        public CostEntry GetById(int id)
        {
            CostEntry costEntry = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM CostEntries WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        costEntry = new CostEntry
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Description = reader.GetString(reader.GetOrdinal("Description"))
                        };
                    }
                }
            }
            return costEntry;
        }
        public CostEntry Insert(CostEntry costEntry)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO CostEntries (Category, Amount, Date, Description) " +
                    "VALUES (@Category, @Amount, @Date, @Description); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);
                command.Parameters.AddWithValue("@Category", costEntry.Category);
                command.Parameters.AddWithValue("@Amount", costEntry.Amount);
                command.Parameters.AddWithValue("@Date", costEntry.Date);
                command.Parameters.AddWithValue("@Description", costEntry.Description);
                var newId = (int)command.ExecuteScalar();
                costEntry.Id = newId;
                return costEntry;
            }
        }

        public void Update(CostEntry costEntry)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE CostEntries SET Category = @Category, Amount = @Amount, " +
                    "Date = @Date, Description = @Description WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Category", costEntry.Category);
                command.Parameters.AddWithValue("@Amount", costEntry.Amount);
                command.Parameters.AddWithValue("@Date", costEntry.Date);
                command.Parameters.AddWithValue("@Description", costEntry.Description);
                command.Parameters.AddWithValue("@Id", costEntry.Id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("CostEntry not found");
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM CostEntries WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("CostEntry not found");
                }
            }
        }
    }

}

