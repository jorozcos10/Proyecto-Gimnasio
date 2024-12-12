namespace OlimpoBlazor.Models
{
    public class Reserva
    {
        public int Id { get; set; } // Identificador único de la reserva
        public int ClaseId { get; set; } // Identificador de la clase reservada
        public string NombreClase { get; set; } // Nombre de la clase
        public DateTime HorarioClase { get; set; } // Horario de la clase
        public int ClienteId { get; set; } // Identificador del cliente que reserva
        public string NombreCliente { get; set; } // Nombre del cliente
        public int EntrenadorId { get; set; } // Identificador del entrenador encargado
        public string NombreEntrenador { get; set; } // Nombre del entrenador
    }
}
