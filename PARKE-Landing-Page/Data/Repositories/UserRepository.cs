using PARKE_Landing_Page.Data.Services;

namespace PARKE_Landing_Page.Data.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }


    }
}
