using System;
using System.IO;
using System.Windows.Forms;
using GimnasioProyecto.Helpers;
using GimnasioProyecto.Configuracion;
using System.Linq;

namespace Gimnasio_proyecto
{
    public partial class frmMembresia : Form
    {
        private string cedulaCliente;

        public frmMembresia(string cedula)
        {
            InitializeComponent();
            cedulaCliente = cedula;
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            lblCedula.Text = $"Cédula: {cedulaCliente}";
            lblInicio.Text = $"Fecha de Inicio: {DateTime.Now:yyyy-MM-dd}";
            lblFinal.Text = $"Fecha de Expiración: {DateTime.Now.AddDays(30):yyyy-MM-dd}";

            // Mostrar el costo fijo desde la clase CostoMembresia
            lblCosto.Text = $"Costo: {CostoMembresia.ObtenerCosto():C2}"; // Formato de moneda
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            decimal costo = CostoMembresia.ObtenerCosto();
            string fechaInicio = DateTime.Now.ToString("yyyy-MM-dd");
            string fechaFinal = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

            // Guardar en membresias.csv
            GuardarDatos(FileHelper.GetRutaArchivo("membresias.csv"), $"{cedulaCliente},{fechaInicio},{fechaFinal},{costo}");

            // Guardar en facturacion.csv
            GuardarDatos(FileHelper.GetRutaArchivo("facturacion.csv"), $"{cedulaCliente},{fechaInicio},{fechaFinal},{costo}");

            MessageBox.Show("Membresía comprada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void GuardarDatos(string rutaArchivo, string linea)
        {
            try
            {
                // Si el archivo no existe, crea el archivo y agrega encabezados
                if (!File.Exists(rutaArchivo))
                {
                    using (var sw = new StreamWriter(rutaArchivo, false))
                    {
                        sw.WriteLine("Cedula,FechaInicio,FechaFinal,Costo");
                    }
                }

                // Leer líneas existentes y filtrar líneas vacías
                var lineasExistentes = File.ReadAllLines(rutaArchivo)
                                           .Where(l => !string.IsNullOrWhiteSpace(l))
                                           .ToList();

                // Agregar la nueva línea
                lineasExistentes.Add(linea);

                // Sobrescribir el archivo con las líneas actualizadas
                File.WriteAllLines(rutaArchivo, lineasExistentes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
