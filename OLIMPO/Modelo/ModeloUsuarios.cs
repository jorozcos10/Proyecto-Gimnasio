using OLIMPO.Helpers;
using System;
using System.IO;

namespace OLIMPO.Modelo
{
    public class ModeloUsuarios
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }
        public string Fecha_registro { get; set; }

        private string ObtenerRutaArchivo()
        {
            // Ruta absoluta para la carpeta Datos
            string carpetaDatos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");

            // Crear la carpeta Datos si no existe
            if (!Directory.Exists(carpetaDatos))
                Directory.CreateDirectory(carpetaDatos);

            // Retornar la ruta completa del archivo usuarios.csv
            return Path.Combine(carpetaDatos, "usuarios.csv");
        }

        /*public void CrearArchivoSiNoExiste()
        {
            try
            {
                string rutaArchivo = ObtenerRutaArchivo();

                // Crear el archivo solo si no existe
                if (!File.Exists(rutaArchivo))
                {
                    // Crear el archivo vacío y agregar la cabecera
                    using (var stream = File.Create(rutaArchivo))
                    {
                        // Crear una cabecera para el archivo CSV
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("Nombre,Correo Electrónico,Cédula,Contraseña");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Es recomendable loguear el error o mostrar un mensaje de error
                Console.WriteLine("Error al crear el archivo: " + ex.Message);
            }
        }*/

        public String[] IniciarSesion()
        {
            try
            {
               // string rutaArchivo = ObtenerRutaArchivo();
                string rutaArchivo =  FileHelper.GetFilePath("usuarios.csv");

                // Leer el archivo CSV y buscar las credenciales
                if (File.Exists(rutaArchivo))
                {
                    using (var reader = new StreamReader(rutaArchivo))
                    {
                        reader.ReadLine();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                           
                            var campos = line.Split(',');

                            // Validar si la cantidad de campos es correcta
                           // if (campos.Length == 4)
                            //{
                                string correoCSV = campos[2].Trim();
                                string contraseñaCSV = campos[4].Trim();
                           // Console.WriteLine(correoCSV + " " + contraseñaCSV);

                                if (CorreoElectronico == correoCSV && Contraseña == contraseñaCSV)
                                    return campos;
                           // }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Es recomendable loguear el error o mostrar un mensaje de error
                Console.WriteLine("Error al iniciar sesión: " + ex.Message);
            }

            return null;
        }
    }
}
