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
    public class ControladorFactura
    {
        private string archivo = FileHelper.GetFilePath("facturas.csv");
        public ControladorFactura()
        {
          
            if (!File.Exists(archivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(archivo)); // Crear la carpeta si no existe
                using (var sw = new StreamWriter(archivo, false))
                {
                    // Escribir la cabecera del archivo CSV (opcional)
                    sw.WriteLine("Id,Idu,Cliente,Fecha emision,Concepto,Total");
                   
    }
            }
        }
        public void GuardarDatos(ModeloFactura factura)
        {
            int prev_id = getLastId();
            prev_id++;
            factura.Id = prev_id;
            using (StreamWriter sw = new StreamWriter(archivo, true)) // Se abre el archivo en modo append
            {
                // Formato de la línea a guardar (ahora incluye la contraseña)
                string linea = $"{factura.Id},{factura.Idu},{factura.Cliente},{factura.FechaEmision},{factura.Concepto},{factura.Total}";
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
    }

}
