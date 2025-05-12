using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IEmailService
    {
        public void SendContactEmail(ContactFormRequest form);
        public void SendUserConfirmationEmail(string userEmail, string userName);
        void SendRecoveryLinkToUser(string email, string link);
    }
}
