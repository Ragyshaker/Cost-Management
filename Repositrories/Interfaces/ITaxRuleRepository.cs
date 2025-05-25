using ERPtask.models;

namespace ERPtask.Repositrories.Interfaces
{
    public interface ITaxRuleRepository
    {
        List<TaxRule> GetAll();
        TaxRule GetById(int id);
        TaxRule Add(TaxRule taxRule);
        void Update(TaxRule taxRule);
        bool Delete(int id);
        TaxRule GetByRegion(string region);
    }
}
