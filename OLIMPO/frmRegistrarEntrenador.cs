using OLIMPO.Controlador;
using System;
using System.Windows.Forms;
using OLIMPO.Modelo;

namespace OLIMPO
{
    public partial class frmRegistrarEntrenador : Form
    {
        private ControladorEntrenador controlador;

        public frmRegistrarEntrenador()
        {
            InitializeComponent();
            controlador = new ControladorEntrenador();
        }

     

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ModeloEntrenador entrenador = new ModeloEntrenador
            {
                Nombre = txtNombre.Text,
                CorreoElectronico = txtCorreoElectronico.Text,
                Cedula = txtCedula.Text,
                Contraseña = txtContraseña.Text,
                PuntosFuertes = txtPuntosFuertes.Text,
                Horarios = txtHorarios.Text
            };

            controlador.GuardarDatos(entrenador);

            MessageBox.Show("Datos del entrenador guardados correctamente.");

            // Limpiar los campos
            txtNombre.Text = "";
            txtCorreoElectronico.Text = "";
            txtCedula.Text = "";
            txtContraseña.Text = "";
            txtPuntosFuertes.Text = "";
            txtHorarios.Text = "";
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
