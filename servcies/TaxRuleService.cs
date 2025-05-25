using ERPtask.DTOs;
using ERPtask.models;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies.Interfaces;

namespace ERPtask.servcies
{
    public class TaxRuleService : ITaxRuleService
    {
        private readonly ITaxRuleRepository _repository;

        public TaxRuleService(ITaxRuleRepository repository)
        {
            _repository = repository;
        }

        public List<TaxRuleDto> GetAll()
        {
            var taxRules = _repository.GetAll();
            return taxRules.Select(tr => new TaxRuleDto
            {
                Id = tr.Id,
                Region = tr.Region,
                TaxRate = tr.TaxRate
            }).ToList();
        }

        public TaxRuleDto GetById(int id)
        {
            var taxRule = _repository.GetById(id);
            if (taxRule == null)
                return null;
            return new TaxRuleDto
            {
                Id = taxRule.Id,
                Region = taxRule.Region,
                TaxRate = taxRule.TaxRate
            };
        }

        public TaxRuleDto Create(TaxRuleCreateDto dto)
        {
            if (_repository.GetByRegion(dto.Region) != null)
                throw new ArgumentException("Region already exists");

            var taxRule = new TaxRule
            {
                Region = dto.Region,
                TaxRate = dto.TaxRate
            };

            var createdRule = _repository.Add(taxRule);
            return new TaxRuleDto
            {
                Id = createdRule.Id,
                Region = createdRule.Region,
                TaxRate = createdRule.TaxRate
            };
        }

        public void Update(TaxRuleUpdateDto dto)
        {
            var existing = _repository.GetById(dto.Id) ?? throw new KeyNotFoundException("Tax rule not found");

            if (existing.Region != dto.Region && _repository.GetByRegion(dto.Region) != null)
                throw new ArgumentException("Region already exists");

            var taxRule = new TaxRule
            {
                Id = dto.Id,
                Region = dto.Region,
                TaxRate = dto.TaxRate
            };

            _repository.Update(taxRule);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public decimal GetTaxRateByRegion(string region)
        {
            var rule = _repository.GetByRegion(region);
            return rule?.TaxRate ?? throw new KeyNotFoundException("Tax rule not found for region");
        }
    }
}
