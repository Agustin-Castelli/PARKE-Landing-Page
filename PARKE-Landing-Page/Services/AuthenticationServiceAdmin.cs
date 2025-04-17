using Microsoft.Extensions.Options;
using PARKE_Landing_Page.Services.DTOs;
using System.Security.Claims;
using System.Text;
using PARKE_Landing_Page.Services.Interfaces;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Data.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PARKE_Landing_Page.Services
{
    public class AuthenticationServiceAdmin : IAuthenticationServiceAdmin
    {
        private readonly IAdminRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;
        private readonly IHashingService _hashingService;

        public AuthenticationServiceAdmin(IAdminRepository userRepository, IOptions<AutenticacionServiceOptions> options, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _options = options.Value;
            _hashingService = hashingService;
        }

        private Admin? ValidateUser(AuthenticationRequestAdmin authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.UserName) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _userRepository.CheckUsername(authenticationRequest.UserName);

            if (user == null) return null;

            if (_hashingService.Verify(authenticationRequest.Password, user.Password))
            {
                return user;
            }

            return null;

        }
        public string Authenticate(AuthenticationRequestAdmin authenticationRequest)
        {
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Fallo en la autenticación del usuario");
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);


            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var jwtSecurityToken = new JwtSecurityToken(
              _options.Issuer,
              _options.Audience,
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
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
