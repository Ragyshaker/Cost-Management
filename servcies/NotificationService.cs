using ERPtask.DTOs;
using ERPtask.models;
using ERPtask.Repositrories;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies.Interfaces;
using System.Data.SqlClient;
using System.Net.Mail;
namespace ERPtask.servcies
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public List<NotificationDto> GetAll()
        {
            var notifications = _repository.GetAll();
            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                InvoiceId = n.InvoiceId,
                ClientId = n.ClientId,
                SentDate = n.SentDate,
                Message = n.Message
            }).ToList();
        }

        public NotificationDto GetById(int id)
        {
            var notification = _repository.GetById(id);
            if (notification == null)
                return null;
            return new NotificationDto
            {
                Id = notification.Id,
                InvoiceId = notification.InvoiceId,
                ClientId = notification.ClientId,
                SentDate = notification.SentDate,
                Message = notification.Message
            };
        }

        public NotificationDto Create(NotificationCreateDto dto)
        {
            var notification = new Notification
            {
                InvoiceId = dto.InvoiceId,
                ClientId = dto.ClientId,
                SentDate = DateTime.UtcNow,
                Message = dto.Message
            };

            var createdNotification = _repository.Add(notification);
            return new NotificationDto
            {
                Id = createdNotification.Id,
                InvoiceId = createdNotification.InvoiceId,
                ClientId = createdNotification.ClientId,
                SentDate = createdNotification.SentDate,
                Message = createdNotification.Message
            };
        }

        public void Update(NotificationUpdateDto dto)
        {
            var existing = _repository.GetById(dto.Id) ?? throw new KeyNotFoundException("Notification not found");

            var notification = new Notification
            {
                Id = dto.Id,
                InvoiceId = dto.InvoiceId,
                ClientId = dto.ClientId,
                SentDate = existing.SentDate, 
                Message = dto.Message
            };

            _repository.Update(notification);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<NotificationDto> GetByInvoiceId(int invoiceId)
        {
            return _repository.GetByInvoiceId(invoiceId)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    InvoiceId = n.InvoiceId,
                    ClientId = n.ClientId,
                    SentDate = n.SentDate,
                    Message = n.Message
                }).ToList();
        }

        public List<NotificationDto> GetByClientId(int clientId)
        {
            return _repository.GetByClientId(clientId)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    InvoiceId = n.InvoiceId,
                    ClientId = n.ClientId,
                    SentDate = n.SentDate,
                    Message = n.Message
                }).ToList();
        }
    }

}
