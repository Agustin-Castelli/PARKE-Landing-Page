using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Services;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;

        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Admin Add(Admin admin)
        {
            _context.Set<Admin>().Add(admin);
            _context.SaveChanges();
            return admin;
        }

        public void Update(Admin admin)
        {
            _context.SaveChanges();
        }

        public void Delete(Admin admin)
        {
            _context.Remove(admin);
            _context.SaveChanges();
        }

        public Admin? GetById(int id)
        {
            return _context.Admins.FirstOrDefault(p => p.Id == id);
        }

        public List<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }
        public Admin? CheckUsername(string userName)
        {
            return _context.Set<Admin>().FirstOrDefault(u => u.Username == userName);
        }
    }
}
   