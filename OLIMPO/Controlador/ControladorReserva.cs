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
    public class ControladorReserva
    {
        private string archivo = FileHelper.GetFilePath("reserva.csv");
        public ControladorReserva()
        {
           
            // Crear el archivo CSV si no existe
            if (!File.Exists(archivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(archivo)); // Crear la carpeta si no existe
                using (var sw = new StreamWriter(archivo, false))
                {
                    // Escribir la cabecera del archivo CSV (opcional)
                    sw.WriteLine("Id,Clase,Horario,Idu,Nombre,Ide,entrenador");
                }
            }
        }
        public void GuardarDatos(ModeloReserva reserva)
        {
            int prev_id = getLastId();
            prev_id++;
            reserva.Id = prev_id;
            using (StreamWriter sw = new StreamWriter(archivo, true)) // Se abre el archivo en modo append
            {
                // Formato de la línea a guardar (ahora incluye la contraseña)
                string linea = $"{reserva.Id},{reserva.Clase},{reserva.Horario},{reserva.Idu},{reserva.Nombre},{reserva.Ide},{reserva.Entrenador}";
                sw.WriteLine(linea);  // Se escribe la línea en el archivo
            }
        }
        public void GuardarDatosEntrenador(ModeloReserva reserva,string nombre)
        {    string archivo = FileHelper.GetFilePath("disponibilidad_entrenador.csv");
            if (!File.Exists(archivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(archivo)); // Crear la carpeta si no existe
                using (var sw = new StreamWriter(archivo, false))
                {
                    // Escribir la cabecera del archivo CSV (opcional)
                    sw.WriteLine("Id,Clase,Horario,Ide,Entrenador");
                }
            }
            int prev_id = getLastIdEntrenador(archivo);
            prev_id++;
            reserva.Id = prev_id;
            using (StreamWriter sw = new StreamWriter(archivo, true)) // Se abre el archivo en modo append
            {
                // Formato de la línea a guardar (ahora incluye la contraseña)
                string linea = $"{reserva.Id},{reserva.Clase},{reserva.Horario},{reserva.Idu},{nombre}";
                sw.WriteLine(linea);  // Se escribe la línea en el archivo
            }
        }

        private int getLastId()
        {
            int lastId = 0;

            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(archivo))
                {
                    // Si el archivo no existe, asumimos que es la primera inserción
                    return 0;
                }

                using (StreamReader reader = new StreamReader(archivo))
                {
                    string line;
                    bool isHeader = true; // Ignorar la primera línea (encabezados)

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isHeader)
                        {
                            isHeader = false;
                            continue; // Ignorar encabezados
                        }

                        if (string.IsNullOrWhiteSpace(line))
                            continue; // Ignorar líneas vacías

                        var campos = line.Split(',');

                        if (campos.Length > 0 && int.TryParse(campos[0].Trim(), out int currentId))
                        {
                            lastId = Math.Max(lastId, currentId); // Obtener el ID más alto
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último ID: " + ex.Message);
            }

            return lastId;
        }
        public List<ModeloReserva> getByIdu(String idu)
        {
            List<ModeloReserva> entrenadores = new List<ModeloReserva>();

            // Verifica si el archivo existe
            if (File.Exists(archivo))

            {
                using (StreamReader sr = new StreamReader(archivo)) // Lee el archivo línea por línea
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)  // Lee cada línea del archivo
                    {
                       
                            // Separa la línea en partes usando la coma como delimitador
                            string[] partes = linea.Split(',');
                        if (idu != partes[3]) {
                            continue;
                        }
                            // Crea un objeto ModeloEntrenador a partir de las partes
                            ModeloReserva mr = new ModeloReserva
                        {
                            Id =int.Parse (partes[0]),
                            Clase = partes[1],
                            Horario = partes[2],
                            Idu = int.Parse(partes[3])
                        };

                        // Agrega el entrenador a la lista
                       
                        entrenadores.Add(mr);
                    }
                }
            }

            return entrenadores;  
        }
        public List<ModeloReserva> getByIde(String ide)
        {
            List<ModeloReserva> entrenadores = new List<ModeloReserva>();

            // Verifica si el archivo existe
            if (File.Exists(archivo))

            {
                using (StreamReader sr = new StreamReader(archivo)) // Lee el archivo línea por línea
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)  // Lee cada línea del archivo
                    {

                        // Separa la línea en partes usando la coma como delimitador
                        string[] partes = linea.Split(',');
                        if (ide != partes[5])
                        {
                            continue;
                        }
                        // Crea un objeto ModeloEntrenador a partir de las partes
                        ModeloReserva mr = new ModeloReserva
                        {
                            Id = int.Parse(partes[0]),
                            Clase = partes[1],
                            Horario = partes[2],
                            Idu = int.Parse(partes[3]),
                            Nombre = partes[4]
                        };

                        // Agrega el entrenador a la lista

                        entrenadores.Add(mr);
                    }
                }
            }

            return entrenadores;
        }
        private int getLastIdEntrenador(String archivo)
        {
            int lastId = 0;

            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(archivo))
                {
                    // Si el archivo no existe, asumimos que es la primera inserción
                    return 0;
                }

                using (StreamReader reader = new StreamReader(archivo))
                {
                    string line;
                    bool isHeader = true; // Ignorar la primera línea (encabezados)

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isHeader)
                        {
                            isHeader = false;
                            continue; // Ignorar encabezados
                        }

                        if (string.IsNullOrWhiteSpace(line))
                            continue; // Ignorar líneas vacías

                        var campos = line.Split(',');

                        if (campos.Length > 0 && int.TryParse(campos[0].Trim(), out int currentId))
                        {
                            lastId = Math.Max(lastId, currentId); // Obtener el ID más alto
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último ID: " + ex.Message);
            }

            return lastId;
        }
        public List<ModeloReserva> getEntrenador(String clase,string horario)
        {
            string archivo = FileHelper.GetFilePath("disponibilidad_entrenador.csv");
            List<ModeloReserva> entrenadores = new List<ModeloReserva>();

            // Verifica si el archivo existe
            if (File.Exists(archivo))

            {
                using (StreamReader sr = new StreamReader(archivo)) // Lee el archivo línea por línea
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)  // Lee cada línea del archivo
                    {

                        // Separa la línea en partes usando la coma como delimitador
                        string[] partes = linea.Split(',');
                        if (clase != partes[1] || horario != partes[2])
                        {
                            continue;
                        }
                        // Crea un objeto ModeloEntrenador a partir de las partes
                        ModeloReserva mr = new ModeloReserva
                        {
                            Id = int.Parse(partes[0]),
                            Clase = partes[1],
                            Horario = partes[2],
                            Idu = int.Parse(partes[3]),
                            Nombre = partes[4]
                        };

                        // Agrega el entrenador a la lista

                        entrenadores.Add(mr);
                    }
                }
            }

            return entrenadores;
        }
    }
   

}
