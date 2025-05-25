using ERPtask.DTOs;
using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies.Interfaces;

namespace ERPtask.servcies
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IInvoiceItemRepository _repository;

        public InvoiceItemService(IInvoiceItemRepository repository)
        {
            _repository = repository;
        }

        public List<InvoiceItemDto> GetAll()
        {
            var invoiceItems = _repository.GetAll();
            return invoiceItems.Select(ii => new InvoiceItemDto
            {
                Id = ii.Id,
                InvoiceId = ii.InvoiceId,
                Description = ii.Description,
                Quantity = ii.Quantity,
                UnitPrice = ii.UnitPrice
            }).ToList();
        }

        public InvoiceItemDto GetById(int id)
        {
            var invoiceItem = _repository.GetById(id);
            if (invoiceItem == null)
                return null;
            return new InvoiceItemDto
            {
                Id = invoiceItem.Id,
                InvoiceId = invoiceItem.InvoiceId,
                Description = invoiceItem.Description,
                Quantity = invoiceItem.Quantity,
                UnitPrice = invoiceItem.UnitPrice
            };
        }

        public InvoiceItemDto Create(InvoiceItemDto invoiceItemDto)
        {
            var invoiceItem = new InvoiceItem
            {
                InvoiceId = invoiceItemDto.InvoiceId,
                Description = invoiceItemDto.Description,
                Quantity = invoiceItemDto.Quantity,
                UnitPrice = invoiceItemDto.UnitPrice
            };
            var createdItem = _repository.Add(invoiceItem);
            return new InvoiceItemDto
            {
                Id = createdItem.Id,
                InvoiceId = createdItem.InvoiceId,
                Description = createdItem.Description,
                Quantity = createdItem.Quantity,
                UnitPrice = createdItem.UnitPrice
            };
        }

        public void Update(InvoiceItemDto invoiceItemDto)
        {
            var invoiceItem = new InvoiceItem
            {
                Id = invoiceItemDto.Id,
                InvoiceId = invoiceItemDto.InvoiceId,
                Description = invoiceItemDto.Description,
                Quantity = invoiceItemDto.Quantity,
                UnitPrice = invoiceItemDto.UnitPrice
            };
            _repository.Update(invoiceItem);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<InvoiceItemDto> GetByInvoiceId(int invoiceId)
        {
            var invoiceItems = _repository.GetByInvoiceId(invoiceId);
            return invoiceItems.Select(ii => new InvoiceItemDto
            {
                Id = ii.Id,
                InvoiceId = ii.InvoiceId,
                Description = ii.Description,
                Quantity = ii.Quantity,
                UnitPrice = ii.UnitPrice
            }).ToList();
        }

        public decimal GetTotalAmountByInvoiceId(int invoiceId)
        {
            var invoiceItems = _repository.GetByInvoiceId(invoiceId);
            return invoiceItems.Sum(ii => ii.Quantity * ii.UnitPrice);
        }
    }
}
