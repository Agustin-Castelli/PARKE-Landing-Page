using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IClientService
    {
        public Client Create(ClientRequest client);
        public void Update(int id, ClientRequest client);
        public void Delete(int id);
        public Client GetById(int id);
        public List<Client> GetAll();

        public List<Machine> GetMachinesByClientId(int clientId);

        bool DeleteClientMachine(int clientId, int machineId);
    }
}
