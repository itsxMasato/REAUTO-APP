using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace REAUTO_APP
{
    public partial class Solicitar_Vacaciones_Empleado : Form
    {
        private ToolTip toolTip1 = new ToolTip();

        // Variable para gestionar la expansión y contracción de la barra lateral
        bool sidebarExpand;

        // Instancias de clases para manejar solicitudes y usuarios
        CD_Solicitud_Vacaciones nsolicitud = new CD_Solicitud_Vacaciones();
        CE_Solicitud_Vacaciones esolicitud = new CE_Solicitud_Vacaciones();
        CN_SolicitudVacaciones vacaciones = new CN_SolicitudVacaciones();
        CE_Usuarios E_Usuarios = new CE_Usuarios();
        CN_Usuarios n_Usuarios = new CN_Usuarios();

        public Solicitar_Vacaciones_Empleado()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Método que se ejecuta cada vez que el temporizador del sidebar (barra lateral) alcanza su tick.
        // Maneja la expansión o contracción de la barra lateral dependiendo de su estado actual.
        //Documentado por: Astrid Gonzales
        private void CargarSolicitudes()
        {
            string usuario = VariableGlobal.variableusuario;
            int idEmpleado = vacaciones.ObtenerIDEmpleadoPorUsuario(usuario);

            dtgvInicio.DataSource = nsolicitud.MostrarSolicitudesE(idEmpleado);
        }

        // Evento que se dispara cuando el usuario hace clic en el botón del menú. 
        // Inicia el temporizador para mostrar o ocultar el sidebar.
        //Documentado por: Astrid Gonzales
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

        // Evento que se dispara cuando el usuario hace clic en el botón del menú. 
        // Inicia el temporizador para mostrar o ocultar el sidebar.
        //Documentado por: Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Evento que se ejecuta al hacer clic en el botón de cerrar sesión. 
        // Muestra un mensaje de confirmación y, si se confirma, cierra la sesión y redirige al formulario de login.
        //Documentado por: Astrid Gonzales
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

        // Evento que se ejecuta cuando se hace clic en el botón de Sueldo. 
        // Abre el formulario para ver el sueldo del empleado y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnSueldo_Click(object sender, EventArgs e)
        {
            Sueldo_Empleado sueldo_Empleado = new Sueldo_Empleado();
            sueldo_Empleado.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Inicio".
        // Abre el formulario de Inicio del empleado y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Inicio_Empleado inicio_Empleado = new Inicio_Empleado();
            inicio_Empleado.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Método que valida y envía la solicitud de vacaciones cuando el empleado hace clic en el botón "Enviar".
        // Verifica que los campos de fecha no estén vacíos, valida que la fecha final no sea menor que la fecha de inicio
        // y verifica que no exista una solicitud pendiente para el empleado. Si todo es correcto, la solicitud se registra.
        //Documentado por: Astrid Gonzales
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (dtpInicio.Text == "" || dtpFinal.Text == "")
            {
                MessageBox.Show("Rellene todos los datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Evita continuar si hay campos vacíos
            }

            // Validar que la fecha final no sea menor a la fecha de inicio
            if (dtpFinal.Value < dtpInicio.Value)
            {
                MessageBox.Show("La fecha final no puede ser menor que la fecha de inicio.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale de la función sin registrar la solicitud
            }

            

            int dias = (dtpFinal.Value - dtpInicio.Value).Days;
            esolicitud.FECHA_INICIO = dtpInicio.Value;
            esolicitud.FECHA_FINAL = dtpFinal.Value;
            esolicitud.DIAS_SOLICITADOS = dias;

            string usuario = VariableGlobal.variableusuario;
            esolicitud.ID_EMPLEADO = vacaciones.ObtenerIDEmpleadoPorUsuario(usuario);
            esolicitud.ESTADO = "Pendiente";

            // Validar si el empleado ya tiene una solicitud pendiente
            if (vacaciones.ExisteSolicitudPendiente(esolicitud.ID_EMPLEADO))
            {
                MessageBox.Show("Ya existe una solicitud pendiente. No puede enviar otra hasta que se reciba una respuesta.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si pasa todas las validaciones, se registra la solicitud
            if (vacaciones.RegistrarSolicitud(esolicitud) > 0)
            {
                E_Usuarios.USUARIO = lblempleado.Text;
                E_Usuarios.ACCION = "Creacion de registro en Solicitud_Vacaciones";
                n_Usuarios.GenerarAuditoria(E_Usuarios);

                MessageBox.Show("Solicitud enviada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSolicitudes();
            }
            else
            {
                MessageBox.Show("Error al enviar la solicitud.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dtgvInicio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Método que se ejecuta al cargar el formulario. 
        // Carga las solicitudes de vacaciones y asigna el usuario actual al label para mostrarlo.
        //Documentado por: Astrid Gonzales
        private void Solicitar_Vacaciones_Empleado_Load(object sender, EventArgs e)
        {
            CargarSolicitudes();
            lblempleado.Text = VariableGlobal.variableusuario;
        }

        private void txtYearFinal_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblempleado_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/8DOa02jCDq0?si=e61yt946YXqpAfOI";
            System.Diagnostics.Process.Start(url);
        }
    }
}
