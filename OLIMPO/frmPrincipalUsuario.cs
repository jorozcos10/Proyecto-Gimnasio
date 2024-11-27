using OLIMPO.Controlador;
using OLIMPO.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace OLIMPO
{
    public partial class frmPrincipalUsuario : Form
    {
        string idu = "";
        string nombre= "";
        ControladorReserva cr;
        public frmPrincipalUsuario(String idu, string nombre,string fechav)
        {
            //  tabControl.TabPages["tabPage1"].Text = "Nueva Reserva";
            DateTime fecha;
            // Fecha actual
            DateTime fechaActual = DateTime.Now;
            if (DateTime.TryParse(fechav, out fecha))
            {
                // Obtenemos la fecha de vencimiento (1 mes después de la fecha de registro)
                DateTime fechaVencimiento = fecha.AddMonths(1);

                // Calcular la diferencia entre la fecha de vencimiento y la fecha actual
                TimeSpan diferencia = fechaVencimiento - fechaActual;

                // Verificar si la diferencia es menor o igual a 5 días (y que aún no haya pasado la fecha de vencimiento)
                if (diferencia.Days <= 5 && diferencia.Days >= 0)
                {
                    // Notificación urgente para el cliente (5 días antes de la fecha de vencimiento)
                    MessageBox.Show(
                  $"Notificación urgente para {nombre}: Su mensualidad vence en {diferencia.Days} días, el {fechaVencimiento.ToString("yyyy-MM-dd")}.",
                  "Aviso de vencimiento",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning
              );
                }
            }
            else
            {
                Console.WriteLine("error al convertir");
            }

            this.idu = idu;
            this.nombre = nombre;
            InitializeComponent();
            ControladorClase clase = new ControladorClase();

            List<ModeloClase> clases = clase.LeerDatos();

            ControladorHorario ho = new ControladorHorario();

            List<ModeloHorario> horarios = ho.LeerDatos();



            cmbClase.DataSource = clases;
            cmbClase.DisplayMember = "Nombre"; // Propiedad que se mostrará
            cmbClase.ValueMember = "Id";

            cmbHorario.DataSource = horarios;
            cmbHorario.DisplayMember = "Horario";
            cmbClase.ValueMember = "Id";
            listarReserva();
            cr = new ControladorReserva();
            this.nombre = nombre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            ModeloClase claseSeleccionada = (ModeloClase)cmbClase.SelectedItem;
            ModeloHorario horarioSeleccionada = (ModeloHorario)cmbHorario.SelectedItem;
            ModeloReserva entrenadorSeleccionado = (ModeloReserva)cmbEntrenador.SelectedItem;

            if (entrenadorSeleccionado == null) {
                MessageBox.Show("ERROR: No se encontro un entrenador");
                return;
            }
            String clase = claseSeleccionada.Nombre;
            String horario = horarioSeleccionada.Horario;
            ModeloReserva mr = new ModeloReserva
            {
                Clase = clase,
                Horario = horario,
                Idu = int.Parse(this.idu),
                Nombre = nombre,
                Ide = entrenadorSeleccionado.Idu,
                Entrenador = entrenadorSeleccionado.Nombre
            };
           
            cr.GuardarDatos(mr);
            listarReserva();


        }
        public void listarReserva() {
            ControladorReserva cr = new ControladorReserva();
            // Crear un DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Clase", typeof(string));
            dt.Columns.Add("Horario", typeof(string));


            List<ModeloReserva> lmr = cr.getByIdu(this.idu);
            for (int i = 0; i < lmr.Count; i++)
            {
                ModeloReserva modeloReserva = lmr[i];
                dt.Rows.Add(modeloReserva.Id, modeloReserva.Clase, modeloReserva.Horario);
            }

            // Asigna el DataTable al DataGridView
            dataGridView1.DataSource = dt;
        }

        private void frmPrincipalUsuario_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            refrecarComboEntrenador();



        }

        private void cmbEntrenador_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            refrecarComboEntrenador();

        }
        public void refrecarComboEntrenador() {
            cr = new ControladorReserva();
            ModeloClase claseSeleccionada = (ModeloClase)cmbClase.SelectedItem;
            ModeloHorario horarioSeleccionada = (ModeloHorario)cmbHorario.SelectedItem;
            cmbEntrenador.Text = string.Empty;
            if (horarioSeleccionada == null) { 
             return;
            }
            List<ModeloReserva> listaEntrenadores = cr.getEntrenador(claseSeleccionada.Nombre, horarioSeleccionada.Horario);
            cmbEntrenador.DataSource = listaEntrenadores;
            cmbEntrenador.DisplayMember = "Nombre"; // Propiedad que se mostrará
            cmbEntrenador.ValueMember = "Id";
        }
    }
}
