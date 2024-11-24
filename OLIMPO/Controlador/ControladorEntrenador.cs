using OLIMPO.Modelo;
using OLIMPO.Helpers;  // Asegúrate de incluir este espacio de nombres
using System.Collections.Generic;
using System.IO;
using System;

public class ControladorEntrenador
{
    private string archivo = FileHelper.GetFilePath("entrenadores.csv");

    // Guardar datos en el archivo
    public ControladorEntrenador()
    {
        Console.WriteLine("se inicializa constructor");
        // Crear el archivo CSV si no existe
        if (!File.Exists(archivo))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivo)); // Crear la carpeta si no existe
            using (var sw = new StreamWriter(archivo, false))
            {
                // Escribir la cabecera del archivo CSV (opcional)
                sw.WriteLine("Id,Nombre,Correo Electrónico,Cédula,Contraseña,Puntos Fuertes,Horarios");
            }
        }
    }
    public int GuardarDatos(ModeloEntrenador entrenador)
    {
        int prev_id = getLastId();
        prev_id++;
        entrenador.id = prev_id;
        using (StreamWriter sw = new StreamWriter(archivo, true)) // Se abre el archivo en modo append
        {
            // Formato de la línea a guardar (ahora incluye la contraseña)
            string linea = $"{entrenador.id},{entrenador.Nombre},{entrenador.CorreoElectronico},{entrenador.Cedula},{entrenador.Contraseña},{entrenador.PuntosFuertes},{entrenador.Horarios}";
            sw.WriteLine(linea);  // Se escribe la línea en el archivo
        }
        return prev_id;
    }



    // Leer los datos del archivo y devolver una lista de entrenadores
    public List<ModeloEntrenador> LeerDatos()
    {
        List<ModeloEntrenador> entrenadores = new List<ModeloEntrenador>();

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

                    // Crea un objeto ModeloEntrenador a partir de las partes
                    ModeloEntrenador entrenador = new ModeloEntrenador
                    {
                        Nombre = partes[0],
                        CorreoElectronico = partes[1],
                        Cedula = partes[2],
                        PuntosFuertes = partes[3],
                        Horarios = partes[4]
                    };

                    // Agrega el entrenador a la lista
                    entrenadores.Add(entrenador);
                }
            }
        }

        return entrenadores;  // Devuelve la lista de entrenadores
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
