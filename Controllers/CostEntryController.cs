using Microsoft.AspNetCore.Mvc;
using ERPtask.servcies;
using ERPtask.models;
using System.ComponentModel.DataAnnotations;
using ERPtask.DTOs;
using ERPtask.servcies.Interfaces;
namespace ERPtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostEntriesController : ControllerBase
    {
        private readonly ICostEntryService _service;

        public CostEntriesController(ICostEntryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<CostEntryDto>> GetAll()
        {
            var costEntries = _service.GetAll();
            return Ok(costEntries);
        }

        [HttpGet("{id}")]
        public ActionResult<CostEntryDto> GetById(int id)
        {
            var costEntry = _service.GetById(id);
            if (costEntry == null)
                return NotFound();
            return Ok(costEntry);
        }
        [HttpPost]
        public ActionResult<CostEntryDto> Create(CostEntryDto costEntryDto)
        {
            try
            {
                var createdCostEntry = _service.Create(costEntryDto);
                return CreatedAtAction(nameof(GetById), new { id = createdCostEntry.Id }, createdCostEntry);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the cost entry.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, CostEntryDto costEntryDto)
        {
            try
            {
                _service.Update(id, costEntryDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) when (ex.Message == "CostEntry not found")
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the cost entry.");
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
            catch (Exception ex) when (ex.Message == "CostEntry not found")
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the cost entry.");
            }
        }
    }
}
