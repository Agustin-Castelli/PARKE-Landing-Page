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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public IActionResult Create(UserRequest user)
        {
            try
            {
                var obj = _userService.Create(user);

                return Ok(obj);
            }
            catch (DuplicateElementException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "RequireSysAdminRole")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserRequest user)
        {
            try
            {
                _userService.Update(id, user);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            try
            {
                return _userService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<User>> GetAll()
        {
            return _userService.GetAll();
        }

    }
}
