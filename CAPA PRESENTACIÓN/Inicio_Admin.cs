using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Datos;
using Capa_Negocio;

namespace REAUTO_APP
{
    public partial class Inico_Admin : Form
    {
        // Indica si la barra lateral está expandida o colapsada. 
        // Se utiliza para controlar el estado de la interfaz de usuario (sidebar).
        // Documentado por: Miguel Flores
        bool sidebarExpand;

        // Indica si la vista principal de inicio está colapsada. 
        // Se usa para controlar la visualización de la sección de inicio en la interfaz.
        bool homeCollapsed;

        // Instancia de la clase CN_Inicio para manejar la lógica relacionada con el inicio de la aplicación.
        // Este objeto se utiliza para interactuar con la lógica de negocio de la pantalla de inicio.
        CN_Inicio ninicio = new CN_Inicio();

        // Declaración de un objeto SqlCommand que se utilizará para ejecutar consultas SQL.
        // Este comando se usará para interactuar con la base de datos en las operaciones SQL.
        SqlCommand cmd;

        private ToolTip toolTip1 = new ToolTip();

        public Inico_Admin()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Controla el cambio en el tamaño de la barra lateral cada vez que el temporizador (sidebarTimer) se activa. 
        // Si la barra está expandida, la reduce, y si está contraída, la expande. 
        // El tamaño de la barra cambia gradualmente en pasos de 10 píxeles hasta que alcanza su tamaño mínimo o máximo.
        // Documentado por: Miguel Flores
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = !sidebarExpand;
                    sidebarTimer.Stop();
                }
            }
        }

        // Inicia el temporizador que controla la expansión o contracción de la barra lateral.
        // Este método se llama cuando el botón de menú es presionado.
        // Documentado por: Miguel Flores
        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        // Controla el cambio en el tamaño del panel de reportes cada vez que el temporizador (ReportTimer) se activa.
        // Si el panel de reportes está contraído, lo expande, y si está expandido, lo contrae. 
        // El tamaño del panel cambia gradualmente en pasos de 10 píxeles hasta que alcanza su tamaño mínimo o máximo.
        // Documentado por: Miguel Flores
        private void ReportTimer_Tick(object sender, EventArgs e)
        {
            if (homeCollapsed)
            {
                Reportes.Height += 10;
                if (Reportes.Height >= Reportes.MaximumSize.Height)
                {
                    homeCollapsed = false;
                    ReportTimer.Stop();
                }
            }
            else
            {
                Reportes.Height -= 10;
                if (Reportes.Height <= Reportes.MinimumSize.Height)
                {
                    homeCollapsed = true;
                    ReportTimer.Stop();
                }
            }

        }

        // Inicia el temporizador que controla la expansión o contracción del panel de reportes.
        // Este método se llama cuando el botón de reportes es presionado.
        // Documentado por: Miguel Flores
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {

        }

        // Abre la ventana para crear un nuevo puesto y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Abre la ventana para registrar un nuevo empleado y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Abre la ventana para crear un nuevo usuario y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_usuario = new Crear_Usuario();
            crear_usuario.Show();
            this.Hide();
        }

        // Abre la ventana de solicitudes de vacaciones y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes = new Solicitudes_Vacaciones();
            solicitudes.Show();
            this.Hide();
        }

        // Abre la ventana de contratos y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Abre la ventana de reporte de vacaciones y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Abre la ventana de reporte de empleados y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_empleados = new Reporte_Empleados();
            reporte_empleados.Show();
            this.Hide();
        }

        // Carga los datos de la entidad 'Inicio' en el DataGridView.
        // Documentado por: Miguel Flores
        private void CargarInicio()
        {
            dtgvInicio.DataSource = ninicio.MostrarInicio();
        }

        // Inicializa los datos y configura los elementos al cargar la ventana de administración.
        // Documentado por: Miguel Flores
        private void Inico_Admin_Load_1(object sender, EventArgs e)
        {
            lbladmin.Text = VariableGlobal.variableusuario;
            EmpleadosTotales();
            EmpleadosDeVacaciones();
            SolicitudesPendientes2();
            GrafVacacionesMes();
            GrafPastel();
            CargarInicio();

            cbxFiltro.Items.Add("Todos"); 
            cbxFiltro.Items.Add("Enero");
            cbxFiltro.Items.Add("Febrero");
            cbxFiltro.Items.Add("Marzo");
            cbxFiltro.Items.Add("Abril");
            cbxFiltro.Items.Add("Mayo");
            cbxFiltro.Items.Add("Junio");
            cbxFiltro.Items.Add("Julio");
            cbxFiltro.Items.Add("Agosto");
            cbxFiltro.Items.Add("Septiembre");
            cbxFiltro.Items.Add("Octubre");
            cbxFiltro.Items.Add("Noviembre");
            cbxFiltro.Items.Add("Diciembre");
            cbxFiltro.SelectedIndex = 0;
        }

        private int ObtenerMesSeleccionado()
        {
            if (cbxFiltro.SelectedIndex == 0) return 0; // "Todos" -> Sin filtro
            return cbxFiltro.SelectedIndex; // Enero = 1, Febrero = 2, ..., Diciembre = 12
        }

        private int ObtenerMesEstatico()
        {
            return 0; // "Todos" -> Sin filtro
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        // Obtiene el total de empleados desde la base de datos ejecutando un procedimiento almacenado.
        // Documentado por: Miguel Flores
        private void EmpleadosTotales()
        {
            CN_Conexion conecta = new CN_Conexion();
            using (SqlConnection conexion = conecta.ObtenerConexion())
            {
                if (conexion == null)
                {
                    MessageBox.Show("Error: No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                cmd = new SqlCommand("sp_Cantidad_Empleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter total = new SqlParameter("@Cantidad_Empleados", SqlDbType.Int);
                total.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(total);

                cmd.ExecuteNonQuery(); // Ejecuta el procedimiento
                lblTotalEmpleados.Text = total.Value.ToString(); // Obtiene el valor
            }
        }

        // Obtiene el total de empleados de vacaciones ejecutando un procedimiento almacenado.
        // Documentado por: Miguel Flores
        private void EmpleadosDeVacaciones()
        {
            CN_Conexion conecta = new CN_Conexion();
            using (SqlConnection conexion = conecta.ObtenerConexion())
            {
                if (conexion == null)
                {
                    MessageBox.Show("Error: No se pudo establecer la conexión con la base de datos.");
                    return;
                }
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                cmd = new SqlCommand("sp_Empleados_De_Vacaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                int mes = ObtenerMesEstatico();
                cmd.Parameters.AddWithValue("@Mes", mes == 0 ? (object)DBNull.Value : 0);

                SqlParameter total = new SqlParameter("@Cantidad_Vacaciones", SqlDbType.Int);
                total.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(total);

                cmd.ExecuteNonQuery();
                lblEmpleadoDeVacaciones.Text = total.Value.ToString();
            }
        }

        // Obtiene el total de solicitudes pendientes ejecutando un procedimiento almacenado.
        // Documentado por: Miguel Flores
        private void SolicitudesPendientes2()
        {
            CN_Conexion conecta = new CN_Conexion();
            using (SqlConnection conexion = conecta.ObtenerConexion())
            {
                if (conexion == null)
                {
                    MessageBox.Show("Error: No se pudo establecer la conexión con la base de datos.");
                    return;
                }
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                cmd = new SqlCommand("sp_Solicitudes_Pendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                int mes = ObtenerMesSeleccionado();
                cmd.Parameters.AddWithValue("@Mes", mes == 0 ? (object)DBNull.Value : mes);

                SqlParameter total = new SqlParameter("@Solicitudes_Pendientes", SqlDbType.Int);
                total.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(total);

                cmd.ExecuteNonQuery();
                lblSolicitudesPendientes.Text = total.Value.ToString();
            }
        }

        // Listas para almacenar los diferentes estados y solicitudes
        ArrayList Estado = new ArrayList();  // Lista para almacenar estados
        ArrayList Solicitudes = new ArrayList();  // Lista para almacenar solicitudes

        // Grafica las vacaciones de los empleados por mes ejecutando un procedimiento almacenado.
        // Documentado por: Miguel Flores
        private void GrafVacacionesMes()
        {
            CN_Conexion conecta = new CN_Conexion();
            using (SqlConnection conexion = conecta.ObtenerConexion())
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                using (SqlCommand cmd = new SqlCommand("Empleados_Vacaciones_Por_Mes", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Estado.Clear();  // Limpiar la lista antes de agregar nuevos datos
                        Solicitudes.Clear();  // Limpiar la lista antes de agregar nuevos datos

                        while (dr.Read())
                        {
                            string estado = dr.IsDBNull(0) ? "Desconocido" : dr.GetString(0);
                            int solicitudes = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);

                            // Traducir los meses de inglés a español
                            estado = TraducirMes(estado);

                            Estado.Add(estado);
                            Solicitudes.Add(solicitudes);
                        }
                    }

                    if (chartempleados != null && chartempleados.Series.Count > 0)
                    {
                        chartempleados.Series[0].Points.Clear();  // Limpiar puntos antes de agregar nuevos
                        chartempleados.Series[0].Points.DataBindXY(Estado, Solicitudes);
                    }
                }
            }
        }

        // Traduce el nombre de los meses del inglés al español.
        // Documentado por: Miguel Flores
        private string TraducirMes(string mesIngles)
        {
            Dictionary<string, string> meses = new Dictionary<string, string>()
            {
                { "January", "Enero" },
                { "February", "Febrero" },
                { "March", "Marzo" },
                { "April", "Abril" },
                { "May", "Mayo" },
                { "June", "Junio" },
                { "July", "Julio" },
                { "August", "Agosto" },
                { "September", "Septiembre" },
                { "October", "Octubre" },
                { "November", "Noviembre" },
                { "December", "Diciembre" }
            };

            return meses.ContainsKey(mesIngles) ? meses[mesIngles] : mesIngles;
        }

        // Grafica las solicitudes de estado en un gráfico de pastel ejecutando un procedimiento almacenado.
        // Documentado por: Miguel Flores
        private void GrafPastel()
        {
            CN_Conexion conecta = new CN_Conexion();
            using (SqlConnection conexion = conecta.ObtenerConexion())
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                using (SqlCommand cmd = new SqlCommand("sp_Solicitudes_Estado", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int mes = ObtenerMesSeleccionado();
                    cmd.Parameters.AddWithValue("@Mes", mes == 0 ? (object)DBNull.Value : mes);

                    // Limpiar listas antes de agregar datos nuevos
                    Estado.Clear();
                    Solicitudes.Clear();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string estado1 = dr.IsDBNull(0) ? "Desconocido" : dr.GetString(0);
                            int solicitudes1 = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);

                            Estado.Add(estado1);
                            Solicitudes.Add(solicitudes1);
                        }
                    }

                    if (CharPastel != null && CharPastel.Series.Count > 0)
                    {
                        CharPastel.Series[0].Points.Clear();  // Limpiar puntos antes de agregar nuevos
                        CharPastel.Series[0].Points.DataBindXY(Estado, Solicitudes);
                    }
                }
            }
        }

        private void dtgvInicio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Maneja el cambio de selección en el filtro de meses, actualizando las gráficas y datos asociados.
        // Documentado por: Miguel Flores
        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpleadosDeVacaciones();
            SolicitudesPendientes2();
            GrafVacacionesMes();
            GrafPastel();
        }

        private void PBEmpleado_Click(object sender, EventArgs e)
        {

        }

        // Abre la ventana de backup y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }

        // Muestra un mensaje de confirmación al usuario antes de cerrar sesión y, si confirma, abre el formulario de login.
        // Documentado por: Miguel Flores
        private void btnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("¿Seguro que quieres cerrar sesión?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                login loginform = new login();
                loginform.Show();
                this.Hide();
            }
        }

        // Abre el formulario de Auditoría y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void BtnAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        // Abre el formulario de Reporte de Puestos y oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void btnReportePues_Click(object sender, EventArgs e)
        {
            Reporte_Puestos reporte_Puestos = new Reporte_Puestos();
            reporte_Puestos.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/u06RniPPPjQ";
            System.Diagnostics.Process.Start(url);
        }
    }
}
