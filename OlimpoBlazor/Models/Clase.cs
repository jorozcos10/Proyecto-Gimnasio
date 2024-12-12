namespace OlimpoBlazor.Models
{
    public class Clase
    {
        public int Id { get; set; } // Identificador único de la clase
        public string Nombre { get; set; } // Nombre de la clase (e.g., Zumba, CardioDance)
        public DateTime Horario { get; set; } // Fecha y hora específicos
        public string Dias { get; set; } // Días en los que se imparte la clase
        public string Hora { get; set; } // Hora específica (formato "HH:mm")
        public int CuposDisponibles { get; set; } // Número de cupos disponibles
        public int EntrenadorId { get; set; } // ID del entrenador encargado
    }
}
