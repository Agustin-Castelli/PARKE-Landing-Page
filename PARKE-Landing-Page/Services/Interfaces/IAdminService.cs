using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IAdminService
    {
        public Admin Create(AdminRequest admin);
        public void Update(int id, AdminRequest admin);
        public void Delete(int id);
        public Admin GetById(int id);
        public List<Admin> GetAll();
        void AssignMachine(int clientId, int machineId);
    }
}
