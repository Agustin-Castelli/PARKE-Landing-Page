using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Repositories;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;
namespace PARKE_Landing_Page.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client GetById(int id)
        {
            var obj = _clientRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            else
            {
                return obj;
            }
        }
        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public void Delete(int id)
        {
            var nnew = _clientRepository.GetById(id);

            if (nnew == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            _clientRepository.Delete(nnew);
        }
        public Client Create(ClientRequest client)
        {
            var newObj = new Client();

            newObj.Username = client.Username;
            newObj.Password = client.Password;
            newObj.NameCompany = client.NameCompany;
            newObj.PhoneNumber = client.PhoneNumber;
            newObj.Email = client.Email;

            return _clientRepository.Add(newObj);
        }
        public void Update(int id, ClientRequest client)
        {
            var obj = _clientRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            if (obj.Username != string.Empty) obj.Username = client.Username;
            if (obj.Password != string.Empty) obj.Password = client.Password;
            if (obj.NameCompany != string.Empty) obj.NameCompany = client.NameCompany;
            if (obj.PhoneNumber != string.Empty) obj.PhoneNumber = client.PhoneNumber;
            if (obj.Email != string.Empty) obj.Email = client.Email;

            _clientRepository.Update(obj);
        }
        public List<Machine> GetMachinesByClientId(int clientId)
        {

            var machines = _clientRepository.GetMachinesByClientId(clientId);

            if (!machines.Any())
            {
                throw new NotFoundException($"No se encontraron máquinas para el cliente con ID {clientId}.");
            }

            return machines;

        }

    }
}
