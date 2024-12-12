namespace OlimpoBlazor.Models
{
    public class Membresia
    {
        public int Id { get; set; } // Identificador único de la membresía
        public int ClienteId { get; set; } // Identificador del cliente
        public string NombreCliente { get; set; } // Nombre del cliente
        public DateTime FechaInicio { get; set; } // Fecha de inicio de la membresía
        public DateTime FechaFin { get; set; } // Fecha de fin de la membresía
        public decimal Costo { get; set; } // Costo de la membresía
        public bool EstaActiva => DateTime.Now <= FechaFin; // Calcula si está activa
        public int DiasRestantes => (FechaFin - DateTime.Now).Days; // Calcula días restantes
    }
}
