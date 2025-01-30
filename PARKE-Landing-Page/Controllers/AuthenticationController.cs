using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Services.DTOs;
using AuthServiceAdmin = PARKE_Landing_Page.Services.Interfaces.IAuthenticationServiceAdmin;
using AuthServiceClient = PARKE_Landing_Page.Services.Interfaces.IAuthenticationServiceClient;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthServiceAdmin _authenticationServiceAdmin;
        private readonly AuthServiceClient _authenticationServiceClient;

        public AuthenticationController(AuthServiceAdmin authenticationServiceAdmin, AuthServiceClient authenticationServiceClient)
        {
            _authenticationServiceAdmin = authenticationServiceAdmin;
            _authenticationServiceClient = authenticationServiceClient;
        }

        [HttpPost("[action]")]
        public ActionResult<string> AuthenticateAdmin([FromBody] AuthenticationRequestAdmin authenticationRequest)
        {
            try
            {
                string token = _authenticationServiceAdmin.Authenticate(authenticationRequest);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
        [HttpPost("[action]")]
        public ActionResult<string> AuthenticateClient([FromBody] AuthenticationRequestClient authenticationRequest)
        {
            try
            {
                string token = _authenticationServiceClient.Authenticate(authenticationRequest);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

    }
}
