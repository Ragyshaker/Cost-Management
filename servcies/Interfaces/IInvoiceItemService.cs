using ERPtask.DTOs;

namespace ERPtask.servcies.Interfaces
{
    public interface IInvoiceItemService
    {
        List<InvoiceItemDto> GetAll();
        InvoiceItemDto GetById(int id);
        InvoiceItemDto Create(InvoiceItemDto invoiceItemDto);
        void Update(InvoiceItemDto invoiceItemDto);
        bool Delete(int id);
        List<InvoiceItemDto> GetByInvoiceId(int invoiceId);
        decimal GetTotalAmountByInvoiceId(int invoiceId);
    }
}
