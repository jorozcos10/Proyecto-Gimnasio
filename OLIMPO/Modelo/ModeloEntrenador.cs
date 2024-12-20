﻿using OLIMPO.Helpers;
using System;
using System.IO;

namespace OLIMPO.Modelo
{
    public class ModeloEntrenador
    {
        // Cambié las propiedades a seguir la convención PascalCase.
        public int id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }
        public string PuntosFuertes { get; set; }
        public string Horarios { get; set; }

        // Método privado para obtener la ruta del archivo 'entrenadores.csv'
        private string ObtenerRutaArchivo()
        {   string archivo = FileHelper.GetFilePath("entrenadores.csv");
            return archivo;
        
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
        public string IniciarSesion()
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
                            string correoCSV = campos[2].Trim();
                            string contraseñaCSV = campos[4].Trim();

                            // Verifica si las credenciales coinciden
                            if (CorreoElectronico == correoCSV && Contraseña == contraseñaCSV)
                                return campos[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra o maneja el error si es necesario
                Console.WriteLine("Error al leer el archivo CSV: " + ex.Message);
            }

            return "";
        }
    }
}
