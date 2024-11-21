using OLIMPO.Modelo;
using OLIMPO.Helpers;  // Asegúrate de incluir este espacio de nombres
using System.Collections.Generic;
using System.IO;

public class ControladorEntrenador
{
    private string archivo = FileHelper.GetFilePath("entrenadores.csv");

    // Guardar datos en el archivo
    public void GuardarDatos(ModeloEntrenador entrenador)
    {
        using (StreamWriter sw = new StreamWriter(archivo, true)) // Se abre el archivo en modo append
        {
            // Formato de la línea a guardar (ahora incluye la contraseña)
            string linea = $"{entrenador.Nombre},{entrenador.CorreoElectronico},{entrenador.Cedula},{entrenador.Contraseña},{entrenador.PuntosFuertes},{entrenador.Horarios}";
            sw.WriteLine(linea);  // Se escribe la línea en el archivo
        }
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
}
