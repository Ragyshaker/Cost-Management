using ERPtask.DTOs;
using ERPtask.models;
using ERPtask.Repositrories;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies.Interfaces;
using System.Data.SqlClient;
namespace ERPtask.servcies
{
    public class CostEntryService : ICostEntryService
    {
        private readonly ICostEntryRepository _repository;

        public CostEntryService(ICostEntryRepository repository)
        {
            _repository = repository;
        }

        public List<CostEntryDto> GetAll()
        {
            var costEntries = _repository.GetAll();
            return costEntries.Select(ce => new CostEntryDto
            {
                Id = ce.Id,
                Category = ce.Category,
                Amount = ce.Amount,
                Date = ce.Date,
                Description = ce.Description
            }).ToList();
        }

        public CostEntryDto GetById(int id)
        {
            var costEntry = _repository.GetById(id);
            if (costEntry == null)
                return null;
            return new CostEntryDto
            {
                Id = costEntry.Id,
                Category = costEntry.Category,
                Amount = costEntry.Amount,
                Date = costEntry.Date,
                Description = costEntry.Description
            };
        }
        public CostEntryDto Create(CostEntryDto costEntryDto)
        {
            if (string.IsNullOrWhiteSpace(costEntryDto.Category) || costEntryDto.Amount <= 0)
            {
                throw new ArgumentException("Category is required and Amount must be positive.");
            }
            var costEntry = new CostEntry
            {
                Category = costEntryDto.Category,
                Amount = costEntryDto.Amount,
                Date = costEntryDto.Date,
                Description = costEntryDto.Description
            };
            var insertedCostEntry = _repository.Insert(costEntry);
            return new CostEntryDto
            {
                Id = insertedCostEntry.Id,
                Category = insertedCostEntry.Category,
                Amount = insertedCostEntry.Amount,
                Date = insertedCostEntry.Date,
                Description = insertedCostEntry.Description
            };
        }

        public void Update(int id, CostEntryDto costEntryDto)
        {
            if (string.IsNullOrWhiteSpace(costEntryDto.Category) || costEntryDto.Amount <= 0)
            {
                throw new ArgumentException("Category is required and Amount must be positive.");
            }
            var existingCostEntry = _repository.GetById(id);
            if (existingCostEntry == null)
            {
                throw new Exception("CostEntry not found");
            }
            existingCostEntry.Category = costEntryDto.Category;
            existingCostEntry.Amount = costEntryDto.Amount;
            existingCostEntry.Date = costEntryDto.Date;
            existingCostEntry.Description = costEntryDto.Description;
            _repository.Update(existingCostEntry);
        }

        public void Delete(int id)
        {
            var existingCostEntry = _repository.GetById(id);
            if (existingCostEntry == null)
            {
                throw new Exception("CostEntry not found");
            }
            _repository.Delete(id);
        }
    }
}
