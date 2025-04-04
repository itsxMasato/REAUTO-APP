using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REAUTO_APP
{
    public partial class login : Form
    {
        // Instanciación de las clases necesarias para manejar la lógica de negocio y la interfaz de usuario.
        // Documentado por: Astrid Gonzales
        CN_Usuarios usuarioNegocio = new CN_Usuarios();
        Inico_Admin inicioadmin = new Inico_Admin();
        Inicio_Empleado inicioempleado = new Inicio_Empleado();
        CE_Usuarios E_Usuarios = new CE_Usuarios();
        
        public login()
        {
            InitializeComponent();
            Inico_Admin inicio = new Inico_Admin();
            
        }


        // Evento que se ejecuta al hacer clic en el label para cerrar la aplicación
        // Simplemente cierra la aplicación al hacer clic en el label.
        // Documentado por: Astrid Gonzales
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Evento que se ejecuta al cambiar el estado del CheckBox de "Mostrar contraseña"
        // Si el CheckBox está marcado, se muestra la contraseña, de lo contrario, se oculta con el caracter '●'.
        // Documentado por: Astrid Gonzales
        private void cbxContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.PasswordChar = cbxContrasena.Checked ? '\0' : '●';
        }

        // Evento que se ejecuta al hacer clic en el botón "Ingresar"
        // Verifica si los campos de usuario y contraseña no están vacíos y valida las credenciales ingresadas.
        // Si son correctas, redirige al usuario según su rango de acceso (administrador o empleado).
        // Documentado por: Astrid Gonzales
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;
            VariableGlobal.variableusuario = usuario;

            E_Usuarios.USUARIO = VariableGlobal.variableusuario;
            E_Usuarios.ACCION = "Inicio de sesión en el sistema";
            usuarioNegocio.GenerarAuditoria(E_Usuarios);

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Debe llenar los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rango;
            string estado;
            int resultado = usuarioNegocio.LoguearUsuario(usuario, contrasena, out rango, out estado);

            if (resultado == 0)
            {
                string mensajeError = "El usuario y/o la contraseña son incorrectos o su usuario está inactivo. Por favor, verifique los datos.";

                MessageBox.Show(mensajeError, "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Permitir acceso
            MessageBox.Show("¡Bienvenido!", "Acceso Permitido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide(); // Oculta el formulario de login

            // Redirigir según el rango
            if (rango == 1) // Administrador
            {
                inicioadmin.Show();
            }
            else if (rango == 2) // Empleado
            {
                inicioempleado.Show();
            }
            else
            {
                MessageBox.Show("Acceso denegado, rango desconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        // Evento que se ejecuta al cargar el formulario de login
        // Verifica si existen usuarios registrados en la base de datos. Si no, muestra un mensaje y permite crear un usuario administrador.
        // Documentado por: Astrid Gonzales
        private void login_Load(object sender, EventArgs e)
        {
            if (!usuarioNegocio.HayUsuariosRegistrados())
            {
                MessageBox.Show("No hay usuarios en la base de datos. Debe crear un usuario administrador.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Registrarse formCrearUsuario = new Registrarse();
                formCrearUsuario.ShowDialog();
            }
        }

        // Evento que se ejecuta al hacer clic en el icono de cierre (pintura de la aplicación)
        // Cierra la aplicación cuando se hace clic en el icono.
        // Documentado por: Astrid Gonzales
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
