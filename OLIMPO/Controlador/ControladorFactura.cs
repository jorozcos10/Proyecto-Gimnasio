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
                    sw.WriteLine("Id,Idu,Cliente,Fecha emision,Concepto,Total,Tipo");
                   
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
                string linea = $"{factura.Id},{factura.Idu},{factura.Cliente},{factura.FechaEmision},{factura.Concepto},{factura.Total},{factura.Tipo}";
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
        public List<ModeloFactura> getByIdu(String idu)
        {
            List<ModeloFactura> lista = new List<ModeloFactura>();

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
                        if (idu != partes[1])
                        {
                            continue;
                        }
                        // Crea un objeto ModeloEntrenador a partir de las partes
                        ModeloFactura mr = new ModeloFactura
                        {
                            Id = int.Parse(partes[0]),
                            Idu = partes[1],
                           Cliente = partes[2],
                           FechaEmision= partes[3],
                           Concepto = partes[4],
                           Total  = (decimal)double.Parse( partes[5])
                        };

                        // Agrega el entrenador a la lista

                        lista.Add(mr);
                    }
                }
            }

            return lista;
        }
        public  List<ModeloFactura> listarMatriculas()
        {
            var matriculas = new List<ModeloFactura>();

            // Leer las líneas del archivo
            foreach (var line in File.ReadLines(archivo).Skip(1)) // Salta la primera línea (encabezados)
            {
                var partes = line.Split(',');

                // Verificar que la línea tiene el formato esperado
                if (partes.Length >= 6 && partes[4] == "MATRICULA")
                {
                    // Convertir los datos relevantes
                    var matricula = new ModeloFactura
                    {
                        Cliente = partes[2],
                        FechaEm = DateTime.Parse(partes[3]),  // Convertir Fecha Emisión a DateTime
                        Total = decimal.Parse(partes[5])  // Convertir Total a decimal
                    };

                    // Agregar la matrícula a la lista
                    matriculas.Add(matricula);
                }
            }

            return matriculas;
        }
    }

}
