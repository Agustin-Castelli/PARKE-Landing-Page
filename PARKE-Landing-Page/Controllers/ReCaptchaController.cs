using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace PARKE_Landing_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReCaptchaController : ControllerBase
    {
        private const string SecretKey = "6Ldng8IqAAAAAMMvIzmrl2VcT85yscnmPtxPGJbL";  //La Secret Key de reCAPTCHA proporcionada por Google Cloud

        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateRecaptcha([FromBody] RecaptchaRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={request.Token}",
                    null
                    );

                // Leer la respuesta como una cadena JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON en un objeto RecaptchaResponse
                var result = JsonSerializer.Deserialize<RecaptchaResponse>(jsonResponse);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "reCAPTCHA validado correctamente.",
                        challengeTs = result.ChallengeTs,
                        hostname = result.Hostname
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "reCAPTCHA no válido.",
                        errorCodes = result.ErrorCodes
                    });
                }
            }
        }

        public class RecaptchaRequest
        {
            public string Token { get; set; }
        }

        public class RecaptchaResponse
        {
            public bool Success { get; set; } // Indica si el reCAPTCHA fue validado correctamente
            public string ChallengeTs { get; set; } // Timestamp de cuando se completó el reCAPTCHA
            public string Hostname { get; set; } // El hostname del sitio donde se resolvió el reCAPTCHA
            public string[] ErrorCodes { get; set; } // Códigos de error (opcional)
        }
    }
}