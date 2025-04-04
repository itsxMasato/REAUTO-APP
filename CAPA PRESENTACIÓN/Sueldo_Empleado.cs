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

namespace REAUTO_APP
{
    public partial class Sueldo_Empleado : Form
    {
        //Instacia de Capa Negocio comunicando Capa Presentacion con capa Datos.
        //Documentao por: Astrid Gonzales
        CN_Usuarios nusuarios = new CN_Usuarios();

        private ToolTip toolTip1 = new ToolTip();
        public Sueldo_Empleado()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Evento que confirma si el usuario realmente desea cerrar sesión.
        // Si la respuesta es afirmativa, se oculta la ventana actual y se muestra la ventana de inicio de sesión.
        //Documentao por: Astrid Gonzales
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

        // Evento que permite al usuario regresar a la pantalla principal de empleado.
        // Se oculta la ventana actual y se muestra la de inicio del empleado.
        //Documentao por: Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Inicio_Empleado inicio_Empleado = new Inicio_Empleado();
            inicio_Empleado.Show();
            this.Hide();
        }

        // Evento que permite al usuario solicitar vacaciones.
        // Se oculta la ventana actual y se muestra la de solicitud de vacaciones.
        //Documentao por: Astrid Gonzales
        private void btnSolicitarVacaciones_Click(object sender, EventArgs e)
        {
            Solicitar_Vacaciones_Empleado solicitar_Vacaciones_Empleado = new Solicitar_Vacaciones_Empleado();
            solicitar_Vacaciones_Empleado.Show();
            this.Hide();
        }

        // Al cargar la ventana, se obtiene el nombre del usuario y su sueldo.
        // La información se obtiene a través de una variable global que almacena el usuario activo en el sistema.
        //Documentao por: Astrid Gonzales
        private void Sueldo_Empleado_Load(object sender, EventArgs e)
        {
            lblempleado.Text = VariableGlobal.variableusuario;
            lblprecio.Text = nusuarios.ObtenerSueldoEmpleado(VariableGlobal.variableusuario) + ".00 LPS";
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/gh77vcIWhJE?si=LTGTjwQnoVfs_BYU";
            System.Diagnostics.Process.Start(url);
        }
    }
}
