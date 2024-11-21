using OLIMPO.Controlador;
using OLIMPO.Modelo;
using System;
using System.Windows.Forms;

namespace OLIMPO
{
    public partial class frmRegistrarUsuario : Form
    {
        private ControladorUsuarios controlador;

        public frmRegistrarUsuario()
        {
            InitializeComponent();
            controlador = new ControladorUsuarios(); // Inicializa el controlador
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturar los datos ingresados en el formulario
                ModeloUsuarios modelo = new ModeloUsuarios
                {
                    Nombre = txtNombre.Text.Trim(),
                    CorreoElectronico = txtCorreoElectronico.Text.Trim(),
                    Cedula = txtCedula.Text.Trim(),
                    Contraseña = txtContraseña.Text.Trim()
                };

                // Validar que todos los campos estén llenos
                if (string.IsNullOrEmpty(modelo.Nombre) ||
                    string.IsNullOrEmpty(modelo.CorreoElectronico) ||
                    string.IsNullOrEmpty(modelo.Cedula) ||
                    string.IsNullOrEmpty(modelo.Contraseña))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Guardar los datos utilizando el controlador
                controlador.GuardarDatos(modelo);

                // Mostrar un mensaje de éxito
                MessageBox.Show("Datos del usuario guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error en caso de excepción
                MessageBox.Show("Error al registrar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cierra el formulario
            this.Close();
        }

        private void LimpiarCampos()
        {
            // Limpiar todos los campos del formulario
            txtNombre.Text = "";
            txtCorreoElectronico.Text = "";
            txtCedula.Text = "";
            txtContraseña.Text = "";
        }
    }
}
