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
        private readonly IConfiguration _configuration;

        public ReCaptchaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateRecaptcha([FromBody] RecaptchaRequest request)
        {

            Console.WriteLine(request.Token);

            var secretKey = "6Ldng8IqAAAAAMMvIzmrl2VcT85yscnmPtxPGJbL";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={request.Token}", null);

                // Leer la respuesta como una cadena JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine(jsonResponse); // Para ver la respuesta de Google

                // Deserializar la respuesta JSON en un objeto RecaptchaResponse

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                };

                var result = JsonSerializer.Deserialize<RecaptchaResponse>(jsonResponse, options);
                //var result = JsonSerializer.Deserialize<RecaptchaResponse>(jsonResponse);

                if (result == null)
                {
                    Console.WriteLine("Error al deserializar la respuesta de reCAPTCHA.");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error al deserializar la respuesta de reCAPTCHA."
                    });
                }

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