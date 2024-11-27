using System;
using System.IO;

namespace OLIMPO.Helpers
{
    public static class FileHelper
    {
        // Esta función devuelve la ruta absoluta para el archivo
        public static string GetFilePath(string fileName)
        {
            /*string directory = Path.Combine(Directory.GetCurrentDirectory(), "Datos");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory); // Si no existe la carpeta "Datos", la crea
            }

            return Path.Combine(directory, fileName);
            */
            // Obtener la ruta del directorio raíz del proyecto
            string directory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Datos");

            // Si no existe la carpeta "Datos", la crea
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Combinar el directorio con el nombre del archivo
            return Path.Combine(directory, fileName);
        }
    }
}
