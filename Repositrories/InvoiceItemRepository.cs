using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using Microsoft.Data.SqlClient;


namespace ERPtask.Repositrories
{

    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly string _connectionString;

        public InvoiceItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<InvoiceItem> GetAll()
        {
            var invoiceItems = new List<InvoiceItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM InvoiceItems", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoiceItems.Add(new InvoiceItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        });
                    }
                }
            }
            return invoiceItems;
        }

        public InvoiceItem GetById(int id)
        {
            InvoiceItem invoiceItem = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM InvoiceItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoiceItem = new InvoiceItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        };
                    }
                }
            }
            return invoiceItem;
        }


        public InvoiceItem Add(InvoiceItem invoiceItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO InvoiceItems (InvoiceId, Description, Quantity, UnitPrice) " +
                    "OUTPUT INSERTED.Id, INSERTED.InvoiceId, INSERTED.Description, INSERTED.Quantity, INSERTED.UnitPrice " +
                    "VALUES (@InvoiceId, @Description, @Quantity, @UnitPrice);", connection);
                command.Parameters.AddWithValue("@InvoiceId", invoiceItem.InvoiceId);
                command.Parameters.AddWithValue("@Description", invoiceItem.Description);
                command.Parameters.AddWithValue("@Quantity", invoiceItem.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", invoiceItem.UnitPrice);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new InvoiceItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        };
                    }
                    throw new Exception("Failed to create InvoiceItem.");
                }
            }
        }

        public void Update(InvoiceItem invoiceItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE InvoiceItems SET InvoiceId = @InvoiceId, Description = @Description, " +
                    "Quantity = @Quantity, UnitPrice = @UnitPrice WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", invoiceItem.Id);
                command.Parameters.AddWithValue("@InvoiceId", invoiceItem.InvoiceId);
                command.Parameters.AddWithValue("@Description", invoiceItem.Description);
                command.Parameters.AddWithValue("@Quantity", invoiceItem.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", invoiceItem.UnitPrice);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                    throw new KeyNotFoundException("InvoiceItem not found.");
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM InvoiceItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public List<InvoiceItem> GetByInvoiceId(int invoiceId)
        {
            var invoiceItems = new List<InvoiceItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM InvoiceItems WHERE InvoiceId = @InvoiceId", connection);
                command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoiceItems.Add(new InvoiceItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        });
                    }
                }
            }
            return invoiceItems;
        }

    }

}
