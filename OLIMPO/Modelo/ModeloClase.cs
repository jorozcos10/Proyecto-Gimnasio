using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    public class ModeloClase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Zumba, CardioDance, etc.
        public DateTime Horario { get; set; }
        public string Dias {  get; set; }
        public string hora {  get; set; }
        public int CuposDisponibles { get; set; }
        public List<int> Participantes { get; set; }
        public ModeloClase()
        {
          
        }

        public ModeloClase(int id, string nombre, DateTime horario, int cuposDisponibles)
        {
            Id = id;
            Nombre = nombre;
            Horario = horario;
            CuposDisponibles = cuposDisponibles;
            Participantes = new List<int>();
        }
        public ModeloClase(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
         
        }
    }
}
