using Microsoft.EntityFrameworkCore;
using PARKE_Landing_Page.Data;
using PARKE_Landing_Page.Data.Services;
using PARKE_Landing_Page.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PARKE_Landing_Page.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationContext _context;

        public MachineRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Machine> GetAll()
        {
            return _context.Machines.ToList();
        }

        public Machine GetById(int id)
        {
            return _context.Machines.FirstOrDefault(m => m.Id == id);
        }

        public Machine Add(Machine machine)
        {
            _context.Machines.Add(machine);
            _context.SaveChanges();
            return machine;
        }

        public void Update(Machine machine)
        {
            _context.Machines.Update(machine);
            _context.SaveChanges(); 
        }

        public void Delete(int id)
        {
            var machine = GetById(id);
            if (machine != null)
            {
                _context.Machines.Remove(machine);
                _context.SaveChanges(); 
            }
        }

    }
}