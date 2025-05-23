﻿using Microsoft.EntityFrameworkCore;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Interfaces
{
    public interface IClientRepository
    {
        public Client GetById(int id);
        public List<Client> GetAll();
        public void Delete(Client client);
        public Client Add(Client client);
        public void Update(Client client);

        public List<Machine> GetMachinesByClientId(int clientId);
        public Client GetByIdWithDetails(int id);
        public void RemoveClientMachines(int clientId);

        public Client? GetByUsername(string username);
        Client GetByEmail(string email);
    }
}
