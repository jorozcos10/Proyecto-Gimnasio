using OLIMPO.Helpers;
using OLIMPO.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Controlador
{
    public class ControladorEquipos
    {
        private string rutaArchivo = FileHelper.GetFilePath("equipos.csv");

        public List<ModeloEquipos> LeerDatos()
        {
            List<ModeloEquipos> lista = new List<ModeloEquipos>();

            // Verifica si el archivo existe
            if (File.Exists(rutaArchivo))
            {
                using (StreamReader sr = new StreamReader(rutaArchivo)) // Lee el archivo línea por línea
                {
                    // Omitir la cabecera
                    sr.ReadLine(); // Lee y descarta la primera línea

                    string linea;
                    while ((linea = sr.ReadLine()) != null)  // Lee cada línea del archivo
                    {
                        // Separa la línea en partes usando la coma como delimitador
                        string[] partes = linea.Split(',');

                        // Crea un objeto ModeloClase a partir de las partes
                        DateTime fecha = DateTime.ParseExact(partes[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        ModeloEquipos equipo = new ModeloEquipos
                        {
                            Id = int.Parse(partes[0]),
                            Nombre = partes[1],
                            FechaCompra = fecha,
                            VidaUtilMeses =int.Parse(partes[3])

                        };

                        // Agrega el objeto a la lista
                        lista.Add(equipo);
                    }
                }
            }

            return lista;  // Devuelve la lista de clases
        }
    }
}
