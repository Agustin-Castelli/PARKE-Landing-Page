using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public RegisterAdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("[action]")]
        public IActionResult RegisterAdmin([FromBody] RegisterAdminRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Username y Password are required." });
            }

         
            

            var newAdmin = _adminService.Create(new AdminRequest
            {
                Username = request.Username,
                Password = request.Password
            });

            return Ok(new { message = "Admin created successfully." });
        }
    }
}
