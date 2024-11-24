using OLIMPO.Helpers;
using OLIMPO.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Controlador
{
    public class ControladorClase
    {
        private string archivo = FileHelper.GetFilePath("clases.csv");
        public List<ModeloClase> LeerDatos()
        {
            List<ModeloClase> lista = new List<ModeloClase>();

            // Verifica si el archivo existe
            if (File.Exists(archivo))
            {
                using (StreamReader sr = new StreamReader(archivo)) // Lee el archivo línea por línea
                {
                    // Omitir la cabecera
                    sr.ReadLine(); // Lee y descarta la primera línea

                    string linea;
                    while ((linea = sr.ReadLine()) != null)  // Lee cada línea del archivo
                    {
                        // Separa la línea en partes usando la coma como delimitador
                        string[] partes = linea.Split(',');

                        // Crea un objeto ModeloClase a partir de las partes
                        ModeloClase clase = new ModeloClase
                        {
                            Id = int.Parse(partes[0]),
                            Nombre = partes[1],
                        };

                        // Agrega el objeto a la lista
                        lista.Add(clase);
                    }
                }
            }

            return lista;  // Devuelve la lista de clases
        }
    }

}
