using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace REAUTO_APP
{

    public partial class Backup : Form
    {
        private System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();

        // Documentado por: Miguel Flores
        // Se crean instancias de las clases de entidad y negocio para trabajar con los usuarios en el sistema.
        CE_Usuarios E_Usuarios = new CE_Usuarios();
        CN_Usuarios n_Usuarios = new CN_Usuarios();

        // Documentado por: Kenny Arias
        // Se crea una instancia de la capa negocio de conexión para llamar la funcion de adjuntar.
        CN_Conexion nconexion = new CN_Conexion();

        public Backup()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        private int IDUsuario;
        private string resultadoRol;
        private string UsuarioLB;

        // Documentado por: Miguel Flores
        // Método para asignar el rol del usuario
        // Se asigna el valor recibido al campo privado 'resultadoRol' para uso interno.
        public void SetRolUsuario(string roles)
        {
            this.resultadoRol = roles;
        }


        // Método para asignar el ID del usuario
        // Se asigna el valor recibido al campo privado 'IDUsuario', representando el identificador del usuario.
        // Documentado por: Miguel Flores
        public void SetIDUsuario(int IDUsuario)
        {
            this.IDUsuario = IDUsuario; // Asignamos el valor al campo privado
        }

        // Documentado por: Miguel Flores
        // Método para asignar el nombre del usuario en el label
        // Se asigna el valor recibido al campo privado 'UsuarioLB' para mostrar el nombre del usuario en el UI.
        public void SetUsuarioLabel(string nombreUsuario)
        {
            UsuarioLB = nombreUsuario;

        }

        // Método para realizar el backup de la base de datos
        // Se crea una carpeta en documentos si no existe y luego realiza el respaldo de la base de datos REAUTO,
        // almacenándolo en un archivo con un timestamp en su nombre para identificarlo fácilmente.
        // Documentado por: Miguel Flores
        private void Btn_CBackUp_Click(object sender, EventArgs e)
        {
            nconexion.Adjuntar();
            
            string backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REAUTO-RESPALDO");
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupFile = Path.Combine(backupFolder, $"REAUTO_{timestamp}.bak");

            string sql = @"Server=(localdb)\MSSQLLocalDB;Database=REAUTO;Integrated Security=True;";

            try
            {
                E_Usuarios.USUARIO = VariableGlobal.variableusuario;
                E_Usuarios.ACCION = "Creacion de Backup de la base de datos";
                n_Usuarios.GenerarAuditoria(E_Usuarios);

                using (SqlConnection conexion = new SqlConnection(sql))
                {
                    conexion.Open();
                    string query = $"BACKUP DATABASE REAUTO TO DISK = @ruta WITH FORMAT, INIT, NAME = 'Backup de REAUTO';";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ruta", backupFile);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Respaldo generado exitosamente en:\n{backupFile}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el respaldo:\n{ex.Message}");
            }
        }









        private void RestaurarBTN_Click(object sender, EventArgs e)
        {
            
        }

        private void Backup_Load(object sender, EventArgs e)
        {
            txtruta.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REAUTO-RESPALDO");
        }

        // Método para restaurar la base de datos a partir de un archivo de respaldo
        // 1. Se solicita la validación de un usuario autorizado mediante un formulario de autorización.
        // 2. Si la validación es exitosa, se abre un explorador de archivos para seleccionar el archivo de backup.
        // 3. La restauración de la base de datos se realiza en modo 'SINGLE_USER' para evitar accesos simultáneos.
        // 4. Se realiza la restauración del archivo seleccionado y se restablece el acceso multiusuario después de la restauración.
        // Documentado por: Miguel Flores
        private void Btn_Restaurar_Click(object sender, EventArgs e)
        {

            nconexion.Adjuntar();

            // 1. Crear una instancia del formulario de autorización
            Autorizacion frmvalidacion = new Autorizacion();

            // 2. Validar la autorización del usuario
            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                // 3. Verificar si la validación es correcta
                if (valor == 1) // Usuario autorizado (Administrador)
                {

                    

                    // 4. Abrir el explorador de archivos para seleccionar el archivo de backup
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Backup Files (*.bak)|*.bak";
                    openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REAUTO-RESPALDO");

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaRespaldo = openFileDialog.FileName;
                        string cadenaConexion = @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;";

                        // 5. Realizar la restauración de la base de datos
                        using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                        {
                            try
                            {
                                conexion.Open();

                                // 6. Poner la base de datos en modo SINGLE_USER para restaurar
                                SqlCommand cmd = new SqlCommand(@"
                        ALTER DATABASE REAUTO SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        RESTORE DATABASE REAUTO FROM DISK = @ruta WITH REPLACE;
                        ALTER DATABASE REAUTO SET MULTI_USER;", conexion);

                                // 7. Parámetro con la ruta del archivo de backup
                                cmd.Parameters.AddWithValue("@ruta", rutaRespaldo);
                                cmd.ExecuteNonQuery();

                                E_Usuarios.USUARIO = VariableGlobal.variableusuario;
                                E_Usuarios.ACCION = "Restauracion de Backup de la base de datos";
                                n_Usuarios.GenerarAuditoria(E_Usuarios);

                                MessageBox.Show("Base de datos restaurada exitosamente.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al restaurar la base de datos:\n{ex.Message}");
                            }
                            finally
                            {
                                if (conexion.State == ConnectionState.Open)
                                {
                                    conexion.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta o usuario no autorizado.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo validar el acceso al sistema.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para abrir la ventana de inicio de administración
        // 1. Crea una instancia del formulario de inicio de administrador (Inico_Admin).
        // 2. Muestra el formulario de inicio de administración.
        // 3. Oculta la ventana actual.
        // Documentado por: Miguel Flores
        private void x_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/MZehQVtwH8g";
            System.Diagnostics.Process.Start(url);
        }

        private void txtruta_TextChanged(object sender, EventArgs e)
        {

        }
    }
}