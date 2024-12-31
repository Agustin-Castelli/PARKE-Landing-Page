using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IUserService
    {
        public User Create(UserRequest user);
        public void Update(int id, UserRequest user);
        public void Delete(int id);
        public User GetById(int id);
        public List<User> GetAll();
    }
}
