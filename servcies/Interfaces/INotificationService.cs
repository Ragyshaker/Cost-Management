using ERPtask.DTOs;

namespace ERPtask.servcies.Interfaces
{
    public interface INotificationService
    {
        List<NotificationDto> GetAll();
        NotificationDto GetById(int id);
        NotificationDto Create(NotificationCreateDto dto);
        void Update(NotificationUpdateDto dto);
        bool Delete(int id);
        List<NotificationDto> GetByInvoiceId(int invoiceId);
        List<NotificationDto> GetByClientId(int clientId);
    }

}
