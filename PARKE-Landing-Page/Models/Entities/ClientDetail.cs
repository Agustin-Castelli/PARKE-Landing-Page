using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PARKE_Landing_Page.Models.Entities
{
    public class ClientDetail
    {
        
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; } 

        
        public int? MachineId { get; set; }
        [ForeignKey("MachineId")]
        public Machine? Machine { get; set; } 
    }
}
