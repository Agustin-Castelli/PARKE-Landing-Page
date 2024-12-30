using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Services;

namespace PARKE_Landing_Page.Data.Repositories
{
    public class NewRepository : INewRepository
    {
        private readonly ApplicationContext _context;

        public NewRepository(ApplicationContext context)
        {
            _context = context;
        }

        public NewRepository Add(NewRepository newRepository)
        {
            _context.Add(newRepository);
            _context.SaveChanges();
            return newRepository;
        }

        public NewRepository GetById(int id)
        {
            return _context.Set<NewRepository>().Find(id);
        }

        public List<NewRepository> GetAll()
        {
            return _context.Set<NewRepository>().ToList();
        }

        public void Update(NewRepository newRepository)
        {
            _context.Update(newRepository);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<NewRepository>().Find(id);
            if (entity != null)
            {
                _context.Remove(entity); 
                _context.SaveChanges(); 
            }
        }
    }

    
}
