using System;
using System.IO;

namespace OLIMPO.Modelo
{
    public class ModeloEntrenador
    {
        // Cambié las propiedades a seguir la convención PascalCase.
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }
        public string PuntosFuertes { get; set; }
        public string Horarios { get; set; }

        // Método privado para obtener la ruta del archivo 'entrenadores.csv'
        private string ObtenerRutaArchivo()
        {
            // Ruta absoluta para la carpeta Datos
            string carpetaDatos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");

            // Crear la carpeta Datos si no existe
            if (!Directory.Exists(carpetaDatos))
                Directory.CreateDirectory(carpetaDatos);

            // Retornar la ruta completa del archivo 'entrenadores.csv'
            return Path.Combine(carpetaDatos, "entrenadores.csv");
        }

        // Método para crear el archivo 'entrenadores.csv' si no existe
        public void CrearArchivoSiNoExiste()
        {
            try
            {
                string rutaArchivo = ObtenerRutaArchivo();

                // Crear el archivo solo si no existe
                if (!File.Exists(rutaArchivo))
                {
                    // Crear el archivo sin escribir nada
                    using (var stream = File.Create(rutaArchivo))
                    {
                        // No se escribe nada en el archivo
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra o maneja el error si es necesario
                Console.WriteLine("Error al crear el archivo: " + ex.Message);
            }
        }

        // Método para iniciar sesión con las credenciales de correo y contraseña
        public bool IniciarSesion()
        {
            try
            {
                string rutaArchivo = ObtenerRutaArchivo();

                // Leer el archivo CSV y buscar las credenciales
                if (File.Exists(rutaArchivo))
                {
                    using (var reader = new StreamReader(rutaArchivo))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var campos = line.Split(',');
                            string correoCSV = campos[1].Trim();
                            string contraseñaCSV = campos[3].Trim();

                            // Verifica si las credenciales coinciden
                            if (CorreoElectronico == correoCSV && Contraseña == contraseñaCSV)
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra o maneja el error si es necesario
                Console.WriteLine("Error al leer el archivo CSV: " + ex.Message);
            }

            return false;
        }
    }
}
