using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Services;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public User? CheckUsername(string userName)
        {
            return _context.Set<User>().FirstOrDefault(u => u.Username == userName);
        }
    }
}
   