using PARKE_Landing_Page.Data.Repositories;

namespace PARKE_Landing_Page.Data.Interfaces
{
    public interface INewRepository
    {
        NewRepository Add(NewRepository newRepository);
        NewRepository GetById(int id);
        List<NewRepository> GetAll();
        void Update(NewRepository newRepository);
        void Delete(int id);
    }
}
