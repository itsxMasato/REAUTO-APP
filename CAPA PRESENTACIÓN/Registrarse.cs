using Capa_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REAUTO_APP
{
    public partial class Registrarse : Form
    {
        public Registrarse()
        {
            InitializeComponent();
        }

        // Evento que muestra u oculta la contraseña al marcar el CheckBox.
        //Documentado por: Astrid Gonzales
        private void cbxContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasenaRegistrar.PasswordChar = cbxContrasena.Checked ? '\0' : '●';
        }

        // Evento que abre el formulario de inicio de sesión y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void BtnIrInicioSesion_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }

        // Evento que cierra la aplicación al hacer clic en el Label.
        //Documentado por: Astrid Gonzales
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Evento que registra un nuevo usuario administrador y redirige a la pantalla de inicio de sesión.
        //Documentado por: Astrid Gonzales
        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            

            // Validar si los campos no están vacíos
            if (txtContrasenaRegistrar.Text == "" || txtUsuarioRegistrar.Text == "")
            {
                MessageBox.Show("Por favor, ingresa el nombre de usuario y la contraseña.");
                return;
            }

            string usuario = txtUsuarioRegistrar .Text;
            string contraseña = txtContrasenaRegistrar.Text;

            // Crear una instancia de la clase DatosUsuarios
            CD_Usuarios datosUsuarios = new CD_Usuarios();

            // Llamar al método CrearUsuarioAdministrador
            datosUsuarios.CrearUsuarioAdministrador(usuario, contraseña);

            // Mostrar mensaje de éxito
            MessageBox.Show("Usuario administrador registrado exitosamente.");

            login login = new login();
            login.Show();
            this.Hide();

        }

        private void Registrarse_Load(object sender, EventArgs e)
        {

        }
    }
}
