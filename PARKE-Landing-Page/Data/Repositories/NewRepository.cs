using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Services;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Repositories
{
    public class NewRepository : INewRepository
    {
        private readonly ApplicationContext _context;

        public NewRepository(ApplicationContext context)
        {
            _context = context;
        }

        public New Add(New nnew)
        {
            _context.Set<New>().Add(nnew);
            _context.SaveChanges();
            return nnew;
        }

        public New GetById(int id)
        {
            return _context.Set<New>().Find(id);
        }

        public List<New> GetAll()
        {
            return _context.Set<New>().ToList();
        }

        public void Update(New nnew)
        {
            _context.Update(nnew);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<New>().Find(id);
            if (entity != null)
            {
                _context.Remove(entity); 
                _context.SaveChanges(); 
            }
        }
    }

    
}
