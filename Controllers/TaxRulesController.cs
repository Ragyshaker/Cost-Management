using ERPtask.DTOs;
using ERPtask.servcies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERPtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxRulesController : ControllerBase
    {
        private readonly ITaxRuleService _service;

        public TaxRulesController(ITaxRuleService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<TaxRuleDto>> GetAll()
        {
            var taxRules = _service.GetAll();
            return Ok(taxRules);
        }

        [HttpGet("{id}")]
        public ActionResult<TaxRuleDto> GetById(int id)
        {
            var taxRule = _service.GetById(id);
            if (taxRule == null)
                return NotFound();
            return Ok(taxRule);
        }
        [HttpPost]
        public ActionResult<TaxRuleDto> Create([FromBody] TaxRuleCreateDto dto)
        {
            try
            {
                var taxRule = _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = taxRule.Id }, taxRule);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaxRuleUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            try
            {
                _service.Update(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
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

        [HttpGet("by-region/{region}")]
        public ActionResult<decimal> GetTaxRateByRegion(string region)
        {
            try
            {
                var rate = _service.GetTaxRateByRegion(region);
                return Ok(rate);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
