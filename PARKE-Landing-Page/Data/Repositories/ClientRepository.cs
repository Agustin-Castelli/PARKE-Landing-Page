using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Machine> GetMachinesByClientId(int clientId)
        {
            return _context.ClientDetails
                .Where(cd => cd.ClientId == clientId)
                .Select(cd => cd.Machine) 
                .Where(m => m != null);   
        }


    }
}
