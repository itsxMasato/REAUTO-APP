using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;
using Capa_Entidad;
using System.Data.SqlClient;
using Capa_Datos;

namespace REAUTO_APP
{
    public partial class Contratos : Form
    {
        // Documentado por: Miguel Flores
        // Se crean instancias de las clases de lógica de negocio necesarias para trabajar con empleados y usuarios.
        // 'CN_Empleado' maneja las operaciones relacionadas con los empleados.
        // 'CN_Usuarios' maneja las operaciones relacionadas con los usuarios.
        // 'CE_Usuarios' es el objeto de entidad que contiene los datos de un usuario específico.
        CN_Empleado nempleados = new CN_Empleado();
        CN_Usuarios nusuarios = new CN_Usuarios();
        CE_Usuarios eusuarios = new CE_Usuarios();

        private ToolTip toolTip1 = new ToolTip();


        public Contratos()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Documentado por: Miguel Flores
        // Se llama al método 'MostrarEmpleadosEnGrid' cuando el formulario 'Contratos' se carga, con el fin de mostrar los empleados en el DataGridView.

        private void Contratos_Load(object sender, EventArgs e)
        {
            MostrarEmpleadosEnGrid();
        }


        // Documentado por: Miguel Flores
        // Se carga una lista de empleados y se muestra en un DataGridView, permitiendo filtrar por las columnas de DNI y Nombre.
        private void MostrarEmpleadosEnGrid()
        {
            CN_Empleado.CN_Empleados empleados = new CN_Empleado.CN_Empleados();
            DataTable dtEmpleados = empleados.MostrarEmpleado();

            if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
            {
                // Crear una nueva tabla con solo las columnas deseadas
                DataTable vistaEmpleados = new DataTable();
                vistaEmpleados.Columns.Clear();
                vistaEmpleados.Columns.Add("DNI");
                vistaEmpleados.Columns.Add("Nombre Empleado");
                // Llenar la nueva tabla con los datos de la consulta
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    vistaEmpleados.Rows.Add(row["DNI"].ToString(), row["Nombre Empleado"].ToString());
                }

                // Asignar la tabla filtrada al DataGridView
                dtgvContratos.DataSource = vistaEmpleados;
            }
            else
            {
                MessageBox.Show("No se encontraron empleados para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Documentado por: Miguel Flores
        // Se valida si el usuario tiene permisos para generar e imprimir un contrato PDF, luego procede a modificar el archivo con los datos proporcionados.

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                // 3. Verificar si la validación es correcta
                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de Contrato de Cliente";
                    nusuarios.GenerarAuditoria(eusuarios);

                    string filename = Path.Combine(Application.StartupPath, "Resources", "Contrato.pdf");

                    if (File.Exists(filename))
                    {
                        try
                        {
                            // Llenar los datos en el PDF
                            using (PdfReader reader = new PdfReader(filename))
                            {


                                string nuevoPDF = Path.Combine(Application.StartupPath, "Resources", "Contrato REAUTO 2025 edit.pdf");

                                using (FileStream fs = new FileStream(nuevoPDF, FileMode.Create, FileAccess.Write))
                                {
                                    using (PdfStamper stamper = new PdfStamper(reader, fs))
                                    {
                                        stamper.AcroFields.GenerateAppearances = true; // Asegura que aparezcan los campos llenados
                                        AcroFields formularioEdit = stamper.AcroFields;

                                        // Llenar los campos con datos (aquí los campos ya conocidos)
                                        formularioEdit.SetField("text_1jvdv", TxtEmpleado.Text);
                                        formularioEdit.SetField("text_4clzq", TxtDNI.Text);
                                        formularioEdit.SetField("text_5yjqf", TxtEmpleado.Text);
                                        formularioEdit.SetField("text_6getg", TxtDNI.Text);

                                        stamper.FormFlattening = true; // Convierte los campos en texto plano
                                    }
                                }

                                // Abrir el PDF modificado
                                Process.Start(new ProcessStartInfo(nuevoPDF) { UseShellExecute = true });
                            }
                        }
                        catch (Exception ex)
                        {
                            // Manejo del error
                            MessageBox.Show("Error al modificar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El archivo no fue encontrado:\n" + filename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dtgvContratos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Documentado por: Miguel Flores
        // Se agrega una nueva fila al DataGridView 'dtgvContratos' con los datos proporcionados en los TextBox y el valor global de usuario.

        private void btnEditar_Click(object sender, EventArgs e)
        {
            dtgvContratos.Rows.Add(TxtDNI.Text, TxtEmpleado.Text, VariableGlobal.variableusuario);
        }

        // Documentado por: Miguel Flores
        // Abre el formulario 'Inico_Admin' y oculta el formulario actual.
        private void x_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        private void dtgvContratos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Documentado por: Miguel Flores
        // Se carga el valor de la celda seleccionada en 'dtgvContratos' en los TextBox correspondientes.
        private void dtgvContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtDNI.Text = dtgvContratos.CurrentRow.Cells[0].Value.ToString();
            TxtEmpleado.Text = dtgvContratos.CurrentRow.Cells[1].Value.ToString();
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/Obl40iFUjvo";
            System.Diagnostics.Process.Start(url);
        }
    }
}
