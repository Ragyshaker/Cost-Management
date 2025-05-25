using ERPtask.DTOs;

namespace ERPtask.servcies.Interfaces
{
    public interface IInvoiceService
    {
        List<InvoiceDto> GetAll();
        InvoiceDto GetById(int id);
        InvoiceDto Create(InvoiceDto invoiceDto);
        void Update(int id, InvoiceDto invoiceDto);
        void Delete(int id);
    }

}
