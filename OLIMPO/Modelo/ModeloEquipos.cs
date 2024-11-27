using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    public class ModeloEquipos
    {
        // Propiedades del equipo
        public int Id { get; set; } // Identificador único del equipo
        public string Nombre { get; set; } // Nombre del equipo (e.g., "Cinta de correr")
        public string Tipo { get; set; } // Tipo de equipo (e.g., "Cardio", "Fuerza")
        public string FechaC { get; set; } // Fecha de adquisición
        public DateTime FechaCompra { get; set; } // Fecha de adquisición
        public int VidaUtilMeses { get; set; } // Vida útil en meses

        public DateTime FechaVencimiento
        {
            get
            {
                // Calculamos la fecha de vencimiento sumando la vida útil a la fecha de compra
                return FechaCompra.AddMonths(VidaUtilMeses);
            }
        }

        public bool EsVencimientoProximo()
        {
            // Verificamos si la fecha de vencimiento está dentro de 3 meses o menos
           // return (FechaVencimiento - DateTime.Now).TotalDays <= 90;

            // Obtener la fecha de vencimiento sumando los meses de vida útil a la fecha de compra
            DateTime fechaVencimiento = FechaCompra.AddMonths(VidaUtilMeses);

            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Verificar si la fecha de vencimiento está dentro de los próximos 3 meses
            return fechaVencimiento <= fechaActual.AddMonths(3) && fechaVencimiento > fechaActual;
        }
    }
}
