using Microsoft.AspNetCore.Mvc;
using ERPtask.models;
using ERPtask.servcies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ERPtask.DTOs;
using ERPtask.servcies.Interfaces;
namespace ERPtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<InvoiceDto>> GetAll()
        {
            var invoices = _service.GetAll();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public ActionResult<InvoiceDto> GetById(int id)
        {
            var invoice = _service.GetById(id);
            if (invoice == null)
                return NotFound();
            return Ok(invoice);
        }
        [HttpPost]
        public ActionResult<InvoiceDto> Create([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                var createdInvoice = _service.Create(invoiceDto);
                return CreatedAtAction(nameof(GetById), new { id = createdInvoice.Id }, createdInvoice);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the invoice.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                _service.Update(id, invoiceDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) when (ex.Message == "Invoice not found")
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the invoice.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message == "Invoice not found")
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the invoice.");
            }
        }
    }
}
