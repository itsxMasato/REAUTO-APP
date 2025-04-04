using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Capa_Negocio;
using Capa_Entidad;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace REAUTO_APP
{
    public partial class Solicitudes_Aprob : Form
    {
        // Instancias de clases necesarias para manejar solicitudes y usuarios.
        //Documentado por: Astrid Gonzales.
        CN_SolicitudVacaciones nevacaciones = new CN_SolicitudVacaciones();
        CN_Usuarios nusuarios = new CN_Usuarios();
        CE_Usuarios eusuarios = new CE_Usuarios();
        public Solicitudes_Aprob()
        {
            InitializeComponent();
        }

        // Función para validar la autorización de un usuario antes de imprimir un documento de aprobación de vacaciones.  
        // Se solicita una contraseña y, si el usuario es autorizado, se registra la acción en la auditoría del sistema.  
        // Luego, se carga un archivo PDF base, se completan sus campos con los datos del empleado y se genera un nuevo PDF editado.  
        // Si la modificación es exitosa, se abre automáticamente el PDF generado para su visualización.  
        // En caso de error en la validación del usuario o en la generación del documento, se muestra un mensaje de error.  
        //Documentado por: Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                // 3. Verificar si la validación es correcta
                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de Autorizacion de Vacaciones";
                    nusuarios.GenerarAuditoria(eusuarios);

                    string filename = Path.Combine(Application.StartupPath, "Resources", "Autorización Vacaciones.pdf");

                    // Verificar los campos del PDF antes de editarlos
                    MostrarCamposPDF(filename);

                    // Ruta donde se guardará el PDF modificado
                    string nuevoPDF = Path.Combine(Application.StartupPath, "Resources", "Autorización Vacaciones edit.pdf");

                    if (File.Exists(filename))
                    {
                        try
                        {
                            // Llenar los datos en el PDF
                            using (PdfReader reader = new PdfReader(filename))
                            {
                                using (FileStream fs = new FileStream(nuevoPDF, FileMode.Create, FileAccess.Write))
                                {
                                    using (PdfStamper stamper = new PdfStamper(reader, fs))
                                    {
                                        stamper.AcroFields.GenerateAppearances = true; // Asegura que aparezcan los campos llenados
                                        AcroFields formulario = stamper.AcroFields;

                                        // Llenar los campos con datos
                                        formulario.SetField("text_1jsqu", TxtEmpleado.Text);
                                        formulario.SetField("text_2auxh", TxtDNI.Text);
                                        formulario.SetField("text_3bpow", TxtFInicio.Text);
                                        formulario.SetField("text_4gvpj", TxtFFinal.Text);
                                        formulario.SetField("text_5xpom", TxtEmpleado.Text);
                                        formulario.SetField("text_6geuh", TxtDNI.Text);

                                        stamper.FormFlattening = true; // Convierte los campos en texto plano
                                    }
                                }
                            }

                            // Verificar que los datos se hayan guardado
                            VerificarDatosPDF(nuevoPDF);

                            // Abrir el PDF modificado
                            if (File.Exists(nuevoPDF))
                            {
                                // Abre el PDF sin mostrar un mensaje
                                Process.Start(new ProcessStartInfo(nuevoPDF) { UseShellExecute = true });
                            }
                        }
                        catch (Exception ex)
                        {
                            // Puedes registrar el error en el log o manejarlo de otra manera
                            Console.WriteLine("Error al modificar el PDF: " + ex.Message);
                        }
                    }
                    else
                    {
                        // Si el archivo no se encuentra, puedes registrar el error o realizar alguna acción alternativa
                        Console.WriteLine("El archivo no fue encontrado: " + filename);
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


        // Evento que se ejecuta cuando el formulario se carga.  
        // Llama a las funciones `CargarSolis()` y `CargarMeses()` para llenar el DataGridView con las solicitudes aprobadas  
        // y el ComboBox con los nombres de los meses disponibles. 
        //Documentado por: Astrid Gonxzales
        private void Solicitudes_Aprob_Load(object sender, EventArgs e)
        {
            CargarSolis();
            CargarMeses();
        }

        // Llena el ComboBox con los nombres de los meses del año para permitir la selección de solicitudes aprobadas  
        // según el mes en que fueron registradas. También agrega una opción especial "Todos los meses"  
        // para permitir la visualización de todas las solicitudes sin filtro.  
        //Documentado por: Astrid Gonxzales
        private void CargarMeses()
        {
            cbxMesesSolicitudes.Items.Clear();
            cbxMesesSolicitudes.Items.Add("Todos los meses"); // Opción sin filtro

            for (int i = 1; i <= 12; i++)
            {
                cbxMesesSolicitudes.Items.Add(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
            }

            cbxMesesSolicitudes.SelectedIndex = 0; // Por defecto mostrar todos
        }

        // Carga y muestra en un DataGridView todas las solicitudes aprobadas de vacaciones.  
        // Si el usuario ha seleccionado un mes específico, se filtran las solicitudes correspondientes.  
        // Además, convierte los nombres de los meses de inglés a español antes de mostrarlos en la interfaz. 
        //Documentado por: Astrid Gonxzales
        private void CargarSolis()
        {
            int mesSeleccionado = cbxMesesSolicitudes.SelectedIndex;

            // Si se seleccionó "Todos los meses" (mesSeleccionado == 0), no se debe aplicar filtro.
            if (mesSeleccionado == 0)
            {
                mesSeleccionado = -1;  // Usamos -1 para indicar que no hay filtro
            }

            DataTable dt = nevacaciones.MostrarAprobs(mesSeleccionado);

            // Convertir los nombres de los meses a español
            foreach (DataRow row in dt.Rows)
            {
                string mesIngles = row["Mes"].ToString();
                string mesEspañol = ConvertirMesAEspanol(mesIngles);
                row["Mes"] = mesEspañol;  // Reemplazamos el nombre del mes en la tabla
            }

            // Asignar el DataTable al DataGridView
            dtgvSolisAprob.DataSource = dt;
        }

        //// Convierte un nombre de mes en inglés a su equivalente en español.  
        // Se usa para mostrar los nombres de los meses en la interfaz de usuario en un formato más comprensible. 
        //Documentado por: Astrid Gonxzales
        private string ConvertirMesAEspanol(string mesIngles)
        {
            switch (mesIngles)
            {
                case "January": return "Enero";
                case "February": return "Febrero";
                case "March": return "Marzo";
                case "April": return "Abril";
                case "May": return "Mayo";
                case "June": return "Junio";
                case "July": return "Julio";
                case "August": return "Agosto";
                case "September": return "Septiembre";
                case "October": return "Octubre";
                case "November": return "Noviembre";
                case "December": return "Diciembre";
                default: return mesIngles;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento que se ejecuta cuando el usuario hace clic en el `pictureBox1`.  
        // Abre el formulario `Solicitudes_Vacaciones`, que permite al usuario gestionar nuevas solicitudes de vacaciones.  
        // Luego, cierra el formulario actual (`Solicitudes_Aprob`) para liberar memoria y evitar múltiples instancias abiertas.
        //Documentado por: Astrid Gonxzales
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones sv = new Solicitudes_Vacaciones();
            sv.Show();
            this.Close();
        }

        private void dtgvSolisAprob_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento que se ejecuta cuando el usuario hace clic en una celda del DataGridView.  
        // Toma los datos de la solicitud seleccionada y los muestra en los campos de texto del formulario. 
        //Documentado por: Astrid Gonxzales
        private void dtgvSolisAprob_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtDNI.Text = dtgvSolisAprob.CurrentRow.Cells[0].Value.ToString();
            TxtEmpleado.Text = dtgvSolisAprob.CurrentRow.Cells[1].Value.ToString();
            TxtFInicio.Text = Convert.ToDateTime(dtgvSolisAprob.CurrentRow.Cells[2].Value).ToString("yyyy-MM-dd");
            TxtFFinal.Text = Convert.ToDateTime(dtgvSolisAprob.CurrentRow.Cells[3].Value).ToString("yyyy-MM-dd");
        }

        // Muestra los nombres de los campos del formulario de un archivo PDF.  
        // Se usa para verificar qué nombres de campos están disponibles antes de completar el documento con datos. 
        //Documentado por: Astrid Gonxzales
        private void MostrarCamposPDF(string rutaPDF)
        {
            using (PdfReader reader = new PdfReader(rutaPDF))
            {
                var campos = reader.AcroFields.Fields;
                StringBuilder sb = new StringBuilder();

                foreach (var campo in campos)
                {
                    sb.AppendLine(campo.Key); // Muestra el nombre del campo
                }
            }
        }

        // Verifica si los datos del formulario fueron correctamente guardados en el PDF.  
        // Se extraen los valores de los campos del documento para asegurarse de que la información ingresada es la esperada. 
        //Documentado por: Astrid Gonxzales
        private void VerificarDatosPDF(string rutaPDF)
        {
            using (PdfReader reader = new PdfReader(rutaPDF))
            {
                AcroFields campos = reader.AcroFields;
                string empleado = campos.GetField("Empleado");
                string dni = campos.GetField("DNI");
                string fechaInicio = campos.GetField("fecha inicio");
                string fechaFinal = campos.GetField("fecha final");
            }
        }


        private void cbxMesesSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void dtgvSolisAprob_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento que se activa cuando el usuario cambia la selección del `ComboBox` que contiene los meses.  
        // Llama a `CargarSolis()` para actualizar el `DataGridView`, mostrando solo las solicitudes de vacaciones  
        // aprobadas correspondientes al mes seleccionado. Si se elige "Todos los meses", se muestran todas las solicitudes.
        //Documentado por: Astrid Gonxzales
        private void cbxMesesSolicitudes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CargarSolis();
            
        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {

        }
    }
}
