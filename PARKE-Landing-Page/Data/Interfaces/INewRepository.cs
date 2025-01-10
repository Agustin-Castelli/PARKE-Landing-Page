using PARKE_Landing_Page.Data.Repositories;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Interfaces
{
    public interface INewRepository
    {
        New Add(New nnew);
        New GetById(int id);
        List<New> GetAll();
        void Update(New nnew);
        void Delete(New nnew);
    }
}
