using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface INewService
    {
        New Create(NewRequest ndto);
        New GetById(int id);
        List<New> GetAll();
        void Update(int id, NewRequest nnew);
        void Delete(int id);
    }
}
