using PARKE_Landing_Page.Models.Entities;
namespace PARKE_Landing_Page.Models.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string? NameCompany { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public int MachineId { get; set; }
        public ICollection<ClientDetail> ClientDetails { get; set; } = new List<ClientDetail>();

    }
}
