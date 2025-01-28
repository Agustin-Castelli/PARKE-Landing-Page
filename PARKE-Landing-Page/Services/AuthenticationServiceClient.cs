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

        public AuthenticationServiceClient(IClientRepository clientRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _clientRepository = clientRepository;
            _options = options.Value;
        }

        private Client? ValidateClient(AuthenticationRequestClient authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var client = _clientRepository.GetByEmail(authenticationRequest.Email);

            if (client == null) return null;

            if (client.Password == authenticationRequest.Password)
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
                new Claim("email", client.Email),
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
    }
}
