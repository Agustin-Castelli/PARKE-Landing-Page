using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Services.DTOs;
using AuthService = PARKE_Landing_Page.Services.Interfaces.IAuthenticationService;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService _authenticationService;

        public AuthenticationController(AuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public ActionResult<string> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                string token = _authenticationService.Authenticate(authenticationRequest);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
