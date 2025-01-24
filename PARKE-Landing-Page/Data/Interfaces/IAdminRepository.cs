using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Interfaces
{
    public interface IAdminRepository
    {
        public Admin Add(Admin user);
        public void Update(Admin user);
        public void Delete(Admin user);
        public Admin? GetById(int id);
        public List<Admin> GetAll();

        Admin? CheckUsername(string userName);
    }
}
