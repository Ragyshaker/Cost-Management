using Microsoft.AspNetCore.Mvc;
using ERPtask.servcies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ERPtask.DTOs;
using ERPtask.servcies.Interfaces;
namespace ERPtask.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<NotificationDto>> GetAll()
        {
            var notifications = _service.GetAll();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public ActionResult<NotificationDto> GetById(int id)
        {
            var notification = _service.GetById(id);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }
        [HttpPost]
        public ActionResult<NotificationDto> Create([FromBody] NotificationCreateDto dto)
        {
            try
            {
                var notification = _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] NotificationUpdateDto dto)
        {
            try
            {
                _service.Update(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("by-invoice/{invoiceId}")]
        public ActionResult<List<NotificationDto>> GetByInvoiceId(int invoiceId)
        {
            return Ok(_service.GetByInvoiceId(invoiceId));
        }

        [HttpGet("by-client/{clientId}")]
        public ActionResult<List<NotificationDto>> GetByClientId(int clientId)
        {
            return Ok(_service.GetByClientId(clientId));
        }
    }
}
