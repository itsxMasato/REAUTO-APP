using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Capa_Negocio.CN_Empleado;
using Capa_Datos;
using Capa_Negocio;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace REAUTO_APP
{
    public partial class Registrar_Empleado : Form
    {
        CN_Empleados nempleados = new CN_Empleados();
        CE_Empleado eempleado = new CE_Empleado();
        RegistrarSolicitante FormSoli = new RegistrarSolicitante();

        private System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();


        bool sidebarExpand;
        bool homeCollapsed;
        public Registrar_Empleado()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de menú.
        // Inicia una animación para expandir o colapsar el sidebar de navegación.
        //Documentado por: Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start(); // Inicia el temporizador que maneja el proceso de expansión/colapso del sidebar.
        }

        // Evento que se ejecuta cuando el temporizador de reportes (ReporTimerRE) alcanza un tick.
        // Controla la expansión o contracción del panel de reportes en la interfaz.
        //Documentado por: Astrid Gonzales
        private void ReporTimerRE_Tick(object sender, EventArgs e)
        {
            if (homeCollapsed) // Si el panel está colapsado...
            {
                // Aumenta la altura del panel de reportes para expandirlo.
                Reportes.Height += 10;

                // Cuando la altura del panel alcanza su tamaño máximo, detiene la animación.
                if (Reportes.Height >= Reportes.MaximumSize.Height)
                {
                    homeCollapsed = false; // El panel ya está expandido.
                    ReporTimerRE.Stop(); // Detiene el temporizador de expansión.
                }
            }
            else // Si el panel está expandido...
            {
                // Reduce la altura del panel para contraerlo.
                Reportes.Height -= 10;

                // Cuando el panel alcanza su tamaño mínimo, detiene la animación.
                if (Reportes.Height <= Reportes.MinimumSize.Height)
                {
                    homeCollapsed = true; // El panel ya está colapsado.
                    ReporTimerRE.Stop(); // Detiene el temporizador de contracción.
                }
            }
        }

        // Evento que se ejecuta cada vez que el temporizador del sidebar alcanza un tick.
        // Controla la expansión y contracción del sidebar mediante un cambio gradual de su ancho.
        //Documentado por: Astrid Gonzales
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand) // Si el sidebar está expandiéndose...
            {
                // Reduce el ancho del sidebar para contraerlo.
                sidebar.Width -= 10;

                // Detiene el proceso cuando el sidebar alcanza su tamaño mínimo.
                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false; // El sidebar ya está colapsado.
                    sidebarTimer.Stop(); // Detiene el temporizador de colapso.
                }
            }
            else // Si el sidebar está colapsado...
            {
                // Aumenta el ancho del sidebar para expandirlo.
                sidebar.Width += 10;

                // Detiene el proceso cuando el sidebar alcanza su tamaño máximo.
                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = !sidebarExpand; // Alterna el estado de expansión.
                    sidebarTimer.Stop(); // Detiene el temporizador de expansión.
                }
            }
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reportes".
        // Inicia la animación de expansión/colapso del panel de reportes.
        //Documentado por: Astrid Gonzales
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReporTimerRE.Start(); // Inicia el temporizador para manejar la expansión/colapso del panel de reportes.
        }

        // Evento que se ejecuta cuando el usuario interactúa con el campo "ID" (al enfocarlo).
        // Limpia el valor predeterminado "ID" y cambia el color del texto para permitir al usuario ingresar su propio valor.
        //Documentado por: Astrid Gonzales
        private void lblIDEmpleado_Enter(object sender, EventArgs e)
        {
            if (txtID.Text == "ID") // Si el texto es el valor por defecto...
            {
                txtID.Text = ""; // Limpia el campo para que el usuario ingrese su valor.
                txtID.ForeColor = Color.Black; // Cambia el color del texto a negro.
            }
        }

        // Evento que se ejecuta cuando el usuario pierde el foco en el campo "ID".
        // Si el campo está vacío, lo vuelve a poner en su valor predeterminado "ID".
        //Documentado por: Astrid Gonzales
        private void txtID_Leave(object sender, EventArgs e)
        {
            if (txtID.Text == "") // Si el campo está vacío...
            {
                txtID.Text = "ID"; // Vuelve a poner el valor predeterminado.
                txtID.ForeColor = Color.Silver; // Cambia el color del texto a gris.
            }
        }

        // Evento que se ejecuta cuando el usuario interactúa con el campo "DNI" (al enfocarlo).
        // Limpia el valor predeterminado "DNI" y cambia el color del texto para permitir al usuario ingresar su propio valor.
        //Documentado por: Astrid Gonzales
        private void txtDNI_Enter(object sender, EventArgs e)
        {
            if (txtDNI.Text == "DNI") // Si el texto es el valor por defecto...
            {
                txtDNI.Text = ""; // Limpia el campo para que el usuario ingrese su valor.
                txtDNI.ForeColor = Color.Black; // Cambia el color del texto a negro.
            }
        }

        // Evento que se ejecuta cuando el usuario pierde el foco en el campo "DNI".
        // Si el campo está vacío, lo vuelve a poner en su valor predeterminado "DNI".
        //Documentado por: Astrid Gonzales
        private void txtDNI_Leave(object sender, EventArgs e)
        {
            if (txtDNI.Text == "") // Si el campo está vacío...
            {
                txtDNI.Text = "DNI"; // Vuelve a poner el valor predeterminado.
                txtDNI.ForeColor = Color.Silver; // Cambia el color del texto a gris.
            }
        }

        // Evento que se ejecuta cuando el usuario interactúa con el campo "Nombres" (al enfocarlo).
        // Limpia el valor predeterminado "Nombres" y cambia el color del texto para permitir al usuario ingresar su propio valor.
        //Documentado por: Astrid Gonzales
        private void txtNombres_Enter(object sender, EventArgs e)
        {
            if (txtNombres.Text == "Nombres") // Si el texto es el valor por defecto...
            {
                txtNombres.Text = ""; // Limpia el campo para que el usuario ingrese su valor.
                txtNombres.ForeColor = Color.Black; // Cambia el color del texto a negro.
            }
        }

        // Evento que se ejecuta cuando el usuario pierde el foco en el campo "Nombres".
        // Si el campo está vacío, lo vuelve a poner en su valor predeterminado "Nombres".
        //Documentado por: Astrid Gonzales
        private void txtNombres_Leave(object sender, EventArgs e)
        {
            if (txtNombres.Text == "") // Si el campo está vacío...
            {
                txtNombres.Text = "Nombres"; // Vuelve a poner el valor predeterminado.
                txtNombres.ForeColor = Color.Silver; // Cambia el color del texto a gris.
            }
        }

        // Evento que se ejecuta cuando el usuario interactúa con el campo "Apellidos" (al enfocarlo).
        // Limpia el valor predeterminado "Apellidos" y cambia el color del texto para permitir al usuario ingresar su propio valor.
        //Documentado por: Astrid Gonzales
        private void txtApellidos_Enter(object sender, EventArgs e)
        {
            if (txtApellidos.Text == "Apellidos") // Si el texto es el valor por defecto...
            {
                txtApellidos.Text = ""; // Limpia el campo para que el usuario ingrese su valor.
                txtApellidos.ForeColor = Color.Black; // Cambia el color del texto a negro.
            }
        }

        // Evento que se ejecuta cuando el usuario pierde el foco en el campo "Apellidos".
        // Si el campo está vacío, lo vuelve a poner en su valor predeterminado "Apellidos".
        //Documentado por: Astrid Gonzales
        private void txtApellidos_Leave(object sender, EventArgs e)
        {
            if (txtApellidos.Text == "") // Si el campo está vacío...
            {
                txtApellidos.Text = "Apellidos"; // Vuelve a poner el valor predeterminado.
                txtApellidos.ForeColor = Color.Silver; // Cambia el color del texto a gris.
            }
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Cerrar Sesión".
        // Muestra un mensaje de confirmación y, si el usuario confirma, cierra la sesión y redirige a la pantalla de login.
        //Documentado por: Astrid Gonzales
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("¿Seguro que quieres cerrar sesión?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes) // Si el usuario confirma que quiere cerrar sesión...
            {
                login loginform = new login(); // Crea una nueva instancia del formulario de login.
                loginform.Show(); // Muestra el formulario de login.
                this.Hide(); // Oculta el formulario actual.
            }
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Inicio" del Administrador.
        // Redirige al formulario de inicio de administrador y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin(); // Crea una nueva instancia del formulario de inicio de administrador.
            inico_Admin.Show(); // Muestra el formulario de inicio.
            this.Hide(); // Oculta el formulario actual.
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Crear Puesto".
        // Abre el formulario para crear un nuevo puesto y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto(); // Crea una nueva instancia del formulario de creación de puesto.
            crear_Puesto.Show(); // Muestra el formulario de creación de puesto.
            this.Hide(); // Oculta el formulario actual.
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Registrar Empleado".
        // Abre el formulario para registrar un nuevo empleado y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado(); // Crea una nueva instancia del formulario de registro de empleado.
            registrar_Empleado.Show(); // Muestra el formulario de registro.
            this.Hide(); // Oculta el formulario actual.
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Crear Usuario".
        // Abre el formulario para crear un nuevo usuario y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario(); // Crea una nueva instancia del formulario de creación de usuario.
            crear_Usuario.Show(); // Muestra el formulario de creación de usuario.
            this.Hide(); // Oculta el formulario actual.
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Solicitudes de Vacaciones".
        // Abre el formulario de solicitudes de vacaciones y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones(); // Crea una nueva instancia del formulario de solicitudes.
            solicitudes_Vacaciones.Show(); // Muestra el formulario de solicitudes de vacaciones.
            this.Hide(); // Oculta el formulario actual.
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reporte de Vacaciones".
        // Abre el formulario de reporte de vacaciones y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones(); // Crea una nueva instancia del formulario de reporte de vacaciones.
            reporte_Vacaciones.Show(); // Muestra el formulario de reporte de vacaciones.
            this.Hide(); // Oculta el formulario actual.
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reporte de Empleados".
        // Abre el formulario de reporte de empleados y cierra el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados(); // Crea una nueva instancia del formulario de reporte de empleados.
            reporte_Empleados.Show(); // Muestra el formulario de reporte de empleados.
            this.Hide(); // Oculta el formulario actual.
        }
        // Evento que se ejecuta al hacer clic en el botón "Crear Usuario".
        // Valida que todos los campos requeridos estén llenos antes de proceder con el registro del empleado.
        // Si los campos son correctos, se asignan valores a las propiedades del empleado y se guarda en la base de datos.
        // Si el registro es exitoso, se muestra un mensaje de confirmación y se recargan los empleados en la interfaz.
        // Si el registro falla, se muestra un mensaje de error.
        // Documentado por: Astrid Gonzales
        private void btncreateuser_Click(object sender, EventArgs e)
        {
            if (txtNombres.Text == "Nombres" || txtFechaContratacion.Value == txtFechaContratacion.MinDate || txtApellidos.Text == "Apellidos" || txtDNI.Text == "DNI" || txtID.Text == "ID")
            {
                MessageBox.Show("Rellene todos los datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                eempleado.ID_EMPLEADO = int.Parse(txtID.Text);
                eempleado.ID_DATOS = int.Parse(FormSoli.ObtenerSolicitante()[3]);
                eempleado.FECHA_CONTRATACION = txtFechaContratacion.Text;
            }

            if (nempleados.RegistrarEmpleado(eempleado) > 0)
            {
                MessageBox.Show("Dato Almacenado");
                CargarEmpleados();
                limpiar();
            }
            else
            {
                MessageBox.Show("Dato NO Almacenado");
            }
        }

        // Método que carga los empleados en el DataGridView.
        // Utiliza el método MostrarEmpleado de la capa de negocio para obtener la lista de empleados y mostrarlos en la interfaz.
        // Documentado por: Astrid Gonzales
        private void CargarEmpleados()
        {
            dtgvRegistrarEmpleado.DataSource = nempleados.MostrarEmpleado();
        }

        // Evento que se ejecuta al cargar el formulario de registrar empleados.
        // Inicializa la interfaz de usuario deshabilitando ciertos controles, como los campos de texto, botones y el DataGridView.
        // Se muestra el nombre de usuario en la etiqueta de administrador.
        // Documentado por: Astrid Gonzales
        private void Registrar_Empleado_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            lbladmin.Text = VariableGlobal.variableusuario;
            txtID.Enabled = false;
            txtDNI.Enabled = false;
            txtNombres.Enabled = false;
            txtApellidos.Enabled = false;
            txtFechaContratacion.Enabled = false;
            btnEditar.Enabled = false;
            btncreateuser.Enabled = false;
            dtgvRegistrarEmpleado.Enabled = false;

            txtFechaContratacion.Enabled = false;
        }

        // Evento que se ejecuta al hacer clic en el botón "Desbloquear".
        // Permite habilitar o deshabilitar los controles de los campos de entrada y el DataGridView.
        // También cambia el texto del botón entre "Desbloquear" y "Bloquear" según el estado de los controles.
        // Documentado por: Astrid Gonzales
        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (txtDNI.Enabled)
            {
                txtDNI.Enabled = false;
                txtNombres.Enabled = false;
                txtApellidos.Enabled = false;
                txtFechaContratacion.Enabled = false;
                btnEditar.Enabled = false;
                btncreateuser.Enabled = false;
                dtgvRegistrarEmpleado.Enabled = false;
                btnDesbloquear.Text = "Desbloquear";
            }
            else
            {
                txtDNI.Enabled = true;
                txtNombres.Enabled = true;
                txtApellidos.Enabled = true;
                txtFechaContratacion.Enabled = true;
                btnEditar.Enabled = true;
                btncreateuser.Enabled = true;
                dtgvRegistrarEmpleado.Enabled = true;
                btnDesbloquear.Text = "Bloquear";
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Editar".
        // Actualmente no implementado, pero se puede agregar la funcionalidad para editar los datos del empleado.
        // Documentado por: Astrid Gonzales
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNombres.Text == "Nombres" || txtFechaContratacion.Value == txtFechaContratacion.MinDate || txtApellidos.Text == "Apellidos" || txtDNI.Text == "DNI" || txtID.Text == "ID")
            {
                MessageBox.Show("Rellene todos los datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                eempleado.ID_EMPLEADO = int.Parse(txtID.Text);
                eempleado.FECHA_CONTRATACION = txtFechaContratacion.Text;
            }

            if (nempleados.EditarEmpleado(eempleado) > 0)
            {
                MessageBox.Show("Dato Almacenado");
                CargarEmpleados();
                limpiar();
            }
            else
            {
                MessageBox.Show("Dato NO Almacenado");
            }

        }

        // Evento que se ejecuta cuando se hace clic en una celda del DataGridView.
        // Rellena los campos de texto con los datos del empleado seleccionado en el DataGridView.
        // Divide el nombre completo en nombre y apellidos si es necesario.
        // Documentado por: Astrid Gonzales
        private void dtgvRegistrarEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dtgvRegistrarEmpleado.CurrentRow.Cells[0].Value.ToString();
            txtDNI.Text = dtgvRegistrarEmpleado.CurrentRow.Cells[1].Value.ToString();

            string nombreCompleto = dtgvRegistrarEmpleado.CurrentRow.Cells[2].Value.ToString();
            string[] partes = nombreCompleto.Split(' ');

            // Si hay al menos dos palabras, el primero es el nombre y el resto el apellido
            if (partes.Length > 1)
            {
                txtNombres.Text = partes[0]; // Primer nombre
                txtApellidos.Text = string.Join(" ", partes.Skip(1)); // El resto como apellidos
            }
            else
            {
                txtNombres.Text = nombreCompleto; // Si solo hay un nombre, lo dejamos completo
                txtApellidos.Text = ""; // No hay apellidos
            }
            txtFechaContratacion.Text = dtgvRegistrarEmpleado.CurrentRow.Cells[3].Value.ToString();
        }

        // Evento que se ejecuta al hacer clic en el botón "Buscar Solicitante".
        // Muestra un formulario de solicitud y luego carga los datos del solicitante seleccionado en los campos de texto.
        // Documentado por: Astrid Gonzales
        private void btnBuscarSolicitante_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormSoli.ShowDialog();

            string[] solicitante = FormSoli.ObtenerSolicitante(); // Suponiendo que devuelve un string[]

            if (solicitante != null && solicitante.Length >= 3) // Verificar si el arreglo tiene al menos 3 elementos
            {
                txtDNI.Text = solicitante[0]; // Primer elemento
                txtNombres.Text = solicitante[1]; // Segundo elemento
                txtApellidos.Text = solicitante[2]; // Tercer elemento
                txtID.Text = solicitante[3];
            }
            else
            {
                MessageBox.Show("No se ha encontrado un solicitante válido.");
            }

            this.Show();
            
        }

        // Método que limpia los campos de texto del formulario.
        // Restaura los valores predeterminados de los campos para que estén vacíos o en su estado inicial.
        // Documentado por: Astrid Gonzales
        private void limpiar()
        {
            txtNombres.Text = "";
            txtFechaContratacion.Text = "";
            txtApellidos.Text = "";
            txtDNI.Text = "";
            txtID.Text = "";
        }

        // Evento que se ejecuta al hacer clic en el botón "Reporte Auditoría".
        // Muestra el formulario de auditoría y oculta el formulario actual.
        // Documentado por: Astrid Gonzales
        private void btnReporteAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en el botón "Reporte Puestos".
        // Muestra el formulario de reportes de puestos y oculta el formulario actual.
        // Documentado por: Astrid Gonzales
        private void btnReportePuestos_Click(object sender, EventArgs e)
        {
            Reporte_Puestos reporte_Puestos = new Reporte_Puestos();
            reporte_Puestos.Show();
            this.Hide();
        }

        private void dtgvRegistrarEmpleado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento que se ejecuta al hacer clic en el botón "Backup".
        // Muestra el formulario de backup y oculta el formulario actual.
        // Documentado por: Astrid Gonzales
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/2nUOntGJtZ8";
            System.Diagnostics.Process.Start(url);
        }
    }
}
