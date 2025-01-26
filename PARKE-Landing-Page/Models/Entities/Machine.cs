namespace PARKE_Landing_Page.Models.Entities
{
    public class Machine
    {
        public int Id { get; set; } // Identificador único
        public string Name { get; set; } // Nombre de la máquina (ejemplo: "Embolsadora de bolsas")
        public string? Type { get; set; } // Tipo de máquina (ejemplo: "Embolsadoras", "Cosedoras", etc.)
        public string? Subtype { get; set; } // Subtipo dentro del tipo (ejemplo: "Boca abierta", "Valvuladas", etc.)
        public string Model { get; set; } // Modelo específico (ejemplo: "EP5B-TC", "GK35")
        public string Description { get; set; } // Descripción breve opcional
        public string FileName { get; set; }
        public List<string> MediaContent { get; set; }
        public string TypeOfForm { get; set; }
        public string IndustrialSector { get; set; }
        public int ClientId { get; set; }


        public ICollection<ClientDetail> ClientDetails { get; set; } = new List<ClientDetail>();
    }
}