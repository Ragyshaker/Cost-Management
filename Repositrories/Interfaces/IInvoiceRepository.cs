using ERPtask.models;

namespace ERPtask.Repositrories.Interfaces
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        Invoice GetById(int id);
        Invoice Insert(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(int id);
    }
}
