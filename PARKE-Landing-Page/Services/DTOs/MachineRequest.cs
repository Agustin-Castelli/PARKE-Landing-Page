using System.ComponentModel.DataAnnotations;

namespace PARKE_Landing_Page.Services.DTOs
{
    public class MachineRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Nombre de la máquina (ejemplo: "Embolsadora de bolsas")
        public string? Type { get; set; } // Tipo de máquina (ejemplo: "Embolsadoras", "Cosedoras", etc.)
        public string? Subtype { get; set; } // Subtipo dentro del tipo (ejemplo: "Boca abierta", "Valvuladas", etc.)
        [Required]
        public string Model { get; set; } // Modelo específico (ejemplo: "EP5B-TC", "GK35")
        public string SerialNumber { get; set; } // Numero de serie de la maquina
        public string Description { get; set; } // Descripción breve opcional
        public string FileName { get; set; }
        public List<string> MediaContent { get; set; }
        public string TypeOfForm { get; set; }
        public string IndustrialSector { get; set; }

    }
}
