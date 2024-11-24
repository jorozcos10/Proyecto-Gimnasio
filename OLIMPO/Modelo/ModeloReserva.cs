using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    public class ModeloReserva
    {
        public int Id { get; set; }
        public string Clase { get; set; }
        public string Horario { get; set; }
        public int Idu { get; set; }
        public string Nombre { get; set; }

        public int Ide { get; set; }
        public string Entrenador { get; set; }
        public ModeloReserva() { }
    }
}
