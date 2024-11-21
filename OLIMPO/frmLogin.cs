﻿using OLIMPO.Modelo;
using System;
using System.Windows.Forms;

namespace OLIMPO
{
    public partial class frmLogin : Form
    {
        // Declara las variables dentro de la clase
        ModeloUsuarios modeloUsuarios = new ModeloUsuarios();
        ModeloEntrenador modeloEntrenador = new ModeloEntrenador();

        public frmLogin()
        {
            InitializeComponent();

            // Llamamos al método CrearArchivoSiNoExiste para ambos modelos
            modeloUsuarios.CrearArchivoSiNoExiste();
            modeloEntrenador.CrearArchivoSiNoExiste();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmRegistrarEntrenador fRegistrarEntrenador = new frmRegistrarEntrenador();
            fRegistrarEntrenador.ShowDialog();
        }

        private void txtCorreoElectronico_TextChanged(object sender, EventArgs e)
        {
            // Puedes agregar validaciones de correo electrónico aquí si es necesario
        }

        private void RegistrarUsuarios_Click(object sender, EventArgs e)
        {
            frmRegistrarUsuario fRegistrarUsuario = new frmRegistrarUsuario();
            fRegistrarUsuario.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbTipo.Text == "Usuario")
            {
                modeloUsuarios.CorreoElectronico = txtCorreoElectronico.Text;
                modeloUsuarios.Contraseña = txtContraseña.Text;

                if (modeloUsuarios.IniciarSesion())
                {
                    frmPrincipalUsuario fPrincipalUsuario = new frmPrincipalUsuario();
                    fPrincipalUsuario.ShowDialog();
                }
                else
                {
                    MessageBox.Show("ERROR: Los Datos de acceso son incorrectos....");
                }
            }
            else
            {
                modeloEntrenador.CorreoElectronico = txtCorreoElectronico.Text;
                modeloEntrenador.Contraseña = txtContraseña.Text;

                if (modeloEntrenador.IniciarSesion())
                {
                    frmPrincipalEntrenador fPrincipalEntrenador = new frmPrincipalEntrenador();
                    fPrincipalEntrenador.ShowDialog();
                }
                else
                {
                    MessageBox.Show("ERROR: Los Datos de acceso son incorrectos....");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}