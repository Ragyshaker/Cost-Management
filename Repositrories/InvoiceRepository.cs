using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using Microsoft.Data.SqlClient;


namespace ERPtask.Repositrories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly string _connectionString;

        public InvoiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Invoice> GetAll()
        {
            var invoices = new List<Invoice>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Invoices", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new Invoice
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            Taxes = reader.GetDecimal(reader.GetOrdinal("Taxes")),
                            Discounts = reader.GetDecimal(reader.GetOrdinal("Discounts"))
                        });
                    }
                }
            }
            return invoices;
        }

        public Invoice GetById(int id)
        {
            Invoice invoice = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Invoices WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoice = new Invoice
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            Taxes = reader.GetDecimal(reader.GetOrdinal("Taxes")),
                            Discounts = reader.GetDecimal(reader.GetOrdinal("Discounts"))
                        };
                    }
                }
            }
            return invoice;
        }


        public Invoice Insert(Invoice invoice)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Invoices (ClientId, Date, TotalAmount, Taxes, Discounts) " +
                    "VALUES (@ClientId, @Date, @TotalAmount, @Taxes, @Discounts); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);
                command.Parameters.AddWithValue("@ClientId", invoice.ClientId);
                command.Parameters.AddWithValue("@Date", invoice.Date);
                command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                command.Parameters.AddWithValue("@Taxes", invoice.Taxes);
                command.Parameters.AddWithValue("@Discounts", invoice.Discounts);
                var newId = (int)command.ExecuteScalar();
                invoice.Id = newId;
                return invoice;
            }
        }

        public void Update(Invoice invoice)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Invoices SET ClientId = @ClientId, Date = @Date, " +
                    "TotalAmount = @TotalAmount, Taxes = @Taxes, Discounts = @Discounts " +
                    "WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@ClientId", invoice.ClientId);
                command.Parameters.AddWithValue("@Date", invoice.Date);
                command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                command.Parameters.AddWithValue("@Taxes", invoice.Taxes);
                command.Parameters.AddWithValue("@Discounts", invoice.Discounts);
                command.Parameters.AddWithValue("@Id", invoice.Id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Invoice not found");
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Invoices WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Invoice not found");
                }
            }
        }
    }

}
