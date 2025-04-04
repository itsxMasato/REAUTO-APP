using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        bool sidebarExpand;
        bool homeCollapsed;
        CN_Inicio ninicio = new CN_Inicio();
        public Inico_Admin()
        {
            InitializeComponent();
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if(sidebarExpand)
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
                if(sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = !sidebarExpand;
                    sidebarTimer.Stop();
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

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

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("¿Seguro que quieres cerrar sesión?","Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                login loginform = new login();
                loginform.Show();
                this.Hide();
            }
        }

        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_usuario = new Crear_Usuario();
            crear_usuario.Show();
            this.Hide();
        }

        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes = new Solicitudes_Vacaciones();
            solicitudes.Show();
            this.Hide();
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_empleados = new Reporte_Empleados();
            reporte_empleados.Show();
            this.Hide();
        }

        private void Inico_Admin_Load(object sender, EventArgs e)
        {
            CargarInicio();
        }

        private void CargarInicio()
        {
            dtgvInicio.DataSource = ninicio.MostrarInicio();
        }

        private void Inico_Admin_Load_1(object sender, EventArgs e)
        {
            lbladmin.Text = VariableGlobal.variableusuario;
            // TODO: esta línea de código carga datos en la tabla 'rEAUTODataSet.VW_Solicitudes_pendientes' Puede moverla o quitarla según sea necesario.
            //this.vW_Solicitudes_pendientesTableAdapter.Fill(this.rEAUTODataSet.VW_Solicitudes_pendientes);

        }
    }
}
