using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REAUTO_APP
{
    public partial class Solicitudes_Vacaciones : Form
    {
        bool sidebarExpand; // Indica si la barra lateral está expandida o contraída.
        bool homeCollapsed; // Indica si la sección de reportes está colapsada o expandida.
        //Documentado por:Astrid Gonzales

        // Instancias de clases necesarias para manejar solicitudes, usuarios y respuestas.
        //Documentado por: Astrid Gonzales.
        CN_Inicio ninicio = new CN_Inicio();
        CN_Respuesta_Solicitudes erespuesta = new CN_Respuesta_Solicitudes();
        CE_Respuesta_Solicitudes nrespuesta = new CE_Respuesta_Solicitudes();
        Solicitudes_Aprob Imprimir = new Solicitudes_Aprob();
        CE_Usuarios E_Usuarios = new CE_Usuarios();
        CN_Usuarios n_Usuarios = new CN_Usuarios();
        CN_SolicitudVacaciones nvaca = new CN_SolicitudVacaciones();
        CE_Solicitud_Vacaciones evaca = new CE_Solicitud_Vacaciones();
        private ToolTip toolTip1 = new ToolTip();

        public Solicitudes_Vacaciones()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Controla la animación de la sección de reportes al expandir o colapsar.
        //Documentado por:Astrid Gonzales
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

        // Controla la animación de la barra lateral al expandir o contraer.
        //Documentado por:Astrid Gonzales
        private void SideBarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    SideBarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = !sidebarExpand;
                    SideBarTimer.Stop();
                }
            }
        }

        // Inicia la animación de la barra lateral al hacer clic en el botón de menú.
        //Documentado por:Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Inicia la animación de la sección de reportes al hacer clic en el botón de reportes.
        //Documentado por:Astrid Gonzales
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        // Maneja el comportamiento del campo de texto del código de almacén cuando recibe el foco.
        //Documentado por:Astrid Gonzales
        private void txtNombreVacante_Enter(object sender, EventArgs e)
        {
            if(txtCodigoAlmacen.Text == "Código de almacén")
            {
                txtCodigoAlmacen.Text = "";
                txtCodigoAlmacen.ForeColor = Color.Black;
            }
        }

        // Restaura el texto predeterminado si el usuario no ingresó datos en el campo de código de almacén.
        //Documentado por:Astrid Gonzales
        private void txtCodigoAlmacen_Leave(object sender, EventArgs e)
        {
            if (txtCodigoAlmacen.Text == "")
            {
                txtCodigoAlmacen.Text = "Código de almacén";
                txtCodigoAlmacen.ForeColor = Color.Silver;
            }
        }

        // Muestra una alerta de confirmación y cierra sesión si el usuario acepta.
        //Documentado por:Astrid Gonzales
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("¿Seguro que quieres cerrar sesión?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                login loginform = new login();
                loginform.Show();
                this.Hide();
            }
        }

        // Abre la ventana de administración y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Abre la ventana para crear un nuevo puesto y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Abre la ventana de registro de empleados y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Abre la ventana de creación de usuario y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Abre la ventana de solicitudes de vacaciones y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }

        // Abre la ventana de contratos y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Abre la ventana del reporte de vacaciones y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Abre la ventana del reporte de empleados y oculta la actual.
        //Documentado por:Astrid Gonzales
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        // Evita que el usuario escriba en el ComboBox de respuestas, permitiendo solo selección.
        //Documentado por:Astrid Gonzales
        private void cbxRespuesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Evita que el usuario escriba
        }

        // Se ejecuta cuando se carga el formulario, inicializando elementos de la interfaz.
        //Documentado por:Astrid Gonzales
        private void Solicitudes_Vacaciones_Load(object sender, EventArgs e)
        {
            lbladmin.Text = VariableGlobal.variableusuario;
            txtCodigoAlmacen.Enabled = false;
            cbxRespuesta.Enabled = false;
            cbxRespuesta.Items.Add("Aprobada");
            cbxRespuesta.Items.Add("Rechazada");
            dtgvSolicitudes.Enabled = false;
            btnEnviar.Enabled = false;
            CargarSolicitudes();
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            btnEditar.Enabled = false;
            textBox1.Enabled = false;

            cbxRespuesta.KeyPress += cbxRespuesta_KeyPress;

        }

        // Obtiene las solicitudes de vacaciones y las muestra en el DataGrid.
        //Documentado por:Astrid Gonzales
        private void CargarSolicitudes()
        {
            erespuesta.ActualizarSolicitudesVencidas();
            dtgvSolicitudes.DataSource = ninicio.MostrarSolicitudVacacioneS();
        }

        // Maneja el comportamiento del ComboBox de respuestas cuando recibe el foco.
        //Documentado por:Astrid Gonzales
        private void cbxRespuesta_Enter(object sender, EventArgs e)
        {
            if (cbxRespuesta.Text == "Respuesta")
            {
                cbxRespuesta.Text = "";
                cbxRespuesta.ForeColor = Color.Black;
            }
        }

        // Restaura el texto predeterminado en el ComboBox de respuestas si el usuario no seleccionó ninguna opción.
        //Documentado por:Astrid Gonzales
        private void cbxRespuesta_Leave(object sender, EventArgs e)
        {
            if (cbxRespuesta.Text == "")
            {
                cbxRespuesta.Text = "Respuesta";
                cbxRespuesta.ForeColor = Color.Silver;
            }
        }

        // El botón "Desbloquear" habilita o deshabilita los campos de entrada y botones de acción.
        // Esto permite evitar modificaciones accidentales en los datos antes de su confirmación.
        //documentado por:Astrid gonzales
        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (txtCodigoAlmacen.Enabled)
            {
                txtCodigoAlmacen.Enabled = false;
                cbxRespuesta.Enabled = false;
                btnDesbloquear.Text = "Desbloquear";
                dtgvSolicitudes.Enabled = false;
                btnEnviar.Enabled = false;
                Limpiar();
            }
            else
            {
                txtCodigoAlmacen.Enabled = true;
                cbxRespuesta.Enabled = true;
                btnDesbloquear.Text = "Bloquear";
                dtgvSolicitudes.Enabled = true;
                txtCodigoAlmacen.Enabled = true;
                btnEnviar.Enabled=true;
            }
        }

        // Este botón permite actualizar el estado de una solicitud en la base de datos.  
        // Antes de hacerlo, se validan los datos ingresados para asegurarse de que sean correctos y que la solicitud sea válida.  
        //Documentado por:Astrid Gonzales
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtCodigoAlmacen.Text == "Código de almacén" || cbxRespuesta.Text == "Respuesta")
            {
                MessageBox.Show("Rellene todos los datos.");
                return;
            }

            if (!int.TryParse(txtCodigoAlmacen.Text, out int idSolicitud))
            {
                MessageBox.Show("El Código de Almacén debe ser un número válido.");
                return;
            }

            if (!erespuesta.ExisteSolicitud(idSolicitud)) // Verifica si la solicitud existe
            {
                MessageBox.Show("No existe una solicitud con ese ID.");
                return;
            }

            if (!erespuesta.ValidarFechaSolicitud(idSolicitud)) // Valida si la fecha ya pasó
            {
                MessageBox.Show("No se puede enviar la respuesta. La fecha de la solicitud ya pasó.");
                return;
            }

            E_Usuarios.USUARIO = lbladmin.Text;
            E_Usuarios.ACCION = "Creacion de registro en Respuestas";
            n_Usuarios.GenerarAuditoria(E_Usuarios);

            // Si pasa las validaciones, guarda la respuesta
            nrespuesta.ID_SOLICITUD_V = idSolicitud;
            nrespuesta.RESPUESTA = cbxRespuesta.Text;

            if (erespuesta.RegistrarSolicitud(nrespuesta) > 0)
            {
                MessageBox.Show("Dato almacenado correctamente.");
                CargarSolicitudes();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al almacenar los datos.");
            }
        }

        //Este botón abre la ventana de impresión de documentos y cierra la ventana actual. 
        //Abriendo la posibilidad de imprimir las solicitudes aprobadas.
        //Documentado por: Astrid Gonzales
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir.Show();
            this.Close();

        }
        
        //Este evento se establecio par vaciar los campos asi al memento de darle enviar y no interferiea en procesos futuros
        //Documentado por: Astrid Gonzales
        private void Limpiar() 
        {
            txtCodigoAlmacen.Text = "Código de almacén";
            cbxRespuesta.Text = "Respuesta";
        }

        private void cbxRespuesta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Evento que permite seleccionar una solicitud en el DataGridView y llenar el campo de código de almacén con su ID.  
        //Documentado por: Astrid Gonzales
        private void dtgvSolicitudes_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoAlmacen.Text = dtgvSolicitudes.CurrentRow.Cells[1].Value.ToString();
        }

        // Este botón abre la ventana del Backup y oculta la ventana actual. 
        //Documentado por: Astrid Gonzales
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }

        // Este botón abre la ventana del reporte de auditoría y oculta la ventana actual. 
        //Documentado por: Astrid Gonzales
        private void btnReporteAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        private void btnReportePuestos_Click(object sender, EventArgs e)
        {

        }

        // Este evento permite modificar una solicitud cuando se hace doble clic en la celda "Editar".  
        // Solo las solicitudes con estado "Pendiente" pueden ser modificadas.
        //Documentado por: Astrid Gonzales
        private void dtgvSolicitudes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que el clic sea en una celda válida y en la columna "Editar"
            if (e.RowIndex >= 0 && dtgvSolicitudes.Columns[e.ColumnIndex].Name == "Editar")
            {
                // Obtener el ID de la solicitud y su estado
                int idSolicitud = Convert.ToInt32(dtgvSolicitudes.Rows[e.RowIndex].Cells["ID_SOLICITUD_V"].Value);
                string estadoSolicitud = dtgvSolicitudes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString();

                // Validar si el estado es "Pendiente"
                if (estadoSolicitud.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    // Cargar datos en los controles del formulario
                    textBox1.Text = idSolicitud.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dtgvSolicitudes.Rows[e.RowIndex].Cells["FECHA_INICIO"].Value);
                    dateTimePicker2.Value = Convert.ToDateTime(dtgvSolicitudes.Rows[e.RowIndex].Cells["FECHA_FINAL"].Value);

                    // Habilitar los controles para edición
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    btnEditar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Solo se pueden editar solicitudes con estado 'Pendiente'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en el botón de editar solicitud
        // Valida las fechas ingresadas y luego actualiza la solicitud en la base de datos.
        //Documentado por: Astrid Gonzales
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Validar que las fechas no estén vacías
            if (string.IsNullOrWhiteSpace(textBox1.Text) || dateTimePicker1.Value == DateTime.MinValue || dateTimePicker2.Value == DateTime.MinValue)
            {
                MessageBox.Show("Debe seleccionar ambas fechas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que la fecha de inicio no sea mayor que la fecha final
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los datos actualizados
            int idSolicitud = int.Parse(textBox1.Text);
            DateTime nuevaFechaInicio = dateTimePicker1.Value;
            DateTime nuevaFechaFinal = dateTimePicker2.Value;

            // Llamar al método para actualizar la solicitud
            int filasAfectadas = nvaca.EditarSolicitud(idSolicitud, nuevaFechaInicio, nuevaFechaFinal);

            if (filasAfectadas > 0)
            {
                MessageBox.Show("Solicitud actualizada correctamente.");

                // Deshabilitar controles después de la actualización
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                btnEditar.Enabled = false;
                textBox1.Enabled = false;

                // Opcional: Recargar el DataGridView para reflejar los cambios
                CargarSolicitudes();
            }
            else
            {
                MessageBox.Show("Error al actualizar la solicitud.");
            }
        }

        // Evento para validar la fecha seleccionada en dateTimePicker1
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Today.AddDays(1))
            {
                MessageBox.Show("No puedes seleccionar una fecha pasada. Selecciona una fecha a partir de mañana.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker1.Value = DateTime.Today.AddDays(1); // Forzar a la fecha mínima
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/Qcrz-YFZsZE";
            System.Diagnostics.Process.Start(url);
        }
    }
}
