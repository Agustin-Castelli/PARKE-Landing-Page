using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Services;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Models.Entities;
using System.Collections.Generic;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(MachineService machineService)
        {
            _machineService = machineService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Machine>> GetAllMachines()
        {
            var machines = _machineService.GetAllMachines();
            return Ok(machines);
        }

        [HttpGet("{id}")]
        public ActionResult<Machine> GetMachineById(int id)
        {
            var machine = _machineService.GetMachineById(id);

            if (machine == null)
            {
                return NotFound(new { message = "Machine not found." });
            }

            return Ok(machine);
        }

        [HttpPost]
        public ActionResult<Machine> AddMachine([FromBody] MachineRequest machineRequest)
        {
            if (machineRequest == null)
            {
                return BadRequest(new { message = "Invalid machine data." });
            }

            var createdMachine = _machineService.AddMachine(machineRequest);

            
            return CreatedAtAction(
                nameof(GetMachineById),
                new { id = createdMachine.Id }, 
                createdMachine);
        }



        [HttpPut("{id}")]
        public ActionResult UpdateMachine(int id, [FromBody] MachineRequest machineRequest)
        {
            if (machineRequest == null)
            {
                return BadRequest(new { message = "Invalid machine data." });
            }

            _machineService.UpdateMachine(id, machineRequest);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMachine(int id)
        {
            _machineService.DeleteMachine(id);
            return NoContent(); 
        }
    }
}
