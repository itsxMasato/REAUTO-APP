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
using Capa_Datos;
using Capa_Negocio;

namespace REAUTO_APP
{
    public partial class Crear_Puesto : Form
    {
        private ToolTip toolTip1 = new ToolTip();

        // Documentado por: Miguel Flores
        // Variable que determina si la barra lateral está expandida o no
        bool sidebarExpand;
        // Documentado por: Miguel Flores
        // Variable que determina si la sección de inicio está colapsada o no
        bool homeCollapsed;


        // Documentado por: Miguel Flores
        // Instancia de la clase de negocio 'CN_Puestos' para manejar la lógica relacionada con los puestos
        CN_Puestos npuestos = new CN_Puestos();
        // Documentado por: Miguel Flores
        // Instancia de la clase entidad 'CE_Puestos' que representa los datos relacionados con los puestos
        CE_Puestos epuestos = new CE_Puestos();
        // Documentado por: Miguel Flores
        // Instancia de la clase entidad 'CE_Usuarios' que representa los datos relacionados con los usuarios
        CE_Usuarios E_Usuarios = new CE_Usuarios();
        // Documentado por: Miguel Flores
        // Instancia de la clase de negocio 'CN_Usuarios' para manejar la lógica relacionada con los usuarios
        CN_Usuarios n_Usuarios = new CN_Usuarios();

        public Crear_Puesto()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al cargar el formulario 'Crear_Puesto'. Llama al método para cargar los puestos en el DataGridView

        private void Crear_Puesto_Load(object sender, EventArgs e)
        {
            CargarPuestos();
        }

        // Documentado por: Miguel Flores
        // Método que carga los puestos en el DataGridView 'dtgvCrearPuesto' usando la función 'MostrarPuesto' de la clase de negocio 'npuestos'
        private void CargarPuestos()
        {
            dtgvCrearPuesto.DataSource = npuestos.MostrarPuesto();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en el botón 'btnReportes'. Inicia el temporizador para manejar la animación del panel de reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en el botón 'menuButton'. Inicia el temporizador para manejar la animación de la barra lateral
        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarCP.Start();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer 'Tick' en el temporizador 'ReportTimer'. Controla la animación del panel 'Reportes' para expandirlo o contraerlo.
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

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer 'Tick' en el temporizador 'sidebarCP'. Controla la animación del panel 'sidebar' para expandirlo o contraerlo.
        private void sidebarCP_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarCP.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = !sidebarExpand;
                    sidebarCP.Stop();
                }
            }
        }

        // Documentado por: Miguel Flores
        // Evento que maneja el enfoque (Enter) en el campo 'txtIDPuesto'. Borra el valor predeterminado "ID" y cambia el color del texto a negro.
        private void txtIDPuesto_Enter(object sender, EventArgs e)
        {
            if (txtIDPuesto.Text == "ID")
            {
                txtIDPuesto.Text = "";
                txtIDPuesto.ForeColor = Color.Black;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que maneja la pérdida de enfoque (Leave) en el campo 'txtIDPuesto'. Restaura el valor predeterminado "ID" y cambia el color del texto a gris.
        private void txtIDPuesto_Leave(object sender, EventArgs e)
        {
            if (txtIDPuesto.Text == "")
            {
                txtIDPuesto.Text = "ID";
                txtIDPuesto.ForeColor = Color.Silver;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que maneja el enfoque (Enter) en el campo 'txtNombrePuesto'. Borra el valor predeterminado "Nombre" y cambia el color del texto a negro.
        private void txtNombrePuesto_Enter(object sender, EventArgs e)
        {
            if (txtNombrePuesto.Text == "Nombre")
            {
                txtNombrePuesto.Text = "";
                txtNombrePuesto.ForeColor = Color.Black;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que maneja la pérdida de enfoque (Leave) en el campo 'txtNombrePuesto'. Restaura el valor predeterminado "Nombre" y cambia el color del texto a gris.
        private void txtNombrePuesto_Leave(object sender, EventArgs e)
        {
            if (txtNombrePuesto.Text == "")
            {
                txtNombrePuesto.Text = "Nombre";
                txtNombrePuesto.ForeColor = Color.Silver;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que muestra un mensaje de confirmación al intentar cerrar sesión y cierra la sesión si el usuario acepta.
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

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'button1'. Abre el formulario 'Inico_Admin' y oculta la ventana actual.
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnCrearPuesto'. Abre el formulario 'Crear_Puesto' y oculta la ventana actual.
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnRegistrarEmpleado'. Abre el formulario 'Registrar_Empleado' y oculta la ventana actual.
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnCrearUsuario'. Abre el formulario 'Crear_Usuario' y oculta la ventana actual.
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnSolicitudes'. Abre el formulario 'Solicitudes_Vacaciones' y oculta la ventana actual.
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }


        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnContrato'. Abre el formulario 'Contratos' y oculta la ventana actual.
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnReporteVacaciones'. Abre el formulario 'Reporte_Vacaciones' y oculta la ventana actual.
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btnReporteEmpleados'. Abre el formulario 'Reporte_Empleados' y oculta la ventana actual.
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_empleados = new Reporte_Empleados();
            reporte_empleados.Show();
            this.Hide();
        }


        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en 'btncreatepuesto'. 
        // Primero verifica si el usuario desea guardar los datos, luego valida los campos, 
        // verifica la existencia del ID y nombre de puesto, registra el nuevo puesto si es válido, 
        // y genera la auditoría correspondiente.
        private void btncreatepuesto_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("¿Seguro que desea guardar estos datos?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                if (txtIDPuesto.Text == "ID" || txtNombrePuesto.Text == "Nombre" || rtxtDescripcionPuesto.Text == "Descripción" || txtsueldo.Text == "Sueldo")
                {
                    MessageBox.Show("RELLENE TODOS LOS DATOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    int idPuesto;
                    if (!int.TryParse(txtIDPuesto.Text, out idPuesto))
                    {
                        MessageBox.Show("El ID debe ser un número válido.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string nombrePuesto = txtNombrePuesto.Text.Trim();
                    if (npuestos.ValidarNombrePuesto(nombrePuesto))
                    {
                        MessageBox.Show("El nombre del puesto ya existe. Ingrese uno diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Primero verifica si el ID ya existe antes de continuar
                    if (npuestos.ExisteIdPuesto(idPuesto))
                    {
                        MessageBox.Show("El ID del puesto ya existe. Ingrese un ID diferente.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    E_Usuarios.USUARIO = lbladmin.Text;
                    E_Usuarios.ACCION = "Creacion de registro en Puestos";
                    n_Usuarios.GenerarAuditoria(E_Usuarios);

                    epuestos.ID_PUESTO = int.Parse(txtIDPuesto.Text);
                    epuestos.NOMBRE_PUESTO = txtNombrePuesto.Text;
                    epuestos.DESCRIPCION_PUESTO = rtxtDescripcionPuesto.Text;
                    epuestos.SUELDO = int.Parse(txtsueldo.Text);
                    epuestos.ID_RANGO = 2;

                    if (npuestos.RegistrarPuesto(epuestos) > 0)
                    {
                        MessageBox.Show("Dato Almacenado");
                        CargarPuestos();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Dato NO Almacenado");

                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al cargar el formulario 'Crear_Puesto'. 
        // Establece valores iniciales, deshabilita campos y controles, 
        // y registra eventos de entrada para validaciones en el campo 'txtsueldo'.
        private void Crear_Puesto_Load_1(object sender, EventArgs e)
        {
            int cont = npuestos.IdPuesto();
            txtIDPuesto.Text = cont.ToString();
            lbladmin.Text = VariableGlobal.variableusuario;
            txtIDPuesto.Enabled = false;
            txtNombrePuesto.Enabled = false;
            rtxtDescripcionPuesto.Enabled = false;
            txtsueldo.Enabled = false;
            dtgvCrearPuesto.Enabled = false;
            btncreatepuesto.Enabled = false;
            btnEditar.Enabled = false;

            CargarPuestos();

            txtsueldo.KeyPress += new KeyPressEventHandler(txtsueldo_KeyPress);
            txtsueldo.TextChanged += new EventHandler(txtsueldo_TextChanged);

        }

        // Evento que se dispara cuando se hace foco en el campo 'rtxtDescripcionPuesto'. 
        // Limpia el texto por defecto y cambia el color del texto a negro.
        private void rtxtDescripcionPuesto_Enter(object sender, EventArgs e)
        {
            if(rtxtDescripcionPuesto.Text == "Descripción")
            {
                rtxtDescripcionPuesto.Text = "";
                rtxtDescripcionPuesto.ForeColor = Color.Black;
            }
        }


        // Evento que se dispara cuando se pierde el foco en el campo 'rtxtDescripcionPuesto'. 
        // Si el campo está vacío, establece el texto por defecto y cambia el color a gris
        private void rtxtDescripcionPuesto_Leave(object sender, EventArgs e)
        {
            if (rtxtDescripcionPuesto.Text == "")
            {
                rtxtDescripcionPuesto.Text = "Descripción";
                rtxtDescripcionPuesto.ForeColor = Color.Silver;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que se dispara al hacer clic en el botón 'btnDesbloquear'. 
        // Alterna el estado de habilitación de los campos de entrada, 
        // cambiando el texto del botón entre "Desbloquear" y "Bloquear".

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (btnDesbloquear.Text == "Bloquear")
            {
                txtNombrePuesto.Enabled = false;
                rtxtDescripcionPuesto.Enabled = false;
                txtsueldo.Enabled = false;
                dtgvCrearPuesto.Enabled = false;
                btncreatepuesto.Enabled = false;
                btnEditar.Enabled = false;
                btnDesbloquear.Text = "Desbloquear";
                Limpiar();
            }
            else
            {
                txtIDPuesto.Enabled = false;
                txtNombrePuesto.Enabled = true;
                rtxtDescripcionPuesto.Enabled = true;
                txtsueldo.Enabled = true;
                dtgvCrearPuesto.Enabled = true;
                btncreatepuesto.Enabled = true;
                btnEditar.Enabled = true;
                btnDesbloquear.Text = "Bloquear";

            }
        }

        private void txtIDPuesto_TextChanged(object sender, EventArgs e)
        {
            
        }

        // Documentado por: Miguel Flores
        // Método que limpia los campos de entrada y les devuelve sus valores predeterminados.
        private void Limpiar()
        {
            int cont = npuestos.IdPuesto();
            txtIDPuesto.Text = cont.ToString();
            txtNombrePuesto.Text = "Nombre";
            rtxtDescripcionPuesto.Text= "Descripción";
            txtsueldo.Text = "Sueldo";
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando se hace clic en el botón 'btnEditar'. 
        // Valida si todos los campos necesarios están llenos, registra la acción de modificación y actualiza los datos en la base de datos.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIDPuesto.Text == "ID" || txtNombrePuesto.Text == "Nombre" || txtsueldo.Text == "Sueldo" || rtxtDescripcionPuesto.Text == "Descripción")
            {
                MessageBox.Show("RELLENE TODOS LOS DATOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                E_Usuarios.USUARIO = lbladmin.Text;
                E_Usuarios.ACCION = "Modificacion de registro en Puestos";
                n_Usuarios.GenerarAuditoria(E_Usuarios);

                epuestos.ID_PUESTO = int.Parse(txtIDPuesto.Text);
                epuestos.NOMBRE_PUESTO = txtNombrePuesto.Text;
                epuestos.SUELDO = int.Parse(txtsueldo.Text);
                epuestos.DESCRIPCION_PUESTO = rtxtDescripcionPuesto.Text;

                if (npuestos.EditarPuesto(epuestos) > 0)
                {
                    MessageBox.Show("Datos actualizados correctamente");
                    CargarPuestos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar los datos");
                }
            
            }
        }

        private void dtgvCrearPuesto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario hace clic en el campo 'txtsueldo' (cuando el control gana foco).
        // Si el campo contiene el valor predeterminado, este se borra y cambia el color del texto a negro.
        private void txtsueldo_Enter(object sender, EventArgs e)
        {
            if (txtsueldo.Text == "Sueldo")
            {
                txtsueldo.Text = "";
                txtsueldo.ForeColor = Color.Black;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario sale del campo 'txtsueldo' (cuando el control pierde foco).
        // Si el campo está vacío, se restaura el texto predeterminado y el color del texto a color plateado.
        private void txtsueldo_Leave(object sender, EventArgs e)
        {
            if (txtsueldo.Text == "")
            {
                txtsueldo.Text = "Sueldo";
                txtsueldo.ForeColor = Color.Silver;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario hace clic en una celda del DataGridView (dtgvCrearPuesto).
        // Este evento permite cargar los datos del puesto seleccionado en los controles de texto.
        private void dtgvCrearPuesto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegúrate de que el clic fue en una fila válida (no en los encabezados)
            if (e.RowIndex >= 0)
            {
                // Obtener el ID_Puesto de la primera celda (ID del puesto)
                int idPuesto = Convert.ToInt32(dtgvCrearPuesto.CurrentRow.Cells[0].Value);

                // Cargar los datos básicos en los controles de texto
                txtIDPuesto.Text = idPuesto.ToString();
                txtNombrePuesto.Text = dtgvCrearPuesto.CurrentRow.Cells[1].Value.ToString();
                rtxtDescripcionPuesto.Text = dtgvCrearPuesto.CurrentRow.Cells[2].Value.ToString();

                // Llamar a la función ObtenerSueldoPorID para obtener el sueldo desde la base de datos
                int sueldo = npuestos.ObtenerSueldoPorID(idPuesto);

                // Asignar el sueldo al TextBox correspondiente
                txtsueldo.Text = sueldo.ToString();
            }
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario presiona una tecla en el campo 'txtsueldo'.
        // Este evento valida que solo se puedan ingresar números y teclas de control (como backspace).
        private void txtsueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y control (backspace, enter, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada si no es un número
            }
        }


        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el texto en el campo 'txtsueldo' cambia.
        // Este evento filtra la entrada para asegurarse de que solo se ingresen números.
        private void txtsueldo_TextChanged(object sender, EventArgs e)
        {
            // Verifica que no sea el valor por defecto "Sueldo"
            if (txtsueldo.Text != "Sueldo" && txtsueldo.Text != "")
            {
                // Filtrar solo números
                txtsueldo.Text = new string(txtsueldo.Text.Where(char.IsDigit).ToArray());

                // Evitar que se pierda el cursor
                txtsueldo.SelectionStart = txtsueldo.Text.Length;
            }
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario hace clic en el botón 'BtnAuditoria'.
        // Abre la ventana de auditoría y oculta la ventana actual.
        private void BtnAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario hace clic en el botón 'Btn_Backup'.
        // Abre la ventana de backup y oculta la ventana actual.
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Evento que se ejecuta cuando el usuario hace clic en el botón 'button2'.
        // Abre la ventana de reporte de puestos y oculta la ventana actual.
        private void button2_Click(object sender, EventArgs e)
        {
            Reporte_Puestos reporte_Puestos = new Reporte_Puestos();
            reporte_Puestos.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/fl80J5gqss4";
            System.Diagnostics.Process.Start(url);
        }
    }
    
}
