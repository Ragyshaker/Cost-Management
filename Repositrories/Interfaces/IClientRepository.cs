using ERPtask.models;

namespace ERPtask.Repositrories.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetAll();
        Client GetById(int id);
        Client Add(Client client);
        void Update(Client client);
        bool Delete(int id);
        Client GetByEmail(string email);
    }
}
