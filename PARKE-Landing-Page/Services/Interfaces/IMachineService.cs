using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Services.DTOs;
using System.Collections.Generic;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IMachineService
    {
        IEnumerable<Machine> GetAllMachines();           // Obtiene todas las máquinas
        Machine GetMachineById(int id);                  // Obtiene una máquina por ID
        Machine AddMachine(MachineRequest machineRequest);  // Agrega una nueva máquina
        void UpdateMachine(int id, MachineRequest machineRequest);  // Actualiza una máquina existente
        void DeleteMachine(int id);                     // Elimina una máquina por ID
    }
}