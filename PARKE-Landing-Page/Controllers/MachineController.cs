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

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Machine>> GetAllMachines()
        {
            try
            {
                var machines = _machineService.GetAllMachines();
                return Ok(machines);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Machine> GetMachineById(int id)
        {
            try
            {
                var machine = _machineService.GetMachineById(id);

                if (machine == null)
                {
                    return NotFound(new { message = "Machine not found." });
                }

                return Ok(machine);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Machine> AddMachine([FromBody] MachineRequest machineRequest)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMachine(int id, [FromBody] MachineRequest machineRequest)
        {
            try
            {
                if (machineRequest == null)
                {
                    return BadRequest(new { message = "Invalid machine data." });
                }

                _machineService.UpdateMachine(id, machineRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMachine(int id)
        {
            try
            {
                _machineService.DeleteMachine(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
