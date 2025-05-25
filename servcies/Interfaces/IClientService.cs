using ERPtask.DTOs;

namespace ERPtask.servcies.Interfaces
{
    public interface IClientService
    {
        List<ClientDto> GetAll();
        ClientDto GetById(int id);
        ClientDto Create(ClientDto clientDto);
        void Update(ClientDto clientDto);
        bool Delete(int id);
        ClientDto GetByEmail(string email);
    }
}
