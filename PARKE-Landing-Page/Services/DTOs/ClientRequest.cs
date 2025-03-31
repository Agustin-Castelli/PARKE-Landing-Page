namespace PARKE_Landing_Page.Services.DTOs
{
    public class ClientRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? NameCompany { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
