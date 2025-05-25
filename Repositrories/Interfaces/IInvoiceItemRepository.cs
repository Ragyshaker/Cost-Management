using ERPtask.models;

namespace ERPtask.Repositrories.Interfaces
{
    public interface IInvoiceItemRepository
    {
        List<InvoiceItem> GetAll();
        InvoiceItem GetById(int id);
        InvoiceItem Add(InvoiceItem invoiceItem);
        void Update(InvoiceItem invoiceItem);
        bool Delete(int id);
        List<InvoiceItem> GetByInvoiceId(int invoiceId);
    }
}
