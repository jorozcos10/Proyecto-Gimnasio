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
    }
}
