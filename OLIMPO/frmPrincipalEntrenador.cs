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
using System.Windows.Forms.DataVisualization.Charting;

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
            listarInventario();
            // grafico();
            resporteMatriculas();

        }
        
      /*  public void grafico() {
            // Limpiar cualquier serie previa
            chart1.Series.Clear();

            // Crear una nueva serie para el gráfico de barras
            Series series = new Series("Matrícula");
            series.ChartType = SeriesChartType.Column;  // Tipo de gráfico: barras
            series.Points.AddXY("Enero", 1000);
            series.Points.AddXY("Febrero", 1200);
            series.Points.AddXY("Marzo", 900);
            series.Points.AddXY("Abril", 1100);

            // Agregar la serie al gráfico
            chart1.Series.Add(series);

            // Configurar el área del gráfico
            chart1.ChartAreas[0].AxisX.Title = "Mes";  // Eje X con los meses
            chart1.ChartAreas[0].AxisY.Title = "Matrícula";  // Eje Y con los valores

            // Opcional: Configurar los límites de los ejes si es necesario
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
        }*/
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
        public void listarInventario()
        {
            ControladorEquipos cr = new ControladorEquipos();
            // Crear un DataTable
            DataTable dt = new DataTable();
          
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha Compra", typeof(string));
            dt.Columns.Add("Fecha vencimiento", typeof(string));


            List<ModeloEquipos> lmr = cr.LeerDatos();
            for (int i = 0; i < lmr.Count; i++)
            {
                ModeloEquipos modeloEquipos = lmr[i];
                dt.Rows.Add(modeloEquipos.Id, modeloEquipos.Nombre, modeloEquipos.FechaCompra, modeloEquipos.FechaVencimiento);
            }

            // Asigna el DataTable al DataGridView
            dtgInventario.DataSource = dt;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public void resporteMatriculas()
        {
            // Leer el archivo CSV y almacenar los datos
            ControladorFactura cf = new ControladorFactura();
            var matriculas = cf.listarMatriculas();
            int totalMatriculas = matriculas.Count();  // Contar todas las matrículas
            decimal montoTotal = matriculas.Sum(m => m.Total);
            lblAlumnos.Text ="Alumnos: " +totalMatriculas.ToString();
            lblIngresos.Text ="Ingresos: "+montoTotal.ToString();


            // Agrupar por mes y año, y calcular el total por mes
            var totalPorMes = matriculas
                .Where(m => m.FechaEm != null) // Filtrar por fecha no nula
                .GroupBy(m => new { m.FechaEm.Year, m.FechaEm.Month })
                .Select(g => new MatriculaPorMes
                {
                    Año = g.Key.Year,
                    Mes = g.Key.Month,
                    Total = g.Count()
                })
                .OrderBy(g => g.Año)
                .ThenBy(g => g.Mes)
                .ToList();

            // Llamar al método para cargar el gráfico con los datos obtenidos
            grafico(totalPorMes);
        }

        public void grafico(List<MatriculaPorMes> totalPorMes)
        {
            // Limpiar cualquier serie previa
            chart1.Series.Clear();

            // Crear una nueva serie para el gráfico de barras
            Series series = new Series("Matrícula");
            series.ChartType = SeriesChartType.Column;  // Tipo de gráfico: columnas

            // Iterar sobre los resultados de la agrupación por mes y año
            foreach (var item in totalPorMes)
            {
                // Convertir el mes numérico a su nombre correspondiente (opcional)
                string mesNombre = ObtenerNombreMes(item.Mes);

                // Agregar los datos al gráfico
                series.Points.AddXY($"{mesNombre} {item.Año}", item.Total);
            }

            // Agregar la serie al gráfico
            chart1.Series.Add(series);

            // Configurar el área del gráfico
            chart1.ChartAreas[0].AxisX.Title = "Meses";
            chart1.ChartAreas[0].AxisY.Title = "Matrícula Total";

            // Opcional: Configurar los límites de los ejes si es necesario
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            // Ajustar el gráfico para que ocupe todo el espacio
           // chart1.Dock = DockStyle.Fill;
        }


        // Método para obtener el nombre del mes a partir del número (1 = Enero, 12 = Diciembre)
        private string ObtenerNombreMes(int mes)
        {
            string[] meses = new string[] {
        "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
        "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
    };
            return meses[mes - 1];  // Restamos 1 porque los meses empiezan desde 1
        }
    }
}
