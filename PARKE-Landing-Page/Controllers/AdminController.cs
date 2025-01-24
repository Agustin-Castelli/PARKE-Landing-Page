using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("[action]")]
        public IActionResult Create(AdminRequest admin)
        {
            try
            {
                var obj = _adminService.Create(admin);

                return Ok(obj);
            }
            catch (DuplicateElementException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "RequireSysAdminRole")]
        public IActionResult Update([FromRoute] int id, [FromBody] AdminRequest admin)
        {
            try
            {
                _adminService.Update(id, admin);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<Admin> GetById([FromRoute] int id)
        {
            try
            {
                return _adminService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<Admin>> GetAll()
        {
            return _adminService.GetAll();
        }

    }
}
