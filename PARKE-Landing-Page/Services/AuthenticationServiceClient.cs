using Microsoft.Extensions.Options;
using PARKE_Landing_Page.Services.DTOs;
using System.Security.Claims;
using System.Text;
using PARKE_Landing_Page.Services.Interfaces;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Data.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace PARKE_Landing_Page.Services
{
    public class AuthenticationServiceClient : IAuthenticationServiceClient
    {
        private readonly IClientRepository _clientRepository;
        private readonly AutenticacionServiceOptions _options;
        private readonly IHashingService _hashingService;

        public AuthenticationServiceClient(IClientRepository clientRepository, IOptions<AutenticacionServiceOptions> options, IHashingService hashingService)
        {
            _clientRepository = clientRepository;
            _options = options.Value;
            _hashingService = hashingService;
        }

        private Client? ValidateClient(AuthenticationRequestClient authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Username) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var client = _clientRepository.GetByUsername(authenticationRequest.Username);
            Console.WriteLine(client);

            if (client == null) return null;

            if (_hashingService.Verify(authenticationRequest.Password, client.Password))
            {
                return client;
            }

            return null;
        }

        public string Authenticate(AuthenticationRequestClient authenticationRequest)
        {
            var client = ValidateClient(authenticationRequest);

            if (client == null)
            {
                throw new UnauthorizedAccessException("Fallo en la autenticación del cliente");
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", client.Id.ToString()),
                new Claim("given_name", client.Username),
                new Claim(ClaimTypes.Role, "Client")

            };

            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public class AutenticacionServiceOptions
        {
            public const string AutenticacionService = "AutenticacionService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }

        //METODO PARA RESETEAR LA CONTRASEÑA
        public bool ResetPassword(string token, string newPassword)
        {
            // Paso 1: Validar el token (verificar firma, expiración, etc.)
            var email = ValidateRecoveryToken(token);
            if (email == null)
            {
                return false; // Token inválido o expirado
            }

            // Paso 2: Buscar al usuario por email
            var client = _clientRepository.GetByEmail(email); // asumimos que tenés un repositorio
            if (client == null)
            {
                return false; // Usuario no encontrado
            }

            // Paso 3: Hashear la nueva contraseña
            string hashedPassword = _hashingService.Hash(newPassword);

            // Paso 4: Guardar la nueva contraseña
            client.Password = hashedPassword;
            _clientRepository.Update(client);

            return true;
        }

        private string? ValidateRecoveryToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("clave-super-larga-y-segura-1234567890abcd");

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // No permitir tolerancia
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

                return emailClaim?.Value;
            }
            catch
            {
                return null;
            }
        }



        //Metodo para generar el token para la nueva contraseña
        public string GenerateRecoveryToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("clave-super-larga-y-segura-1234567890abcd"); // Usá algo seguro y configurable

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, email)
        }),
                Expires = DateTime.UtcNow.AddHours(1), // Duración del link de recuperación
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
