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
    }
}
