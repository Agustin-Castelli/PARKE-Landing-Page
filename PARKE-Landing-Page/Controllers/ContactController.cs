using Microsoft.AspNetCore.Mvc;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("send")]
        public IActionResult SendContactForm([FromBody] ContactFormRequest form)
        {
            if (form == null || !ModelState.IsValid)
                return BadRequest("Invalid data.");

            
            _emailService.SendContactEmail(form);

            return Ok("Email sent successfully.");
        }

        [HttpPost("quick-quote")]
        public IActionResult SubmitQuickQuote([FromBody] QuoteFormRequest request)
        {
            try
            {
                _emailService.SendQuoteEmail(
                    "Nueva cotización PARKE",  // Asunto fijo
                    $"DATOS DEL CLIENTE:\n{request.CustomerData}\n\n" +
                    $"DATOS DE LA MÁQUINA:\n{request.MachineData}"
                );

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al enviar: {ex.Message}");
            }
        }
    }
}
