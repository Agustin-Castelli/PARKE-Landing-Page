using System.Net;
using System.Net.Mail;
using PARKE_Landing_Page.Services.Interfaces;
using Microsoft.Extensions.Options;
using PARKE_Landing_Page.Services.DTOs;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail = "parketesting@gmail.com"; 
        private readonly string _fromPassword = "muyb ajdx totd wagv";  

        public EmailService()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)  // Configuración SMTP de Gmail
            {
                Credentials = new NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true
            };
            _smtpClient.UseDefaultCredentials = false;
        }

        public void SendContactEmail(ContactFormRequest form)
        {
            string subject = form.Subject;
            string body = $"Nombre: {form.Name}\nEmail: {form.Email}\nMensaje: {form.Message}";

            SendEmail("parketesting@gmail.com", subject, body);  // Dirección de destino (tu cuenta de Gmail)
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage(_fromEmail, toEmail, subject, body)
            {
                IsBodyHtml = false 
            };

            _smtpClient.Send(mailMessage);
        }
    }
}
