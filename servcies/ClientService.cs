using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using ERPtask.models;
using ERPtask.Repositrories;
using ERPtask.DTOs;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies.Interfaces;
namespace ERPtask.servcies
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public List<ClientDto> GetAll()
        {
            var clients = _repository.GetAll();
            return clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address
            }).ToList();
        }

        public ClientDto GetById(int id)
        {
            var client = _repository.GetById(id);
            if (client == null)
                return null;
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Address = client.Address
            };
        }
        public ClientDto Create(ClientDto clientDto)
        {
            var client = new Client
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                Address = clientDto.Address
            };
            var createdClient = _repository.Add(client);
            return new ClientDto
            {
                Id = createdClient.Id,
                Name = createdClient.Name,
                Email = createdClient.Email,
                Address = createdClient.Address
            };
        }

        public void Update(ClientDto clientDto)
        {
            var client = new Client
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Email = clientDto.Email,
                Address = clientDto.Address
            };
            _repository.Update(client);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public ClientDto GetByEmail(string email)
        {
            var client = _repository.GetByEmail(email);
            if (client == null) return null;
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Address = client.Address
            };
        }
    }

}
