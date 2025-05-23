﻿using Microsoft.EntityFrameworkCore;
using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Services;
using PARKE_Landing_Page.Models.Entities;
namespace PARKE_Landing_Page.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;
        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Client GetById(int id)
        {
            return _context.Clients.Find(id);   
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public void Delete(Client client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
        public Client Add(Client client)
        {
            _context.Clients.Add(client);  
            _context.SaveChanges();
            return client;
        }
        public void Update(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }

        public List<Machine> GetMachinesByClientId(int clientId)
        {
            return _context.Clients
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.ClientDetails) 
                .Select(cd => cd.Machine) 
                .AsQueryable()
                .ToList();
        }

        public void RemoveClientMachines(int clientId)
        {
            var details = _context.ClientDetails
                                .Where(cd => cd.ClientId == clientId)
                                .ToList();

            _context.ClientDetails.RemoveRange(details);
            _context.SaveChanges();
        }

        public Client GetByIdWithDetails(int id)
        {
            return _context.Clients
                .Include(c => c.ClientDetails)
                .FirstOrDefault(c => c.Id == id);
        }

        public Client? GetByUsername(string username)
        {
            return _context.Set<Client>().FirstOrDefault(u => u.Username == username); 
        }

        public Client GetByEmail(string email)
        {
            return _context.Clients.FirstOrDefault(c => c.Email == email);
        }
    }
}
