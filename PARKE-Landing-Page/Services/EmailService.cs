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

        // Método para el envío de correo a la empresa
        public void SendContactEmail(ContactFormRequest form)
        {
            string subject = form.Subject;
            string body = $"Nombre: {form.Name}\nEmpresa: {form.Company}\nTeléfono: {form.Phone}\nEmail: {form.Email}\nMensaje: {form.Message}";

            SendEmail("parketesting@gmail.com", subject, body);  // Dirección de destino (tu cuenta de Gmail)
        }

        // Método para enviar correo de confirmación al usuario
        public void SendUserConfirmationEmail(string userEmail, string userName)
        {
            string subject = "Solicitud de registro PARKE Portal Web";
            string body = $"Hola {userName},\n\nLe notificamos que hemos recibido su solicitud de registro de cuenta para el Portal Web de Clientes de PARKE. Nos pondremos en contacto a la brevedad para proporcionarle más información.\n\nSaludos,\nEl equipo de PARKE";

            SendEmail(userEmail, subject, body);  // Envía el correo al usuario
        }

        // Método privado para enviar correos
        private void SendEmail(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage(_fromEmail, toEmail, subject, body)
            {
                IsBodyHtml = false 
            };

            _smtpClient.Send(mailMessage);
        }

        //metodo para recibir el mail de recuperacion de contraseña del usuario
        public void SendPasswordRecoveryRequestToCompany(string userEmail)
        {
            string subject = "Solicitud de recuperación de contraseña";
            string body = $"Se ha recibido una solicitud de recuperación de contraseña del usuario con el email: {userEmail}";

            SendEmail("parketesting@gmail.com", subject, body); 
        }

    }
}
