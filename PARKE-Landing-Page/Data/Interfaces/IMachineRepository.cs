using PARKE_Landing_Page.Models.Entities;
using System.Collections.Generic;

namespace PARKE_Landing_Page.Repositories
{
    public interface IMachineRepository
    {
        IEnumerable<Machine> GetAll();          
        Machine GetById(int id);                 
        Machine Add(Machine machine);               
        void Update(Machine machine);            
        void Delete(int id);                  
    }
}
