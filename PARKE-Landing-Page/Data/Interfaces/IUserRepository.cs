using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Interfaces
{
    public interface IUserRepository
    {
        public User Add(User user);
        public void Update(User user);
        public void Delete(User user);
        public User? GetById(int id);
        public List<User> GetAll();
    }
}
