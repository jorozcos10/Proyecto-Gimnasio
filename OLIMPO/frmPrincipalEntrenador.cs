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

namespace OLIMPO
{
    public partial class frmPrincipalEntrenador : Form
    {
        string idu;
        public frmPrincipalEntrenador(string idu)
        {
            InitializeComponent();
            this.idu = idu;
            listarReserva();
            verificarEquipos();
           
        }
        public void verificarEquipos()
        {
            ControladorEquipos ce = new ControladorEquipos();
            List<ModeloEquipos> equipos = ce.LeerDatos();
            Console.WriteLine($"Total equipos: {equipos.Count}");

            // Filtrar los equipos que están a 3 meses o menos de cumplir su vida útil
            var equiposCercaDeVencer = equipos.Where(e => e.EsVencimientoProximo()).ToList();

            Console.WriteLine($"Equipos a punto de vencer: {equiposCercaDeVencer.Count}");

            if (equiposCercaDeVencer.Any())
            {
                // Crear la cadena para mostrar en el MessageBox
                string listaEquipos = "Equipos a punto de vencer (dentro de 3 meses):\n\n";

                foreach (var equipo in equiposCercaDeVencer)
                {
                    // Mostrar la información de los equipos
                    listaEquipos += $"{equipo.Nombre} fv: {equipo.FechaCompra.AddMonths(equipo.VidaUtilMeses):yyyy-MM-dd} fc: {equipo.FechaCompra:yyyy-MM-dd}\n";
                }

                // Mostrar la lista de equipos próximos a vencer en un MessageBox
                MessageBox.Show(
                    listaEquipos,                  // El mensaje que contiene la lista
                    "Notificación de Vencimiento",  // Título del MessageBox
                    MessageBoxButtons.OK,          // Botón OK
                    MessageBoxIcon.Warning         // Icono de advertencia
                );
            }
            else
            {
                MessageBox.Show("No hay equipos próximos a vencer dentro de 3 meses.");
            }
        }
        public void listarReserva()
        {
            ControladorReserva cr = new ControladorReserva();
            // Crear un DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Clase", typeof(string));
            dt.Columns.Add("Horario", typeof(string));
            dt.Columns.Add("Usuario",typeof(string));


            List<ModeloReserva> lmr = cr.getByIde(this.idu);
            for (int i = 0; i < lmr.Count; i++)
            {
                ModeloReserva modeloReserva = lmr[i];
                dt.Rows.Add(modeloReserva.Id, modeloReserva.Clase, modeloReserva.Horario,modeloReserva.Nombre);
            }

            // Asigna el DataTable al DataGridView
            dataGridView1.DataSource = dt;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
