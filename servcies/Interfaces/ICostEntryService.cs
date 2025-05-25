using ERPtask.DTOs;

namespace ERPtask.servcies.Interfaces
{
    public interface ICostEntryService
    {
        List<CostEntryDto> GetAll();
        CostEntryDto GetById(int id);
        CostEntryDto Create(CostEntryDto costEntryDto);
        void Update(int id, CostEntryDto costEntryDto);
        void Delete(int id);
    }
}
