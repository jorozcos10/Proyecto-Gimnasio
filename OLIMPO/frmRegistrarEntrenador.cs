using OLIMPO.Controlador;
using System;
using System.Windows.Forms;
using OLIMPO.Modelo;
using System.Collections.Generic;
using System.Data;

namespace OLIMPO
{
    public partial class frmRegistrarEntrenador : Form
    {
        private ControladorEntrenador controlador;
        List<ModeloReserva> listaReserva;
        ControladorReserva cr;
        public frmRegistrarEntrenador()
        {
            InitializeComponent();
            controlador = new ControladorEntrenador();

            ControladorClase clase = new ControladorClase();

             cr = new ControladorReserva();

            List<ModeloClase> clases = clase.LeerDatos();

            ControladorHorario ho = new ControladorHorario();

            List<ModeloHorario> horarios = ho.LeerDatos();
            listaReserva = new List<ModeloReserva>();


            cmbPf.DataSource = clases;
            cmbPf.DisplayMember = "Nombre"; // Propiedad que se mostrará
            cmbPf.ValueMember = "Id";

            cmbHorario.DataSource = horarios;
            cmbHorario.DisplayMember = "Horario";
            cmbHorario.ValueMember = "Id";


        }

     

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ModeloEntrenador entrenador = new ModeloEntrenador
            {
                Nombre = txtNombre.Text,
                CorreoElectronico = txtCorreoElectronico.Text,
                Cedula = txtCedula.Text,
                Contraseña = txtContraseña.Text,
               // PuntosFuertes = txtPuntosFuertes.Text,
               // Horarios = txtHorarios.Text
            };

          int id =  controlador.GuardarDatos(entrenador);

           
            foreach(var item in listaReserva)
            {
                item.Idu =id;
                cr.GuardarDatosEntrenador(item, txtNombre.Text);

            }
           

            MessageBox.Show("Datos del entrenador guardados correctamente.");

            // Limpiar los campos
            txtNombre.Text = "";
            txtCorreoElectronico.Text = "";
            txtCedula.Text = "";
            txtContraseña.Text = "";
          //  txtPuntosFuertes.Text = "";
           // txtHorarios.Text = "";
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clbClase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModeloClase claseSeleccionada = (ModeloClase)cmbPf.SelectedItem;
            ModeloHorario horarioSeleccionada = (ModeloHorario)cmbHorario.SelectedItem;
            String clase = claseSeleccionada.Nombre;
            String horario = horarioSeleccionada.Horario;
            ModeloReserva mr = new ModeloReserva
           
            {    
                Clase = clase,
                Horario = horario,
               
            };
            listaReserva.Add(mr);
            listarReserva();
        }
        public void listarReserva()
        {
            ControladorReserva cr = new ControladorReserva();
            // Crear un DataTable
            DataTable dt = new DataTable();
           
            dt.Columns.Add("Clase", typeof(string));
            dt.Columns.Add("Horario", typeof(string));


          
            for (int i = 0; i < listaReserva.Count; i++)
            {
                ModeloReserva modeloReserva = listaReserva[i];
                dt.Rows.Add(modeloReserva.Clase, modeloReserva.Horario);
            }

            // Asigna el DataTable al DataGridView
            dataGridView1.DataSource = dt;
        }
    }
}
