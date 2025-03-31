using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;
using System.Runtime.ConstrainedExecution;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("[action]")]
        public IActionResult Create(ClientRequest client)
        {
            try
            {
                var obj = _clientService.Create(client);

                return Ok(obj);
            }
            catch (DuplicateElementException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("[action]/{id}")]
       
        public IActionResult Update([FromRoute] int id, [FromBody] ClientRequest client)
        {
            try
            {
                _clientService.Update(id, client);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("[action]/{id}")]
        public ActionResult<Client> GetById([FromRoute] int id)
        {
            try
            {
                return _clientService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("[action]")]
        public ActionResult<List<Client>> GetAll()
        {
            return _clientService.GetAll();
        }

        [Authorize(Policy = "Client")]
        [HttpGet("GetMachinesByClient/{id}")]
        public ActionResult<List<Machine>> GetMachinesByClient(int id)
        {
            
            var machines = _clientService.GetMachinesByClientId(id);

            if (!machines.Any())
            {
                return NotFound("No machines found for the given client.");
            }


            return Ok(machines);
        }

        [HttpDelete("{clientId}/machine/{machineId}")]
        public ActionResult DeleteMachine([FromRoute] int clientId, [FromRoute] int machineId)
        {
            var result = _clientService.DeleteClientMachine(clientId, machineId);
            if (result)
            {
                return Ok(new { message = "Machine deleted successfully." });
            }
            return NotFound(new { message = "Machine not found." });
        }

    }
}
