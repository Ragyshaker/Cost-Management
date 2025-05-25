using ERPtask.models;

namespace ERPtask.Repositrories.Interfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAll();
        Notification GetById(int id);
        Notification Add(Notification notification);
        void Update(Notification notification);
        bool Delete(int id);
        List<Notification> GetByInvoiceId(int invoiceId);
        List<Notification> GetByClientId(int clientId);
    }

}
