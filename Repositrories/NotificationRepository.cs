using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using Microsoft.Data.SqlClient;


namespace ERPtask.Repositrories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Notification> GetAll()
        {
            var notifications = new List<Notification>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notifications", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                            ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                            SentDate = reader.GetDateTime(reader.GetOrdinal("SentDate")),
                            Message = reader.GetString(reader.GetOrdinal("Message"))
                        });
                    }
                }
            }
            return notifications;
        }

        public Notification GetById(int id)
        {
            Notification notification = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        notification = new Notification
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                            ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                            SentDate = reader.GetDateTime(reader.GetOrdinal("SentDate")),
                            Message = reader.GetString(reader.GetOrdinal("Message"))
                        };
                    }
                }
            }
            return notification;
        }
        public Notification Add(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Notifications (InvoiceId, ClientId, SentDate, Message) " +
                    "OUTPUT INSERTED.Id, INSERTED.InvoiceId, INSERTED.ClientId, INSERTED.SentDate, INSERTED.Message " +
                    "VALUES (@InvoiceId, @ClientId, @SentDate, @Message)", connection);

                command.Parameters.AddWithValue("@InvoiceId", notification.InvoiceId);
                command.Parameters.AddWithValue("@ClientId", notification.ClientId);
                command.Parameters.AddWithValue("@SentDate", notification.SentDate);
                command.Parameters.AddWithValue("@Message", notification.Message);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Notification
                        {
                            Id = reader.GetInt32(0),
                            InvoiceId = reader.GetInt32(1),
                            ClientId = reader.GetInt32(2),
                            SentDate = reader.GetDateTime(3),
                            Message = reader.GetString(4)
                        };
                    }
                    throw new Exception("Failed to create notification");
                }
            }
        }

        public void Update(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Notifications SET " +
                    "InvoiceId = @InvoiceId, " +
                    "ClientId = @ClientId, " +
                    "SentDate = @SentDate, " +
                    "Message = @Message " +
                    "WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", notification.Id);
                command.Parameters.AddWithValue("@InvoiceId", notification.InvoiceId);
                command.Parameters.AddWithValue("@ClientId", notification.ClientId);
                command.Parameters.AddWithValue("@SentDate", notification.SentDate);
                command.Parameters.AddWithValue("@Message", notification.Message);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                    throw new KeyNotFoundException("Notification not found");
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Notifications WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public List<Notification> GetByInvoiceId(int invoiceId)
        {
            var notifications = new List<Notification>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE InvoiceId = @InvoiceId", connection);
                command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(0),
                            InvoiceId = reader.GetInt32(1),
                            ClientId = reader.GetInt32(2),
                            SentDate = reader.GetDateTime(3),
                            Message = reader.GetString(4)
                        });
                    }
                }
            }
            return notifications;
        }

        public List<Notification> GetByClientId(int clientId)
        {
            var notifications = new List<Notification>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE ClientId = @ClientId", connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(0),
                            InvoiceId = reader.GetInt32(1),
                            ClientId = reader.GetInt32(2),
                            SentDate = reader.GetDateTime(3),
                            Message = reader.GetString(4)
                        });
                    }
                }
            }
            return notifications;
        }
    }
}
