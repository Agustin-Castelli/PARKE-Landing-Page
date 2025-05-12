using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Services.DTOs;
using AuthServiceAdmin = PARKE_Landing_Page.Services.Interfaces.IAuthenticationServiceAdmin;
using AuthServiceClient = PARKE_Landing_Page.Services.Interfaces.IAuthenticationServiceClient;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthServiceAdmin _authenticationServiceAdmin;
        private readonly AuthServiceClient _authenticationServiceClient;
        private readonly IEmailService _emailService;

        public AuthenticationController(AuthServiceAdmin authenticationServiceAdmin, AuthServiceClient authenticationServiceClient, IEmailService emailService)
        {
            _authenticationServiceAdmin = authenticationServiceAdmin;
            _authenticationServiceClient = authenticationServiceClient;
            _emailService = emailService;
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

        [HttpPost("[action]")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                    return BadRequest("Email inválido.");

                // Lógica para generar el link de recuperación (puede incluir un token firmado con expiración)
                string recoveryToken = _authenticationServiceClient.GenerateRecoveryToken(request.Email);

                string recoveryLink = $"https://localhost:5173/resetPassword?token={recoveryToken}";

                // envia el link al usuario
                _emailService.SendRecoveryLinkToUser(request.Email, recoveryLink);

                return Ok("Link de recuperación enviado al correo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        [HttpPost("[action]")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Token) ||
        string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest("Datos inválidos.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest("Las contraseñas no coinciden.");
            }

            try
            {
                bool result = _authenticationServiceClient.ResetPassword(request.Token, request.NewPassword);

                if (!result)
                    return BadRequest("El token es inválido o ha expirado.");

                return Ok("Contraseña actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}
