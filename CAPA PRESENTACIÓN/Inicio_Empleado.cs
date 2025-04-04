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
    public partial class Inicio_Empleado : Form
    {
        // Variables que controlan la expansión y contracción de la barra lateral y el estado de la pantalla principal.
        //Documentado por: Astrid Gonzales
        bool sidebarExpand;

        private ToolTip toolTip1 = new ToolTip();
        public Inicio_Empleado()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Evento que se ejecuta cuando se hace clic en el botón del menú lateral
        // Inicia el temporizador que controlará la animación de expansión o contracción de la barra lateral.
        //Documentado por: Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Evento que se ejecuta cada vez que el temporizador "SideBarTimer" hace un tick (intervalo de tiempo)
        // Controla la animación de expansión o contracción de la barra lateral.
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

        // Evento que se ejecuta cuando se hace clic en el botón de cerrar sesión
        // Muestra un cuadro de diálogo para confirmar si se desea cerrar sesión. Si se confirma, se abre el formulario de login.
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

        // Evento que se ejecuta cuando se hace clic en el botón de solicitar vacaciones
        // Abre el formulario de solicitud de vacaciones para el empleado y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnSolicitarVacaciones_Click(object sender, EventArgs e)
        {
            Solicitar_Vacaciones_Empleado solicitar_Vacaciones_Empleado = new Solicitar_Vacaciones_Empleado();
            solicitar_Vacaciones_Empleado.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando se hace clic en el botón de sueldo
        // Abre el formulario que muestra el sueldo del empleado y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnSueldo_Click(object sender, EventArgs e)
        {
            Sueldo_Empleado sueldo_Empleado = new Sueldo_Empleado();
            sueldo_Empleado.Show();
            this.Hide();
        }

        // Evento que se ejecuta al cargar el formulario de inicio de empleado
        // Muestra el nombre del usuario en el label correspondiente al inicio.
        //Documentado por: Astrid Gonzales
        private void Inicio_Empleado_Load(object sender, EventArgs e)
        {
            lblempleado.Text = VariableGlobal.variableusuario;
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
