namespace OlimpoBlazor.Models
{
    public class Equipo
    {
        public int Id { get; set; } // Identificador único del equipo
        public string Nombre { get; set; } // Nombre del equipo (e.g., Cinta de correr)
        public string Tipo { get; set; } // Tipo de equipo (e.g., Cardio, Fuerza)
        public DateTime FechaCompra { get; set; } // Fecha de adquisición
        public int VidaUtilMeses { get; set; } // Vida útil estimada en meses
    }
}
