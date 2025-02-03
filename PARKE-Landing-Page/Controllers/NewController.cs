using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly INewService _newService;

        public NewController(INewService newService)
        {
            _newService = newService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("[action]")]
        public ActionResult<New> Create(NewRequest newRequest)
        {
            try
            {
                var createdNew = _newService.Create(newRequest);
                return CreatedAtAction(nameof(GetById), new { id = createdNew.Id }, createdNew);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<New> GetById(int id)
        {
            try
            {
                var foundNew = _newService.GetById(id);
                return Ok(foundNew);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<New>> GetAll()
        {
            try
            {
                var allNews = _newService.GetAll();
                return Ok(allNews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("[action]/{id}")]
        public ActionResult Update(int id, NewRequest newRequest)
        {
            try
            {
                _newService.Update(id, newRequest);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound(new { Message = "Resource not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("[action]/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _newService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound(new { Message = "Resource not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
