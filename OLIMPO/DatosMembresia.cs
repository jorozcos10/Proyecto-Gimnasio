using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GimnasioProyecto.Helpers;

namespace Gimnasio_proyecto
{
    public partial class frmDatosMembresia : Form
    {
        private string cedulaCliente;

        public frmDatosMembresia(string cedula)
        {
            InitializeComponent();
            cedulaCliente = cedula;
            CargarDatosMembresia();
        }

        private void CargarDatosMembresia()
        {
            string rutaMembresias = FileHelper.GetRutaArchivo("membresias.csv");

            if (!File.Exists(rutaMembresias))
            {
                MessageBox.Show("El archivo de membresías no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var lineas = File.ReadAllLines(rutaMembresias).Skip(1); // Omitir encabezado
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    if (datos[0] == cedulaCliente)
                    {
                        lblCedula.Text = $"Cédula: {datos[0]}";
                        lblInicio.Text = $"Fecha de Inicio: {datos[1]}";
                        lblFinal.Text = $"Fecha de Expiración: {datos[2]}";
                        lblCosto.Text = $"Costo: {decimal.Parse(datos[3]):C2}";
                        return;
                    }
                }

                MessageBox.Show("No se encontraron datos de membresía para este cliente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos de la membresía: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
