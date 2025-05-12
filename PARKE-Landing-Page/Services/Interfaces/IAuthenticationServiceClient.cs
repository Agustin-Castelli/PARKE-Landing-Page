using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IAuthenticationServiceClient
    {
        string Authenticate(AuthenticationRequestClient authenticationRequest);
        bool ResetPassword(string token, string newPassword);
        string GenerateRecoveryToken(string email);
    }
}
