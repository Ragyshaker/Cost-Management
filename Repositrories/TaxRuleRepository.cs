using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using Microsoft.Data.SqlClient;

namespace ERPtask.Repositrories
{
    public class TaxRuleRepository : ITaxRuleRepository
    {
        private readonly string _connectionString;

        public TaxRuleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TaxRule> GetAll()
        {
            var taxRules = new List<TaxRule>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM TaxRules", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taxRules.Add(new TaxRule
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Region = reader.GetString(reader.GetOrdinal("Region")),
                            TaxRate = reader.GetDecimal(reader.GetOrdinal("TaxRate"))
                        });
                    }
                }
            }
            return taxRules;
        }

        public TaxRule GetById(int id)
        {
            TaxRule taxRule = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM TaxRules WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        taxRule = new TaxRule
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Region = reader.GetString(reader.GetOrdinal("Region")),
                            TaxRate = reader.GetDecimal(reader.GetOrdinal("TaxRate"))
                        };
                    }
                }
            }
            return taxRule;
        }

        public TaxRule Add(TaxRule taxRule)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO TaxRules (Region, TaxRate) " +
                    "OUTPUT INSERTED.Id, INSERTED.Region, INSERTED.TaxRate " +
                    "VALUES (@Region, @TaxRate)", connection);

                command.Parameters.AddWithValue("@Region", taxRule.Region);
                command.Parameters.AddWithValue("@TaxRate", taxRule.TaxRate);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TaxRule
                        {
                            Id = reader.GetInt32(0),
                            Region = reader.GetString(1),
                            TaxRate = reader.GetDecimal(2)
                        };
                    }
                    throw new Exception("Failed to create tax rule");
                }
            }
        }

        public void Update(TaxRule taxRule)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE TaxRules SET Region = @Region, TaxRate = @TaxRate " +
                    "WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", taxRule.Id);
                command.Parameters.AddWithValue("@Region", taxRule.Region);
                command.Parameters.AddWithValue("@TaxRate", taxRule.TaxRate);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                    throw new KeyNotFoundException("Tax rule not found");
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM TaxRules WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public TaxRule GetByRegion(string region)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM TaxRules WHERE Region = @Region", connection);
                command.Parameters.AddWithValue("@Region", region);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TaxRule
                        {
                            Id = reader.GetInt32(0),
                            Region = reader.GetString(1),
                            TaxRate = reader.GetDecimal(2)
                        };
                    }
                    return null;
                }
            }
        }
    }

}
