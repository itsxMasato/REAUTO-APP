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
    public partial class Autorizacion : Form
    {
        // Instancia de la capa de negocio para manejar usuarios
        // Documentado por: Miguel Flores
        CN_Usuarios usuarioNegocio = new CN_Usuarios();
        // Instancia del formulario de inicio para administradores
        // Documentado por: Miguel Flores
        Inico_Admin inicioadmin = new Inico_Admin();
        // Instancia del formulario de inicio para empleados
        // Documentado por: Miguel Flores
        Inicio_Empleado inicioempleado = new Inicio_Empleado();
        public int result = 0;

        // Variable para almacenar el resultado de una operación
        // Documentado por: Miguel Flores
        public Autorizacion()

        {
            InitializeComponent();
        }

        private void Autorizacion_Load(object sender, EventArgs e)
        {

        }
        
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            
           
        }

        // Este método devuelve el valor de la variable 'result', que contiene el resultado de un proceso anterior.
        // Documentado por: Miguel Flores
        public int RetornarValor()
        {
            return result;
        }


        // Este método maneja el proceso de login. Verifica si los campos están completos,
        // valida el acceso según el rango del usuario, y muestra los mensajes apropiados.
        // Documentado por: Miguel Flores
        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;
            VariableGlobal.variableusuario = usuario;

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("Debe llenar los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rango;
            string estado;
            int resultado = usuarioNegocio.LoguearUsuario(usuario, contrasena, out rango, out estado);

            if (resultado > 0) // Usuario encontrado y activo
            {
                result = resultado; // Asignar el valor de resultado a result

                // Redirigir según el rango
                if (rango == 1) // Administrador
                {
                    MessageBox.Show("Acceso permitido", "Aprobado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Devolver DialogResult.OK para continuar
                    this.Hide(); // Ocultar el formulario de login
                }
                else if (rango == 2) // Empleado
                {
                    MessageBox.Show("Acceso denegado, rango insuficiente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Acceso denegado, rango desconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Su cuenta está inactiva. Contacte al administrador.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
