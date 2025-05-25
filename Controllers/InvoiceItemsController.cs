using ERPtask.DTOs;
using ERPtask.servcies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERPtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly IInvoiceItemService _service;

        public InvoiceItemsController(IInvoiceItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<InvoiceItemDto>> GetAll()
        {
            var invoiceItems = _service.GetAll();
            return Ok(invoiceItems);
        }

        [HttpGet("{id}")]
        public ActionResult<InvoiceItemDto> GetById(int id)
        {
            var invoiceItem = _service.GetById(id);
            if (invoiceItem == null)
                return NotFound();
            return Ok(invoiceItem);
        }

        [HttpPost]
        public ActionResult<InvoiceItemDto> Create(InvoiceItemDto invoiceItemDto)
        {
            try
            {
                var createdItem = _service.Create(invoiceItemDto);
                return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, InvoiceItemDto invoiceItemDto)
        {
            if (id != invoiceItemDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                _service.Update(invoiceItemDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpGet("byinvoice/{invoiceId}")]
        public ActionResult<List<InvoiceItemDto>> GetByInvoiceId(int invoiceId)
        {
            var items = _service.GetByInvoiceId(invoiceId);
            return Ok(items);
        }

        [HttpGet("byinvoice/{invoiceId}/total")]
        public ActionResult<decimal> GetTotalByInvoiceId(int invoiceId)
        {
            var total = _service.GetTotalAmountByInvoiceId(invoiceId);
            return Ok(total);
        }
    }
}
