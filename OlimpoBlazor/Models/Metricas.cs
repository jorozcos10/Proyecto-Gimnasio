namespace OlimpoBlazor.Models
{
    public class Metricas
    {
        public int Id { get; set; } // Identificador único
        public int ClienteId { get; set; } // Identificador del cliente
        public decimal Peso { get; set; } // Peso corporal en kg
        public decimal Pecho { get; set; } // Medida del pecho en cm
        public decimal Biceps { get; set; } // Medida del bíceps en cm
        public decimal Triceps { get; set; } // Medida del tríceps en cm
        public decimal Piernas { get; set; } // Medida de las piernas en cm
        public DateTime FechaRegistro { get; set; } // Fecha de registro de las métricas
    }
}
