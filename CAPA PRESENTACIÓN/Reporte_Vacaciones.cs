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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using Capa_Entidad;



namespace REAUTO_APP
{
    public partial class Reporte_Vacaciones : Form
    {
        private ToolTip toolTip1 = new ToolTip();

        bool sidebarExpand;// Indica si la barra lateral está expandida o no
        bool homeCollapsed;// Indica si la sección de reportes está colapsada o no
        CN_Inicio nvacaciones = new CN_Inicio();// Instancia para manejar las vacaciones
        CN_Usuarios nusuarios = new CN_Usuarios(); // Instancia para manejar los usuarios
        CE_Usuarios eusuarios = new CE_Usuarios();// Instancia de la entidad de usuarios

        public Reporte_Vacaciones()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Evento que gestiona la animación de expansión y contracción de la sección de reportes.
        //Documentado por: Astrid Gonzales
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

        // Evento que gestiona la animación de la barra lateral (expandir/colapsar).
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

        // Evento que se ejecuta cuando el usuario hace clic en el botón del menú.
        // Inicia la animación de la barra lateral.
        //Documentado por: Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reportes".
        // Inicia la animación de expansión/contracción de la sección de reportes.
        //Documentado por: Astrid Gonzales
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        // Evento que cierra la sesión del usuario y redirige a la pantalla de inicio de sesión.
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

        // Eventos que abren los diferentes formularios dentro del sistema.
        //Documentado por: Astrid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Crear Puesto".
        // Permite acceder al formulario donde se registran nuevos puestos dentro de la empresa.
        // Documentado por: Astrid Gonzales
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en "Registrar Empleado".
        // Abre la interfaz para registrar nuevos empleados en el sistema.
        // Documentado por: Astrid Gonzales
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en "Crear Usuario".
        // Muestra la ventana para registrar nuevos usuarios en la plataforma.
        // Documentado por: Astrid Gonzales
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en "Solicitudes de Vacaciones".
        // Abre el formulario donde se gestionan y revisan las solicitudes de vacaciones.
        // Documentado por: Astrid Gonzales
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en "Contratos".
        // Permite visualizar y administrar los contratos laborales dentro del sistema.
        // Documentado por: Astrid Gonzales
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en "Reporte de Vacaciones".
        // Muestra el formulario con el informe detallado de las vacaciones de los empleados.
        // Documentado por: Astrid Gonzales
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Evento que se ejecuta al hacer clic en "Reporte de Empleados".
        // Permite acceder al informe detallado de los empleados registrados en el sistema.
        // Documentado por: Astrid Gonzales
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        // Evento que se ejecuta al cargar el formulario de Reporte de Vacaciones.
        //Documentado por: Astrid Gonzales
        private void Reporte_Vacaciones_Load(object sender, EventArgs e)
        {
            
            label1.Text = VariableGlobal.variableusuario;;

            cbxFiltroVacaciones.Items.Add("Pendiente");
            cbxFiltroVacaciones.Items.Add("Solicitud Aprobada");
            cbxFiltroVacaciones.Items.Add("Solicitud Rechazada");
            cbxFiltroVacaciones.Items.Add("Todos");
            cbxFiltroVacaciones.SelectedIndex = 0;

            CargarVacaciones();
        }

        // Recarda los datos en el grid cada ves que se inicialice.
        //Documentado por: Astrid Gonzales
        private void ReporteVacaciones_Load(object sender, EventArgs e)
        {
            CargarVacaciones();
        }

        // Carga los datos de vacaciones en el DataGridView.
        //Documentado por: Astrid Gonzales
        private void CargarVacaciones()
        {
            nvacaciones.MostrarVacacciones();
            DataTable dt = nvacaciones.MostrarVacacciones();
            dtgvVacaciones.DataSource = dt;
        }

        // Convierte un número de columna en su equivalente en letra según el formato de Excel.
        //Documentado por: Astrid Gonzales
        private string GetExcelColumnLetter(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = "";
            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }
            return columnName;
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Imprimir Excel".
        // Se valida el reporte de Vacaciones para proceder con la generación del reporte en una Hoja de Excel.
        // Si el usuario está autorizado, se genera la Hoja de Excel con los datos del reporte.
        // Se manejan posibles errores en el proceso de exportación.
        // Documentado por: Astrid Gonzales
        private void btn_Excel_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                // 3. Verificar si la validación es correcta
                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de Excel Reporte de Vacaciones";
                    nusuarios.GenerarAuditoria(eusuarios);

                    if (dtgvVacaciones.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application excelApp = new Excel.Application();
                            excelApp.Visible = true;
                            Excel.Workbook workbook = excelApp.Workbooks.Add();
                            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                            int totalColumns = dtgvVacaciones.Columns.Count;
                            string lastColumnLetter = GetExcelColumnLetter(totalColumns);

                            // 🔹 **Generar el número de reporte**
                            string codigoReporte = "RPTV001";  // El número de reporte siempre será RPTA001

                            // ==== ENCABEZADO ====
                            worksheet.Cells[1, 1] = "Taller Industrial REAUTO";
                            Excel.Range empresaRange = worksheet.Range["A1", lastColumnLetter + "1"];
                            empresaRange.Merge();
                            empresaRange.Font.Size = 14;
                            empresaRange.Font.Bold = true;
                            empresaRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // Dirección
                            worksheet.Cells[2, 1] = "Lotificación Carranza,\ndos cuadras al sur y media al este de gasolinera Puma Roady,\ncostado sur de Senasa.";
                            Excel.Range direccionRange = worksheet.Range["A2", lastColumnLetter + "2"];
                            direccionRange.Merge();
                            direccionRange.Font.Size = 10;
                            direccionRange.WrapText = true;
                            direccionRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            direccionRange.RowHeight = 40;

                            // Teléfono
                            worksheet.Cells[4, 1] = "Teléfono: 3354-9886";
                            Excel.Range telefonoRange = worksheet.Range["A4", lastColumnLetter + "4"];
                            telefonoRange.Merge();
                            telefonoRange.Font.Size = 10;
                            telefonoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // 🔹 **Código de Reporte**
                            worksheet.Cells[5, 1] = codigoReporte;
                            Excel.Range codigoRange = worksheet.Range["A5", lastColumnLetter + "5"];
                            codigoRange.Merge();
                            codigoRange.Font.Size = 11;
                            codigoRange.Font.Bold = true;
                            codigoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // ==== TÍTULO DEL REPORTE ====
                            worksheet.Cells[7, 1] = "Reporte de Vacaciones";
                            Excel.Range titleRange = worksheet.Range["A7", lastColumnLetter + "7"];
                            titleRange.Merge();
                            titleRange.Font.Size = 12;
                            titleRange.Font.Bold = true;
                            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // ==== ENCABEZADOS DE COLUMNA ====
                            for (int i = 1; i <= totalColumns; i++)
                            {
                                worksheet.Cells[9, i] = dtgvVacaciones.Columns[i - 1].HeaderText;
                                Excel.Range headerCell = worksheet.Cells[9, i];

                                headerCell.Interior.Color = Excel.XlRgbColor.rgbLightSteelBlue;
                                headerCell.Font.Bold = true;
                                headerCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                headerCell.Borders.Color = System.Drawing.Color.Black;

                                if (dtgvVacaciones.Columns[i - 1].HeaderText.ToUpper().Contains("DNI"))
                                {
                                    worksheet.Columns[i].NumberFormat = "@";  // DNI como texto
                                }
                            }

                            // ==== DATOS DE LA TABLA ====
                            int rowIndex = 10;  // Comenzamos a partir de la fila 10
                            foreach (DataGridViewRow row in dtgvVacaciones.Rows)
                            {
                                if (row.Visible)  // Solo exportar las filas visibles (filtradas)
                                {
                                    for (int j = 0; j < totalColumns; j++)
                                    {
                                        var cellValue = row.Cells[j].Value?.ToString();
                                        worksheet.Cells[rowIndex, j + 1] = cellValue;

                                        Excel.Range dataCell = worksheet.Cells[rowIndex, j + 1];
                                        dataCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                        dataCell.Borders.Color = System.Drawing.Color.LightGray;

                                        if (rowIndex % 2 == 0)
                                        {
                                            dataCell.Interior.Color = Excel.XlRgbColor.rgbWhiteSmoke;
                                        }
                                    }
                                    rowIndex++;
                                }
                            }

                            // Autoajustar columnas
                            worksheet.Columns.AutoFit();

                            MessageBox.Show("Exportación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Imprimir".
        // Se valida la autorización del usuario para proceder con la generación del reporte en PDF.
        // Si el usuario está autorizado, se genera el archivo PDF con los datos del reporte.
        // Se manejan posibles errores en el proceso de exportación.
        // Documentado por: Astrid Gonzales
        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de PDF Reporte de Vacaciones";
                    nusuarios.GenerarAuditoria(eusuarios);

                    if (dtgvVacaciones.Rows.Count > 0)
                    {
                        SaveFileDialog pdf = new SaveFileDialog();
                        pdf.Filter = "PDF (*.pdf)|*.pdf";
                        pdf.FileName = "Reporte_Vacaciones_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                        bool errormensaje = false;

                        if (pdf.ShowDialog() == DialogResult.OK)
                        {
                            if (File.Exists(pdf.FileName))
                            {
                                DialogResult overwriteCheck = MessageBox.Show("El archivo ya existe. ¿Quieres sobrescribirlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (overwriteCheck == DialogResult.No) return;

                                try { File.Delete(pdf.FileName); }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al intentar reemplazar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    errormensaje = true;
                                }
                            }

                            if (!errormensaje)
                            {
                                try
                                {
                                    iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 10f);
                                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdf.FileName, FileMode.Create));
                                    document.Open();

                                    BaseFont helveticaBold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                    BaseFont helveticaRegular = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(helveticaBold, 18f);
                                    iTextSharp.text.Font headerFont = new iTextSharp.text.Font(helveticaBold, 10f);
                                    iTextSharp.text.Font dataFont = new iTextSharp.text.Font(helveticaRegular, 9f);

                                    string codigoReporte = "RPTV001";

                                    PdfPTable encabezado = new PdfPTable(3);
                                    encabezado.WidthPercentage = 100;
                                    encabezado.SetWidths(new float[] { 20f, 60f, 20f });

                                    string exePath = AppDomain.CurrentDomain.BaseDirectory;
                                    string imagePath = Path.Combine(exePath, "Resources", "logo.jpg");

                                    if (File.Exists(imagePath))
                                    {
                                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                                        img.ScaleToFit(80f, 80f);
                                        PdfPCell cellImagen = new PdfPCell(img)
                                        {
                                            Border = iTextSharp.text.Rectangle.NO_BORDER,
                                            HorizontalAlignment = Element.ALIGN_LEFT
                                        };
                                        encabezado.AddCell(cellImagen);
                                    }
                                    else
                                    {
                                        encabezado.AddCell("");
                                    }

                                    PdfPCell cellEmpresa = new PdfPCell();
                                    cellEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                    cellEmpresa.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cellEmpresa.HorizontalAlignment = Element.ALIGN_CENTER;

                                    Paragraph empresaInfo = new Paragraph();
                                    empresaInfo.SetLeading(0, 1.2f);
                                    empresaInfo.Alignment = Element.ALIGN_CENTER;
                                    empresaInfo.Add(new Chunk("Taller Industrial REAUTO\n", titleFont));
                                    empresaInfo.Add(new Chunk("Lotificación Carranza, dos cuadras al sur y media al este de gasolinera Puma Roady, costado sur de Senasa.\n", dataFont));
                                    empresaInfo.Add(new Chunk("Teléfono: 3354-9886", dataFont));

                                    cellEmpresa.AddElement(empresaInfo);
                                    encabezado.AddCell(cellEmpresa);

                                    PdfPCell cellCodigo = new PdfPCell(new Phrase(codigoReporte, titleFont))
                                    {
                                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                                        HorizontalAlignment = Element.ALIGN_RIGHT
                                    };
                                    encabezado.AddCell(cellCodigo);

                                    document.Add(encabezado);

                                    PdfPTable fechaTabla = new PdfPTable(1);
                                    fechaTabla.WidthPercentage = 100;
                                    PdfPCell cellFecha = new PdfPCell(new Phrase("Fecha de emisión: " + DateTime.Now.ToString("dd/MM/yyyy"), headerFont))
                                    {
                                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                                        HorizontalAlignment = Element.ALIGN_RIGHT
                                    };
                                    fechaTabla.AddCell(cellFecha);

                                    document.Add(fechaTabla);
                                    document.Add(new Paragraph("\n"));

                                    Paragraph title = new Paragraph("Reporte de Vacaciones", titleFont)
                                    {
                                        Alignment = Element.ALIGN_CENTER
                                    };
                                    document.Add(title);
                                    document.Add(new Paragraph("\n"));

                                    PdfPTable table = new PdfPTable(dtgvVacaciones.Columns.Count);
                                    table.WidthPercentage = 100;

                                    foreach (DataGridViewColumn col in dtgvVacaciones.Columns)
                                    {
                                        PdfPCell headerCell = new PdfPCell(new Phrase(col.HeaderText, headerFont))
                                        {
                                            BackgroundColor = new BaseColor(200, 200, 255),
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        };
                                        table.AddCell(headerCell);
                                    }

                                    for (int i = 0; i < dtgvVacaciones.Rows.Count; i++)
                                    {
                                        if (dtgvVacaciones.Rows[i].Visible)
                                        {
                                            for (int j = 0; j < dtgvVacaciones.Columns.Count; j++)
                                            {
                                                PdfPCell dataCell = new PdfPCell(new Phrase(dtgvVacaciones.Rows[i].Cells[j].Value?.ToString(), dataFont))
                                                {
                                                    BackgroundColor = (i % 2 == 0) ? new BaseColor(245, 245, 245) : BaseColor.WHITE
                                                };
                                                table.AddCell(dataCell);
                                            }
                                        }
                                    }

                                    document.Add(table);
                                    document.Close();

                                    MessageBox.Show("Exportación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        

        private void dtgvVacaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Función que filtra los datos de vacaciones por su estado (Pendiente, Aprobada, Rechazada o Todos).
        // Se aplica un filtro de fila al DataTable basado en el estado seleccionado.
        // Documentado por: Astrid Gonzales
        private void FiltrarDatosPorEstado(string estado)
        {
            if(dtgvVacaciones.DataSource is DataTable dt)
            {
                string filtro = "";

                if (estado == "Pendiente")
                {
                    filtro = "Estado = 'Pendiente'";  
                }
                else if (estado == "Solicitud Aprobada")
                {
                    filtro = "Estado = 'Solicitud Aprobada'";
                }
                else if (estado == "Solicitud Rechazada")
                {
                    filtro = "Estado = 'Solicitud Rechazada'";
                }
                else if (estado == "Todos")  
                {
                    filtro = ""; 
                }

                dt.DefaultView.RowFilter = filtro;
            }
        }

        // Evento que se ejecuta cuando el usuario selecciona un estado en el ComboBox de filtro de vacaciones.
        // Llama a la función FiltrarDatosPorEstado para actualizar la vista del DataGridView con los datos filtrados.
        // Documentado por: Astrid Gonzales
        private void cbxFiltroVacaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estadoSeleccionado = cbxFiltroVacaciones.SelectedItem.ToString();
            FiltrarDatosPorEstado(estadoSeleccionado);
        }


        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Backup".
        // Abre el formulario de Backup y oculta el formulario actual.
        // Documentado por: Astrid Gonzales

        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }


        // Evento que se ejecuta cuando el usuario hace clic en el botón de "Reporte de Auditoría".
        // Abre el formulario de Auditoría y oculta el formulario actual.
        // Documentado por: Astrid Gonzales

        private void btnReporteAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        private void btnReportePuestos_Click(object sender, EventArgs e)
        {
            Reporte_Puestos reporte_Puestos = new Reporte_Puestos();
            reporte_Puestos.Show();
            this.Hide();
        }

        private void dtgvVacaciones_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento que se ejecuta al hacer clic en el botón de "Help".  
        // Abre el enlace del video de ayuda en el navegador predeterminado.  
        // Documentado por: Miguel Flores 
        private void Help_Click(object sender, EventArgs e)
        {
            string url = "https://youtu.be/OpX3Ahlpx28";
            System.Diagnostics.Process.Start(url);
        }
    }
}
