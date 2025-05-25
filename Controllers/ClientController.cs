using ERPtask.DTOs;
using ERPtask.models;
using ERPtask.servcies;
using ERPtask.servcies.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ERPtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ClientDto>> GetAll()
        {
            var clients = _service.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> GetById(int id)
        {
            var client = _service.GetById(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }


        [HttpPost]
        public ActionResult<ClientDto> Create([FromBody] ClientDto clientDto)
        {
            try
            {
                var createdClient = _service.Create(clientDto);
                return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClientDto clientDto)
        {
            if (id != clientDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                _service.Update(clientDto);
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

        [HttpGet("byemail/{email}")]
        public ActionResult<ClientDto> GetByEmail(string email)
        {
            var client = _service.GetByEmail(email);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
    }
}
