using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Repositories;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;
using PARKE_Landing_Page.Models.Exceptions;

namespace PARKE_Landing_Page.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public IEnumerable<Machine> GetAllMachines()
        {
            return _machineRepository.GetAll();
        }

        public Machine GetMachineById(int id)
        {
            var machine = _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new NotFoundException(nameof(Machine), id);
            }

            return machine;
        }

        public Machine AddMachine(MachineRequest machineRequest)
        {
            var newMachine = new Machine
            {
                Name = machineRequest.Name,
                Type = machineRequest.Type,
                Subtype = machineRequest.Subtype,
                Model = machineRequest.Model,
                Description = machineRequest.Description,
                FileName = machineRequest.FileName,
                MediaContent = machineRequest.MediaContent,
                TypeOfForm = machineRequest.TypeOfForm,
                IndustrialSector = machineRequest.IndustrialSector
            };

            _machineRepository.Add(newMachine);
            return newMachine;
        }

        public void UpdateMachine(int id, MachineRequest machineRequest)
        {
            var machine = _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new NotFoundException(nameof(Machine), id);
            }

            machine.Name = machineRequest.Name;
            machine.Type = machineRequest.Type;
            machine.Subtype = machineRequest.Subtype;
            machine.Model = machineRequest.Model;
            machine.Description = machineRequest.Description;
            machine.FileName = machineRequest.FileName;
            machine.MediaContent = machineRequest.MediaContent;
            machine.TypeOfForm = machineRequest.TypeOfForm;
            machine.IndustrialSector = machineRequest.IndustrialSector;

            _machineRepository.Update(machine);
        }

        public void DeleteMachine(int id)
        {
            var machine = _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new NotFoundException(nameof(Machine), id);
            }

            _machineRepository.Delete(id);
        }
    }
}
