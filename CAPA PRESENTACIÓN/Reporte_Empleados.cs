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
using static Capa_Negocio.CN_Empleado;
using Capa_Entidad;


namespace REAUTO_APP
{
    public partial class Reporte_Empleados : Form
    {
        // Variables para controlar la animación del sidebar y el menú de reportes
        bool sidebarExpand;
        bool homeCollapsed;
        // Instancias de clases para gestionar empleados y usuarios
        //Documentado por: Asrtid Gonzales
        CN_Inicio nempleados = new CN_Inicio();
        CN_Empleados objEmpleados = new CN_Empleados();
        CN_Usuarios nusuarios = new CN_Usuarios();
        CE_Usuarios eusuarios = new CE_Usuarios();

        private ToolTip toolTip1 = new ToolTip();

        public Reporte_Empleados()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Evento que maneja la expansión y colapso del menú de reportes mediante animaciones
        //Documentado por: Asrtid Gonzales
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

        // Expande o colapsa la barra lateral según su estado actual
        //Documentado por: Asrtid Gonzales
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

        // Inicia la animación de la barra lateral al hacer clic en el botón del menú
        //Documentado por: Asrtid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Inicia la animación del menú de reportes al hacer clic en el botón de reportes
        //Documentado por: Asrtid Gonzales
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        // Cierra sesión mostrando una confirmación antes de salir
        //Documentado por: Asrtid Gonzales
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

        // Recarga el formulario de Reporte de Empleados
        //Documentado por: Asrtid Gonzales
        private void button1_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        // Abre la ventana de creación de puestos y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Abre la ventana de registro de empleados y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Abre la ventana de creación de usuarios y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Abre la ventana de solicitudes de vacaciones y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }

        // Abre la ventana de contratos y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Abre la ventana de reportes de vacaciones y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Abre nuevamente el reporte de empleados y oculta la actual
        //Documentado por: Asrtid Gonzales
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        // Carga los empleados al abrir el formulario
        //Documentado por: Asrtid Gonzales
        private void ReporteEmpleados_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        // Obtiene y muestra la lista de empleados
        //Documentado por: Asrtid Gonzales
        private void CargarEmpleados()
        {
            nempleados.MostrarEmpleados();
        }

        // Carga los empleados y configura el filtro de reportes al abrir el formulario
        //Documentado por: Asrtid Gonzales
        private void Reporte_Empleados_Load(object sender, EventArgs e)
        {

            CargarEmpleados();
            filtroReporteEmpleados.Items.Add("Todos");
            filtroReporteEmpleados.Items.Add("Activos");
            filtroReporteEmpleados.Items.Add("Inactivos");
            filtroReporteEmpleados.SelectedIndex = 0;

        }

        // Contador de reportes generados para asignar un código único a cada exportación.
        //Documentado por: Asrtid Gonzales
        private int contadorReportes = 1;

        /// Evento que se ejecuta cuando el usuario hace clic en el botón de exportar a Excel.
        /// Se valida el acceso del usuario, se genera un informe de empleados en Excel con un formato estructurado,
        /// y se documenta la acción en la auditoría del sistema.
        /// Documentado por: Astrid Gonzales
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
                    eusuarios.ACCION = "Impresion de Excel Reporte de Empleados";
                    nusuarios.GenerarAuditoria(eusuarios);

                    if (dtgvReporteEmpleados.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application excelApp = new Excel.Application();
                            excelApp.Visible = true;
                            Excel.Workbook workbook = excelApp.Workbooks.Add();
                            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                            int totalColumns = dtgvReporteEmpleados.Columns.Count;
                            string lastColumnLetter = GetExcelColumnLetter(totalColumns);

                            // 🔹 **Generar el número de reporte**
                            string codigoReporte = "RPTE " + contadorReportes.ToString("D3");
                            contadorReportes++;  // Incrementar para la siguiente exportación

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
                            worksheet.Cells[7, 1] = "Reporte de Empleados";
                            Excel.Range titleRange = worksheet.Range["A7", lastColumnLetter + "7"];
                            titleRange.Merge();
                            titleRange.Font.Size = 12;
                            titleRange.Font.Bold = true;
                            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // ==== ENCABEZADOS DE COLUMNA ====
                            for (int i = 1; i <= totalColumns; i++)
                            {
                                worksheet.Cells[9, i] = dtgvReporteEmpleados.Columns[i - 1].HeaderText;
                                Excel.Range headerCell = worksheet.Cells[9, i];

                                headerCell.Interior.Color = Excel.XlRgbColor.rgbLightSteelBlue;
                                headerCell.Font.Bold = true;
                                headerCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                headerCell.Borders.Color = System.Drawing.Color.Black;

                                if (dtgvReporteEmpleados.Columns[i - 1].HeaderText.ToUpper().Contains("DNI"))
                                {
                                    worksheet.Columns[i].NumberFormat = "@";  // DNI como texto
                                }
                            }

                            // ==== DATOS DE LA TABLA ====
                            for (int i = 0; i < dtgvReporteEmpleados.Rows.Count; i++)
                            {
                                for (int j = 0; j < totalColumns; j++)
                                {
                                    var cellValue = dtgvReporteEmpleados.Rows[i].Cells[j].Value?.ToString();
                                    worksheet.Cells[i + 10, j + 1] = cellValue;

                                    Excel.Range dataCell = worksheet.Cells[i + 10, j + 1];
                                    dataCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                    dataCell.Borders.Color = System.Drawing.Color.LightGray;

                                    if (i % 2 == 0)
                                    {
                                        dataCell.Interior.Color = Excel.XlRgbColor.rgbWhiteSmoke;
                                    }
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

        // Convierte un número de columna en su equivalente en letras de Excel (A, B, ..., Z, AA, AB, ...).
        // Utiliza la lógica de conversión de base 26 con caracteres alfabéticos.
        // Ejemplo: 1 -> A, 2 -> B, 27 -> AA, etc.
        // Documentado por: Astrid Gonzales
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

        private void dtgvReporteEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento que maneja el botón "Imprimir", generando un reporte en PDF de los empleados.
        // Primero, solicita autenticación mediante un formulario de autorización.
        // Si el usuario es válido, se registra la acción en la auditoría.
        // Luego, se genera un archivo PDF con la información de los empleados y se guarda en la ubicación elegida.
        // Documentado por: Astrid Gonzales
        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                // 3. Verificar si la validación es correcta
                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de PDF Reporte de Empleados";
                    nusuarios.GenerarAuditoria(eusuarios);

                    if (dtgvReporteEmpleados.Rows.Count > 0)
                    {
                        SaveFileDialog pdf = new SaveFileDialog();
                        pdf.Filter = "PDF (*.pdf)|*.pdf";
                        pdf.FileName = "Reporte_Empleados_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
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

                                    string codigoReporte = "RPTE" + contadorReportes.ToString("D3");
                                    contadorReportes++;

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

                                    // Ajustar el espaciado entre líneas para mejorar la alineación
                                    Paragraph empresaInfo = new Paragraph();
                                    empresaInfo.SetLeading(0, 1.2f); // Espaciado entre líneas
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

                                    Paragraph title = new Paragraph("Reporte de Empleados", titleFont)
                                    {
                                        Alignment = Element.ALIGN_CENTER
                                    };
                                    document.Add(title);
                                    document.Add(new Paragraph("\n"));

                                    PdfPTable table = new PdfPTable(dtgvReporteEmpleados.Columns.Count);
                                    table.WidthPercentage = 100;

                                    foreach (DataGridViewColumn col in dtgvReporteEmpleados.Columns)
                                    {
                                        PdfPCell headerCell = new PdfPCell(new Phrase(col.HeaderText, headerFont))
                                        {
                                            BackgroundColor = new BaseColor(200, 200, 255),
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        };
                                        table.AddCell(headerCell);
                                    }

                                    for (int i = 0; i < dtgvReporteEmpleados.Rows.Count; i++)
                                    {
                                        for (int j = 0; j < dtgvReporteEmpleados.Columns.Count; j++)
                                        {
                                            PdfPCell dataCell = new PdfPCell(new Phrase(dtgvReporteEmpleados.Rows[i].Cells[j].Value?.ToString(), dataFont))
                                            {
                                                BackgroundColor = (i % 2 == 0) ? new BaseColor(245, 245, 245) : BaseColor.WHITE
                                            };
                                            table.AddCell(dataCell);
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

        // Se obtiene el estado seleccionado en el ComboBox.
        // Esto permite filtrar la lista de empleados según su estado.
        // Se verifica si el usuario desea ver todos los registros o solo aquellos con un estado específico.
        // En caso de "Todos", se recuperan todos los empleados sin aplicar un filtro adicional.
        // Si el usuario selecciona un estado específico, se filtran los empleados según ese criterio.
        //Documentado por: Astrid Gonzales
        private void filtroReporteEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estadoSeleccionado = filtroReporteEmpleados.SelectedItem.ToString();

            if (estadoSeleccionado == "Todos")
            {
                // Aquí llamas al método para cargar todos los empleados.
                dtgvReporteEmpleados.DataSource = objEmpleados.FiltrarEmpleados("Todos");
            }
            else
            {
                // Aquí filtras según el estado seleccionado.
                dtgvReporteEmpleados.DataSource = objEmpleados.FiltrarEmpleados(estadoSeleccionado);
            }
        }

        // Se abre la ventana de auditoría, permitiendo ver registros de acciones realizadas en el sistema.
        // Se oculta la ventana actual para evitar duplicaciones visuales en la interfaz.
        //Documentado por: Astrid Gonzales
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

        // Se abre la ventana de respaldo del sistema.
        // Esto permite realizar copias de seguridad de la base de datos para evitar pérdida de información.
        //Documentado por: Astrid Gonzales
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
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
