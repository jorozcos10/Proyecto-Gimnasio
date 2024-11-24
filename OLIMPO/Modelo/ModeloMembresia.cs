using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    internal class ModeloMembresia
    {
        public int ClienteId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Costo { get; set; }

        public bool EstaPorVencer()
        {
            return (FechaFin - DateTime.Now).TotalDays <= 5;
        }
    }
}
