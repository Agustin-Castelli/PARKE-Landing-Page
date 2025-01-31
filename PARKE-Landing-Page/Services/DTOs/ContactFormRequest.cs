namespace PARKE_Landing_Page.Services.DTOs
{
    public class ContactFormRequest
    {
        public string Name { get; set; }       // Se mantiene igual, ya que "Name" es correcto
        public string Email { get; set; }      // También se mantiene igual, "Email" es correcto
        public string Company { get; set; }    // Cambié "Subject" por "Company" para que coincida con el campo "Empresa"
        public string Phone { get; set; }      // Cambié "Message" por "Phone" para coincidir con el campo "Teléfono"
        public string Subject { get; set; }
        public string Message { get; set; }    // "Message" se mantiene igual, porque coincide con el campo "Mensaje"
    }

}
