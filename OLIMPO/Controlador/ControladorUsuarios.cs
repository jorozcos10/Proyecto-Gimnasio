using OLIMPO.Modelo;
using System;
using System.IO;

namespace OLIMPO.Controlador
{
    public class ControladorUsuarios
    {
        private string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos", "usuarios.csv");

        public ControladorUsuarios()
        {
            // Crear el archivo CSV si no existe
            if (!File.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo)); // Crear la carpeta si no existe
                using (var sw = new StreamWriter(rutaArchivo, false))
                {
                    // Escribir la cabecera del archivo CSV (opcional)
                    sw.WriteLine("Nombre,Correo Electrónico,Cédula,Contraseña");
                }
            }
        }

        public void GuardarDatos(ModeloUsuarios usuario)
        {
            try
            {
                // Verificar si el usuario ya existe en el archivo (por correo o cédula)
                if (UsuarioYaExiste(usuario))
                {
                    throw new Exception("El usuario con este correo o cédula ya está registrado.");
                }

                // Formato de la línea
                string linea = $"{usuario.Nombre},{usuario.CorreoElectronico},{usuario.Cedula},{usuario.Contraseña}";

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
        }

        private bool UsuarioYaExiste(ModeloUsuarios usuario)
        {
            try
            {
                using (StreamReader reader = new StreamReader(rutaArchivo))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var campos = line.Split(',');

                        // Validar por correo o cédula
                        if (campos[1].Trim() == usuario.CorreoElectronico || campos[2].Trim() == usuario.Cedula)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del usuario: " + ex.Message);
            }

            return false;
        }
    }
}
