using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    public class ModeloMembresia
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public string Fi { get; set; }
        public string Ff { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Costo { get; set; }

        public bool EstaPorVencer()
        {
            return (FechaFin - DateTime.Now).TotalDays <= 5;
        }
    }
}
