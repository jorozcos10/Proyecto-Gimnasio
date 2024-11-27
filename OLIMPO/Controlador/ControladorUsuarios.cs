using OLIMPO.Modelo;
using System;
using System.IO;
using System.Windows.Forms.VisualStyles;
using OLIMPO.Helpers;
using System.Collections.Generic;

namespace OLIMPO.Controlador
{
    public class ControladorUsuarios
    {
       // private string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos", "usuarios.csv");
        private string rutaArchivo = FileHelper.GetFilePath("usuarios.csv");

        public ControladorUsuarios()
        {
            Console.WriteLine("se inicializa constructor");
            // Crear el archivo CSV si no existe
            if (!File.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo)); // Crear la carpeta si no existe
                using (var sw = new StreamWriter(rutaArchivo, false))
                {
                    // Escribir la cabecera del archivo CSV (opcional)
                    sw.WriteLine("Id,Nombre,Correo Electrónico,Cédula,Contraseña,Fecha Registro");
                }
            }
        }
        public List<ModeloClase> LeerDatos()
        {
            List<ModeloClase> lista = new List<ModeloClase>();

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

        public int GuardarDatos(ModeloUsuarios usuario)
        {
            try
            {
                // Verificar si el usuario ya existe en el archivo (por correo o cédula)
              
                if (UsuarioYaExiste(usuario))
                {
                    throw new Exception("El usuario con este correo o cédula ya está registrado.");
                }
                int prev_id = 0;// getLastId();
                prev_id++;
                usuario.id = prev_id;
                // Formato de la línea
                string linea = $"{usuario.id},{usuario.Nombre},{usuario.CorreoElectronico},{usuario.Cedula},{usuario.Contraseña},{usuario.Fecha_registro}";

                // Agregar la línea al archivo CSV
                using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
                {
                    writer.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los datos del usuario: " + ex.Message);
            }
            return usuario.id;
        }

        private bool UsuarioYaExiste(ModeloUsuarios usuario)
        {
           // try
            //{
                using (StreamReader reader = new StreamReader(rutaArchivo))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var campos = line.Split(',');

                        // Validar por correo o cédula
                        if (campos[2].Trim() == usuario.CorreoElectronico || campos[3].Trim() == usuario.Cedula)
                        {
                            return true;
                        }
                    }
                }
            //}
           /* catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del usuario: " + ex.Message);
            }*/

            return false;
        }
        private int getLastId()
        {
            int lastId = 0;

            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(rutaArchivo))
                {
                    // Si el archivo no existe, asumimos que es la primera inserción
                    return 0;
                }

                using (StreamReader reader = new StreamReader(rutaArchivo))
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
    }
}
