using ERPtask.DTOs;

namespace ERPtask.servcies.Interfaces
{
    public interface ITaxRuleService
    {
        List<TaxRuleDto> GetAll();
        TaxRuleDto GetById(int id);
        TaxRuleDto Create(TaxRuleCreateDto dto);
        void Update(TaxRuleUpdateDto dto);
        bool Delete(int id);
        decimal GetTaxRateByRegion(string region);
    }
}
