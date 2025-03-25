using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public RegistrationEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("[action]")]
        public IActionResult SendRegistrationForm([FromBody] ContactFormRequest form)
        {

            // Validar el modelo
            if (form == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos inválidos." });
            }

            try
            {
                // Enviar correo a la empresa
                _emailService.SendContactEmail(form);

                // Enviar correo de confirmación al usuario
                _emailService.SendUserConfirmationEmail(form.Email, form.Name);

                return Ok(new { message = "Formulario enviado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el formulario", error = ex.Message });
            }
        }
    }
}
