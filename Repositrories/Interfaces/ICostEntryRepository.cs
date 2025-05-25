using ERPtask.models;

namespace ERPtask.Repositrories.Interfaces
{
    public interface ICostEntryRepository
    {
        List<CostEntry> GetAll();
        CostEntry GetById(int id);
        CostEntry Insert(CostEntry costEntry);
        void Update(CostEntry costEntry);
        void Delete(int id);
    }

}
