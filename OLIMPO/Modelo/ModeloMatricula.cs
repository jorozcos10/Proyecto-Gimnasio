using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    public class ModeloMatricula
    {
        public int Id { get; set; } // Identificador único de la matrícula
        public int ClienteId { get; set; } // Identificador del cliente asociado
        public DateTime FechaInicio { get; set; } // Fecha de inicio de la membresía
        public DateTime FechaFin { get; set; } // Fecha de finalización de la membresía
        public decimal Costo { get; set; } // Costo de la membresía
        public bool EstaActiva => DateTime.Now <= FechaFin; // Determina si la matrícula está activa
        public int DiasRestantes => (FechaFin - DateTime.Now).Days; // Días restantes de la membresía

        // Métodos
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID Matrícula: {Id}");
            Console.WriteLine($"Cliente ID: {ClienteId}");
            Console.WriteLine($"Fecha Inicio: {FechaInicio.ToShortDateString()}");
            Console.WriteLine($"Fecha Fin: {FechaFin.ToShortDateString()}");
            Console.WriteLine($"Costo: S/{Costo}");
            Console.WriteLine($"Estado: {(EstaActiva ? "Activa" : "Vencida")}");
            Console.WriteLine($"Días restantes: {(DiasRestantes > 0 ? DiasRestantes : 0)}");
            Console.WriteLine();
        }

        public bool NotificarProximaVencimiento()
        {
            if (EstaActiva && DiasRestantes <= 5)
            {
                Console.WriteLine("¡Atención! Su matrícula vence en menos de 5 días.");
                return true;
            }
            return false;
        }
    }
}
