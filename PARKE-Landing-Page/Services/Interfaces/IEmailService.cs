using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IEmailService
    {
        void SendContactEmail(ContactFormRequest form);
    }
}
