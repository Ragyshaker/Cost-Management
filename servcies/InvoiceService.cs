using ERPtask.models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ERPtask.Repositrories;
using ERPtask.Controllers;
using ERPtask.DTOs;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies.Interfaces;
namespace ERPtask.servcies
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceService(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public List<InvoiceDto> GetAll()
        {
            var invoices = _repository.GetAll();
            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                ClientId = i.ClientId,
                Date = i.Date,
                TotalAmount = i.TotalAmount,
                Taxes = i.Taxes,
                Discounts = i.Discounts
            }).ToList();
        }

        public InvoiceDto GetById(int id)
        {
            var invoice = _repository.GetById(id);
            if (invoice == null)
                return null;
            return new InvoiceDto
            {
                Id = invoice.Id,
                ClientId = invoice.ClientId,
                Date = invoice.Date,
                TotalAmount = invoice.TotalAmount,
                Taxes = invoice.Taxes,
                Discounts = invoice.Discounts
            };
        }

        public InvoiceDto Create(InvoiceDto invoiceDto)
        {
            if (invoiceDto.ClientId <= 0 || invoiceDto.Date == default(DateTime))
            {
                throw new ArgumentException("ClientId and Date are required.");
            }

            var invoice = new Invoice
            {
                ClientId = invoiceDto.ClientId,
                Date = invoiceDto.Date,
                TotalAmount = invoiceDto.TotalAmount,
                Taxes = invoiceDto.Taxes,
                Discounts = invoiceDto.Discounts
            };

            var insertedInvoice = _repository.Insert(invoice);
            return new InvoiceDto
            {
                Id = insertedInvoice.Id,
                ClientId = insertedInvoice.ClientId,
                Date = insertedInvoice.Date,
                TotalAmount = insertedInvoice.TotalAmount,
                Taxes = insertedInvoice.Taxes,
                Discounts = insertedInvoice.Discounts
            };
        }

        public void Update(int id, InvoiceDto invoiceDto)
        {
            if (invoiceDto.ClientId <= 0 || invoiceDto.Date == default(DateTime))
            {
                throw new ArgumentException("ClientId and Date are required.");
            }

            var existingInvoice = _repository.GetById(id);
            if (existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }

            existingInvoice.ClientId = invoiceDto.ClientId;
            existingInvoice.Date = invoiceDto.Date;
            existingInvoice.TotalAmount = invoiceDto.TotalAmount;
            existingInvoice.Taxes = invoiceDto.Taxes;
            existingInvoice.Discounts = invoiceDto.Discounts;

            _repository.Update(existingInvoice);
        }

        public void Delete(int id)
        {
            var existingInvoice = _repository.GetById(id);
            if (existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }
            _repository.Delete(id);
        }
    }

}

