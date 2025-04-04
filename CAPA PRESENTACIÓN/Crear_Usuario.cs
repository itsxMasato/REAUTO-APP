using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Negocio;
using Capa_Datos;
using static REAUTO_APP.Crear_Usuario;

namespace REAUTO_APP
{
    public partial class Crear_Usuario : Form
    {
        private ToolTip toolTip1 = new ToolTip();

        // Variable que controla la expansión o contracción de la barra lateral.
        bool sidebarExpand;

        // Variable que controla la expansión o contracción del panel de reportes.
        bool homeCollapsed;

        // Instancia de la clase de entidad de usuario.
        CE_Usuarios eusuario = new CE_Usuarios();

        // Instancia de la clase de negocio de usuario.
        CN_Usuarios nusuario = new CN_Usuarios();

        public Crear_Usuario()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Método encargado de cargar la lista de usuarios en el DataGridView.
        // Obtiene los usuarios desde la base de datos y los asigna como fuente de datos.
        // Documentado por: Astrid Gonzales
        private void CargarUsuarios()
        {
            dtgvCrearUsuario.DataSource = nusuario.MostrarUsuario();
        }

        // Método que controla la animación de expansión y contracción del panel de reportes.
        // Modifica la altura del panel de forma progresiva hasta alcanzar los valores límite.
        // Detiene el temporizador una vez que se alcanza el tamaño deseado.
        // Documentado por: Astrid Gonzales
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

        // Método que controla la animación de expansión y contracción de la barra lateral.
        // Modifica el ancho de la barra de forma progresiva hasta alcanzar los valores límite.
        // Detiene el temporizador una vez que se alcanza el tamaño deseado.
        // Documentado por: Astrid Gonzales
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

        // Evento que se ejecuta cuando el usuario hace clic en el botón del menú lateral.
        // Inicia la animación de expansión o contracción de la barra lateral.
        // Documentado por: Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de reportes.
        // Inicia la animación de expansión o contracción del panel de reportes.
        // Documentado por: Astrid Gonzales
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        // Métodos encargados de limpiar los campos de entrada cuando reciben el foco.
        // Evita que el usuario tenga que borrar manualmente los textos de marcador de posición.
        // Documentado por: Astrid Gonzales
        private void txtIDCrearUsuario_Enter(object sender, EventArgs e)
        {
            if (txtIDCrearUsuario.Text == "ID")
            {
                txtIDCrearUsuario.Text = "";
                txtIDCrearUsuario.ForeColor = Color.Black;
            }
        }

        // Métodos encargados de prevenir la escritura en los ComboBox.
        // Restringe la entrada manual del usuario, asegurando que solo seleccione opciones existentes.
        // Documentado por: Astrid Gonzales
        private void txtIDCrearUsuario_Leave(object sender, EventArgs e)
        {
            if (txtIDCrearUsuario.Text == "")
            {
                txtIDCrearUsuario.Text = "ID";
                txtIDCrearUsuario.ForeColor = Color.Silver;
            }
        }


        // Evento que se ejecuta cuando el campo de "Usuario" recibe el foco.
        // Si el campo contiene el texto predeterminado "Usuario", se borra y el texto se cambia a color negro para indicar que el campo está listo para la entrada del usuario.
        // Documentado por: Astrid Gonzales
        private void txtDNICrearusuario_Enter(object sender, EventArgs e)
        {
            if (txtUserCrearusuario.Text == "Usuario")
            {
                txtUserCrearusuario.Text = "";
                txtUserCrearusuario.ForeColor = Color.Black;
            }
        }

        // Evento que se ejecuta cuando el campo de "Usuario" pierde el foco.
        // Si el campo está vacío, se restablece el valor predeterminado "Usuario" y se cambia el color a gris para indicar que es un texto predeterminado.
        // Documentado por: Astrid Gonzales
        private void txtDNICrearusuario_Leave(object sender, EventArgs e)
        {
            if (txtUserCrearusuario.Text == "")
            {
                txtUserCrearusuario.Text = "Usuario";
                txtUserCrearusuario.ForeColor = Color.Silver;
            }
        }

        // Métodos encargados de limpiar los campos de entrada cuando reciben el foco.
        // Evita que el usuario tenga que borrar manualmente los textos de marcador de posición.
        // Documentado por: Astrid Gonzales
        private void txtNombresCrearUsuario_Enter(object sender, EventArgs e)
        {
            if (txtContraCrearUsuario.Text == "Contraseña")
            {
                txtContraCrearUsuario.Text = "";
                txtContraCrearUsuario.ForeColor = Color.Black;
            }
        }

        // Evento que se ejecuta cuando el campo de "Contraseña" pierde el foco.
        // Si el campo está vacío, se restablece el valor predeterminado para evitar que el usuario deje el campo vacío sin notarlo.
        // Documentado por: Astrid Gonzales
        private void txtNombresCrearUsuario_Leave(object sender, EventArgs e)
        {
            if (txtContraCrearUsuario.Text == "")
            {
                txtContraCrearUsuario.Text = "Contraseña";
                txtContraCrearUsuario.ForeColor = Color.Silver;
            }
        }


        // Evento que se ejecuta cuando el campo de "Puesto" recibe el foco.
        // Si el texto predeterminado es "Puesto", se borra el texto y se cambia el color para indicar que el campo está listo para la entrada del usuario.
        // Documentado por: Astrid Gonzales
        private void cbxPuesto_Enter(object sender, EventArgs e)
        {
            if (cbxPuesto.Text == "Puesto")
            {
                cbxPuesto.Text = "";
                cbxPuesto.ForeColor = Color.Black;
            }
        }

        // Métodos encargados de prevenir la escritura en los ComboBox.
        // Restringe la entrada manual del usuario, asegurando que solo seleccione opciones existentes.
        // Documentado por: Astrid Gonzales
        private void cbxPuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Evita que el usuario escriba
        }

        // Evento que se ejecuta cuando el usuario intenta escribir en el ComboBox de empleados.
        // Restringe la entrada manual para garantizar que solo se seleccionen valores válidos del listado.
        // Documentado por: Astrid Gonzales
        private void cbxEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Evita que el usuario escriba
        }

        // Evento que se ejecuta cuando el usuario intenta escribir en el ComboBox de estado.
        // Se deshabilita la escritura para asegurar que solo se escojan opciones predefinidas.
        // Documentado por: Astrid Gonzales
        private void cbxEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Evita que el usuario escriba
        }

        // Evento que se activa cuando el ComboBox de rango pierde el foco.
        // Si el campo está vacío, se restablece su valor predeterminado para evitar que quede sin datos visibles.
        // Documentado por: Astrid Gonzales
        private void cbxRango_Leave(object sender, EventArgs e)
        {
            if (cbxPuesto.Text == "")
            {
                cbxPuesto.Text = "Puesto";
                cbxPuesto.ForeColor = Color.Silver;
            }
        }

        // Evento que se ejecuta cuando el usuario intenta escribir en el ComboBox de rango.
        // Se bloquea la escritura manual para evitar datos inválidos y mantener la consistencia en la selección.
        // Documentado por: Astrid Gonzales
        private void cbxRango_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Evita que el usuario escriba
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Cerrar Sesión".
        // Muestra una confirmación antes de cerrar la sesión para evitar cierres accidentales.
        // Si el usuario confirma, se abre el formulario de inicio de sesión y se oculta el formulario actual.
        // Documentado por: Astrid Gonzales
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

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Inicio".
        // Abre el formulario de inicio del administrador y cierra el formulario actual.
        // Documentado por: Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Crear Puesto".
        // Abre el formulario donde se pueden gestionar los puestos disponibles en la empresa.
        // Se oculta el formulario actual para mantener una mejor organización visual.
        // Documentado por: Astrid Gonzales
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Registrar Empleado".
        // Abre el formulario donde se pueden registrar nuevos empleados en el sistema.
        // Se oculta el formulario actual para evitar múltiples ventanas abiertas innecesariamente.
        // Documentado por: Astrid Gonzales
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Crear Usuario".
        // Abre el formulario donde se pueden crear y gestionar usuarios del sistema.
        // Se oculta el formulario actual para mantener una interfaz limpia y ordenada.
        // Documentado por: Astrid Gonzales
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Solicitudes".
        // Abre el formulario donde se pueden visualizar y gestionar las solicitudes de vacaciones de los empleados.
        // Se oculta el formulario actual para evitar la acumulación de ventanas en la interfaz.
        // Documentado por: Astrid Gonzales
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Contratos".
        // Abre el formulario donde se pueden gestionar los contratos de los empleados.
        // Se oculta el formulario actual para proporcionar una experiencia de usuario más fluida.
        // Documentado por: Astrid Gonzales
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Reporte de Vacaciones".
        // Abre el formulario donde se pueden visualizar los reportes relacionados con las vacaciones de los empleados.
        // Se oculta el formulario actual para mejorar la organización del sistema.
        // Documentado por: Astrid Gonzales
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Reporte de Empleados".
        // Abre el formulario donde se pueden visualizar los reportes detallados de los empleados de la empresa.
        // Se oculta el formulario actual para mantener la interfaz despejada y ordenada.
        // Documentado por: Astrid Gonzales
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        // Clase que representa un puesto dentro del sistema.
        // Se utiliza para almacenar el identificador y nombre de cada puesto.
        // El método ToString permite que el objeto se represente con su nombre.
        // Documentado por: Astrid Gonzales

        public class Puesto
        {
            public int ID_PUESTO { get; set; }
            public string NombrePuesto { get; set; }

            public override string ToString()
            {
                return NombrePuesto;
            }
        }

        // Clase que representa un empleado dentro del sistema.
        // Contiene el identificador y el nombre del empleado.
        // El método ToString facilita su visualización en controles de interfaz.
        // Documentado por: Astrid Gonzales
        public class Empleado
        {
            public int ID_EMPLEADO { get; set; }
            public string NombreEmpleado { get; set; }
            public override string ToString()
            {
                return NombreEmpleado;
            }
        }

        // Evento que se ejecuta cuando el usuario hace clic en "Crear Usuario".
        // Primero, confirma si el usuario desea continuar con el registro.
        // Luego, verifica que todos los campos obligatorios estén llenos.
        // Si el ID o el nombre de usuario ya existen, detiene el proceso y muestra una advertencia.
        // En caso contrario, registra la auditoría del evento y procede con la creación del usuario.
        // Si el registro es exitoso, actualiza la lista de usuarios y limpia los campos del formulario.
        // Documentado por: Astrid Gonzales
        private void btncreateuser_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("¿Seguro que desea guardar estos datos?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                if (txtIDCrearUsuario.Text == "ID" || txtUserCrearusuario.Text == "Usuario" || txtContraCrearUsuario.Text == "Contraseña" || cbxPuesto.Text == "Puesto" || cbxEmpleado.Text == "Empleado")
                {
                    MessageBox.Show("RELLENE TODOS LOS DATOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int idUsuario = int.Parse(txtIDCrearUsuario.Text);
                    string usuario = txtUserCrearusuario.Text.ToLower();

                    if (nusuario.ExisteIDUsuario(idUsuario, usuario))
                    {
                        MessageBox.Show("El ID de usuario o el nombre de usuario ya existen, por favor ingrese otros.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // No se detiene la ejecución, solo evita el registro
                    }

                    eusuario.USUARIO = lbladmin.Text;
                    eusuario.ACCION = "Creacion de registro en Usuarios";
                    nusuario.GenerarAuditoria(eusuario);

                    eusuario.ID_USUARIO = int.Parse(txtIDCrearUsuario.Text);
                    eusuario.CONTRASENA = txtContraCrearUsuario.Text.ToLower();
                    eusuario.USUARIO = txtUserCrearusuario.Text.ToLower();
                    eusuario.ID_PUESTO = (int)cbxPuesto.SelectedValue;
                    eusuario.ID_EMPLEADO = (int)cbxEmpleado.SelectedValue;
                    eusuario.ESTADO = cbxEstado.Text;

                    if (nusuario.RegistrarUsuario(eusuario) > 0)
                    {
                        MessageBox.Show("Dato Almacenado");
                        CargarUsuarios();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Dato NO Almacenado");
                    }
                }

            }

        }


        // Método encargado de cargar la lista de puestos en el ComboBox.
        // Obtiene los nombres de los puestos desde la base de datos.
        // Limpia el ComboBox antes de cargar nuevos datos para evitar duplicados.
        // Crea una lista de objetos Puesto y la asigna como fuente de datos.
        // En caso de error, muestra un mensaje de advertencia con la descripción del problema.
        // Documentado por: Astrid Gonzales
        private void CargarPuestos()
        {
            try
            {
                Dictionary<int, string> puestos = nusuario.ObtenerNombresPuestos();

                cbxPuesto.Items.Clear();
                List<Puesto> listaPuestos = new List<Puesto>();

                foreach (var puesto in puestos)
                {
                    listaPuestos.Add(new Puesto { ID_PUESTO = puesto.Key, NombrePuesto = puesto.Value });
                }

                cbxPuesto.DataSource = listaPuestos;
                cbxPuesto.DisplayMember = "NombrePuesto";
                cbxPuesto.ValueMember = "ID_PUESTO";
                cbxPuesto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar puestos: " + ex.Message);
            }
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al cargar el formulario 'Crear_Usuarios'. 
        // Establece valores iniciales, deshabilita campos y controles, 
        // y registra eventos de entrada para validaciones en el campo 'txtUserCrearusuario'.
        //Documentado por: Astrid Gonzales
        private void Crear_Usuario_Load(object sender, EventArgs e)
        {
            lbladmin.Text = VariableGlobal.variableusuario;
            txtIDCrearUsuario.Enabled = false;
            txtUserCrearusuario.Enabled = false;
            txtContraCrearUsuario.Enabled = false;
            cbxPuesto.Enabled = false;
            cbxEmpleado.Enabled = false;
            cbxEstado.Enabled = false;
            dtgvCrearUsuario.Enabled = false;
            btncreateuser.Enabled = false;   // Bloquea el botón de Crear
            btnEditar.Enabled = false;
            cbxEstado.Items.Add("ACTIVO");
            cbxEstado.Items.Add("INACTIVO");

            cbxPuesto.KeyPress += cbxPuesto_KeyPress;
            cbxEmpleado.KeyPress += cbxEmpleado_KeyPress;
            cbxEstado.KeyPress += cbxEstado_KeyPress;

            CargarUsuarios();
            CargarPuestos();
            CargarEmpleados();
            Limpiar();

        }

        // Método encargado de restablecer los valores de los campos del formulario a sus estados iniciales.
        // Permite limpiar los datos ingresados para facilitar la introducción de nueva información sin necesidad de borrar manualmente.
        // Se utilizan valores predeterminados para indicar al usuario qué tipo de dato debe ingresar en cada campo.
        // Documentado por: Astrid Gonzales
        private void Limpiar()
        {
            int cont = nusuario.IdUsuario();
            txtIDCrearUsuario.Text = cont.ToString();
            txtUserCrearusuario.Text = "Usuario";
            txtContraCrearUsuario.Text = "Contraseña";
            cbxPuesto.Text = "Puesto";
            cbxEmpleado.Text = "Empleado";
        }

        private void txtUserCrearusuario_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en el botón de desbloquear/activar los controles
        //Documentado por: Astrid Gonzales
        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (txtUserCrearusuario.Enabled)
            {
                // Bloquea todos los controles
                int cont = nusuario.IdUsuario();
                txtIDCrearUsuario.Text = cont.ToString();
                txtUserCrearusuario.Enabled = false;
                txtContraCrearUsuario.Enabled = false;
                cbxPuesto.Enabled = false;
                cbxEmpleado.Enabled = false;
                cbxEstado.Enabled = false;
                dtgvCrearUsuario.Enabled = false;  // Bloquea el DataGridView
                btncreateuser.Enabled = false;   // Bloquea el botón de Crear
                btnEditar.Enabled = false;
                btnDesbloquear.Text = "Desbloquear";
                Limpiar();
            }
            else
            {
                // Desbloquea todos los controles
                txtUserCrearusuario.Enabled = true;
                txtContraCrearUsuario.Enabled = true;
                cbxPuesto.Enabled = true;
                cbxEmpleado.Enabled = true;
                cbxEstado.Enabled = true;
                dtgvCrearUsuario.Enabled = true;  // Desbloquea el DataGridView
                btncreateuser.Enabled = true;   // Bloquea el botón de Crear
                btnEditar.Enabled = true;
                btnDesbloquear.Text = "Bloquear";
            }
        }

        // Evento que se ejecuta cuando el comboBox1 obtiene el foco
        // Cambia el texto del comboBox de "Empleado" a vacío y el color a negro cuando el control es enfocado
        //Documentado por: Astrid Gonzales
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (cbxEmpleado.Text == "Empleado")
            {
                cbxEmpleado.Text = "";
                cbxEmpleado.ForeColor = Color.Black;
            }
        }

        // Evento que se ejecuta cuando el comboBox1 pierde el foco
        // Restaura el texto y color por defecto si el campo está vacío al perder el foco
        //Documentado por: Astrid Gonzales
        private void cbxEmpleado_Leave(object sender, EventArgs e)
        {
            if (cbxEmpleado.Text == "")
            {
                cbxEmpleado.Text = "Empleado";
                cbxEmpleado.ForeColor = Color.Silver;
            }
        }


        // Evento que se ejecuta cuando el comboBox de Puesto pierde el foco
        // Restaura el texto y color por defecto si el campo está vacío
        //Documentado por: Astrid Gonzales
        private void cbxPuesto_Leave(object sender, EventArgs e)
        {
            if (cbxPuesto.Text == "")
            {
                cbxPuesto.Text = "Puesto";
                cbxPuesto.ForeColor = Color.Silver;
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de editar usuario
        // Valida los datos y luego actualiza el usuario en la base de datos
        //Documentado por: Astrid Gonzales
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIDCrearUsuario.Text == "" || txtUserCrearusuario.Text == "" || txtContraCrearUsuario.Text == "" || cbxPuesto.Text == "" || cbxEmpleado.Text == "" || cbxEstado.Text == "")
            {
                MessageBox.Show("RELLENE TODOS LOS DATOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idUsuario = int.Parse(txtIDCrearUsuario.Text);
                string usuario = txtUserCrearusuario.Text.ToLower();

                // Verificar si el nombre de usuario ya existe en la base de datos (sin considerar el ID del usuario actual)
                if (nusuario.ExisteUsuarioConIDDistinto(idUsuario, usuario))
                {
                    // Si el nombre de usuario ya está en uso, mostrar un mensaje y detener la ejecución
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor, elija otro.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Detener la ejecución si el nombre ya está en uso
                }
                else
                {

                    eusuario.USUARIO = lbladmin.Text;
                    eusuario.ACCION = "Modificacion de registro en Usuarios";
                    nusuario.GenerarAuditoria(eusuario);

                    // Si el usuario no existe, proceder con la edición normalmente
                    eusuario.ID_USUARIO = idUsuario;
                    eusuario.USUARIO = usuario;
                    eusuario.CONTRASENA = txtContraCrearUsuario.Text.ToLower();
                    eusuario.ID_PUESTO = (int)cbxPuesto.SelectedValue;
                    eusuario.ESTADO = cbxEstado.Text;
                }
                if (nusuario.EditarUsuario(eusuario) > 0)
                {
                    MessageBox.Show("Datos actualizados correctamente");
                    CargarUsuarios(); // Recarga los datos en el DataGridView
                    Limpiar();
                }
                else
                {
                    CargarUsuarios();
                    MessageBox.Show("No se pudo actualizar los datos");
                }
            }

        }

        private void dtgvCrearUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtContraCrearUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en una celda del DataGridView
        // Carga los datos de la celda seleccionada en los controles correspondientes
        //Documentado por: Astrid Gonzales
        private void dtgvCrearUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDCrearUsuario.Text = dtgvCrearUsuario.CurrentRow.Cells[0].Value.ToString();
            txtUserCrearusuario.Text = dtgvCrearUsuario.CurrentRow.Cells[1].Value.ToString();
            cbxEmpleado.Text = dtgvCrearUsuario.CurrentRow.Cells[2].Value.ToString();
            cbxPuesto.Text = dtgvCrearUsuario.CurrentRow.Cells[3].Value.ToString();
            cbxEstado.Text = dtgvCrearUsuario.CurrentRow.Cells[4].Value.ToString();
        }


        // Método encargado de cargar la lista de empleados en el ComboBox.
        // Obtiene los nombres de los empleados desde la base de datos mediante un diccionario.
        // Limpia el ComboBox antes de cargar nuevos datos para evitar datos redundantes.
        // Convierte la información en una lista de objetos Empleado y la asigna como fuente de datos.
        // Si ocurre un error durante el proceso, muestra un mensaje de advertencia con la descripción del problema.
        // Documentado por: Astrid Gonzales
        private void CargarEmpleados()
        {
            try
            {
                Dictionary<int, string> empleados = nusuario.ObtenerNombresEmpleados();
                cbxEmpleado.Items.Clear();
                List<Empleado> listaEmpleados = new List<Empleado>();

                foreach (var empleado in empleados)
                {
                    listaEmpleados.Add(new Empleado { ID_EMPLEADO = empleado.Key, NombreEmpleado = empleado.Value });
                }

                cbxEmpleado.DataSource = listaEmpleados;
                cbxEmpleado.DisplayMember = "NombreEmpleado";
                cbxEmpleado.ValueMember = "ID_EMPLEADO";
                cbxEmpleado.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
        }

        private void cbxEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reporte de Auditorías".
        // Permite acceder al formulario de auditorías para revisar los registros de cambios en el sistema.
        // Se oculta la ventana actual para evitar duplicados y mejorar la navegación.
        // Documentado por: Astrid Gonzales
        private void btnReporteAuditorias_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reporte de Puestos".
        // Abre el formulario donde se pueden visualizar los diferentes puestos registrados en el sistema.
        // Se oculta el formulario actual para mantener una interfaz limpia y organizada.
        // Documentado por: Astrid Gonzales
        private void btnReportePuestos_Click(object sender, EventArgs e)
        {
            Reporte_Puestos reporte_Puestos = new Reporte_Puestos();
            reporte_Puestos.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en el botón de "Backup".
        // Abre la ventana encargada de gestionar las copias de seguridad del sistema.
        // Se oculta la ventana actual para evitar superposiciones innecesarias.
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
            string url = "https://youtu.be/gnn4yMm6aMU";
            System.Diagnostics.Process.Start(url);
        }
    }
}