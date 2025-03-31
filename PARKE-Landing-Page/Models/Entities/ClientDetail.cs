using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PARKE_Landing_Page.Models.Entities
{
    public class ClientDetail
    {
        [Key, Column(Order = 0)]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Key, Column(Order = 1)]
        public int? MachineId { get; set; }

        [ForeignKey("MachineId")]
        public Machine? Machine { get; set; } 
    }
}
