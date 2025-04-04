using Capa_Entidad;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using static REAUTO_APP.Crear_Usuario;

namespace REAUTO_APP
{
    public partial class Reporte_Puestos: Form
    {
        // Variable que indica si la barra lateral está expandida o contraída.
        bool sidebarExpand;
        // Variable que indica si la sección de reportes está colapsada o expandida.
        bool homeCollapsed;
        // Instancia de la capa de negocio para gestionar usuarios.
        CN_Usuarios nusuario = new CN_Usuarios();
        // Instancia de la capa de entidad para almacenar datos temporales del usuario.
        CE_Usuarios eusuarios = new CE_Usuarios();
        // Instancia de la capa de negocio para gestionar puestos.
        CN_Puestos n_Puestos = new CN_Puestos();

        private ToolTip toolTip1 = new ToolTip();

        public Reporte_Puestos()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }


        // Evento que se activa al hacer clic en el botón del menú lateral.
        // Inicia el temporizador para animar la expansión o contracción del menú.
        //Documentado por: Astrid Gonzales
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Evento que se activa al hacer clic en el botón de reportes.
        // Inicia el temporizador para animar la expansión o contracción de la sección de reportes.
        //Documentado por: Astrid Gonzales
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReporTimer.Start();
        }

        // Temporizador que controla la animación de expansión y contracción de la sección de reportes.
        //Documentado por: Astrid Gonzales
        private void ReporTimer_Tick(object sender, EventArgs e)
        {
            if (homeCollapsed)
            {
                Reportes.Height += 10;
                if (Reportes.Height >= Reportes.MaximumSize.Height)
                {
                    homeCollapsed = false;
                    ReporTimer.Stop();
                }
            }
            else
            {
                Reportes.Height -= 10;
                if (Reportes.Height <= Reportes.MinimumSize.Height)
                {
                    homeCollapsed = true;
                    ReporTimer.Stop();
                }
            }
        }

        // Temporizador que controla la animación de la barra lateral.
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

        // Evento que abre el reporte de vacaciones y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Evento que abre el reporte de empleados y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        // Evento que abre el reporte de empleados y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnReporteAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.Show();
            this.Hide();
        }

        //Documentado por: Astrid Gonzales
        // Evento que abre el formulario de copias de seguridad y oculta el formulario actual.
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }

        // Evento que muestra un cuadro de diálogo para confirmar el cierre de sesión.
        // Si el usuario acepta, se abre la pantalla de inicio de sesión y se oculta el formulario actual.
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

        // Evento que abre el formulario de contratos y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Evento que abre el formulario de solicitudes de vacaciones y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }

        //Documentado por: Astrid Gonzales
        // Evento que abre el formulario para crear un nuevo usuario y oculta el formulario actual.
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Evento que abre el formulario para registrar un nuevo empleado y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Evento que abre el formulario para crear un nuevo puesto y oculta el formulario actual.
        //Documentado por: Astrid Gonzales
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        //Documentado por: Astrid Gonzales
        // Evento que abre el formulario de inicio del administrador y oculta el formulario actual.
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }


        // Este método maneja la acción de imprimir un reporte en formato PDF.
        // Primero valida que el usuario tenga los permisos necesarios para la acción.
        // Si el usuario es un administrador, se registra la acción en el sistema de auditoría.
        // Luego verifica si hay datos disponibles en el DataGridView para exportar.
        // Si es así, se solicita al usuario una ubicación para guardar el archivo PDF.
        // En el proceso de creación del PDF, se genera un documento con información de la empresa y el reporte,
        // seguido por la tabla de datos extraídos del DataGridView.
        // Si ya existe un archivo con el mismo nombre, se pregunta al usuario si desea sobrescribirlo.
        // Finalmente, si la exportación es exitosa, se muestra un mensaje de confirmación. Si hay un error,
        // se muestra un mensaje de error detallado.
        //Documentado por: Astrid Gonzales
        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de PDF Reporte de Puestos";
                    nusuario.GenerarAuditoria(eusuarios);

                    if (dtgvReportePuestos.Rows.Count > 0)
                    {
                        SaveFileDialog pdf = new SaveFileDialog();
                        pdf.Filter = "PDF (*.pdf)|*.pdf";
                        pdf.FileName = "Reporte_Auditoria_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
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

                                    string codigoReporte = "RPTP001";

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

                                    Paragraph title = new Paragraph("Reporte de Puestos", titleFont)
                                    {
                                        Alignment = Element.ALIGN_CENTER
                                    };
                                    document.Add(title);
                                    document.Add(new Paragraph("\n"));

                                    PdfPTable table = new PdfPTable(dtgvReportePuestos.Columns.Count);
                                    table.WidthPercentage = 100;

                                    foreach (DataGridViewColumn col in dtgvReportePuestos.Columns)
                                    {
                                        PdfPCell headerCell = new PdfPCell(new Phrase(col.HeaderText, headerFont))
                                        {
                                            BackgroundColor = new BaseColor(200, 200, 255),
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        };
                                        table.AddCell(headerCell);
                                    }

                                    for (int i = 0; i < dtgvReportePuestos.Rows.Count; i++)
                                    {
                                        if (dtgvReportePuestos.Rows[i].Visible)
                                        {
                                            for (int j = 0; j < dtgvReportePuestos.Columns.Count; j++)
                                            {
                                                PdfPCell dataCell = new PdfPCell(new Phrase(dtgvReportePuestos.Rows[i].Cells[j].Value?.ToString(), dataFont))
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

        // Evento que permite exportar los datos del DataGridView a un archivo de Excel.
        //Documentado por: Astrid Gonzales
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
                    eusuarios.ACCION = "Impresion de Excel Reporte de Puestos";
                    nusuario.GenerarAuditoria(eusuarios);

                    if (dtgvReportePuestos.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application excelApp = new Excel.Application();
                            excelApp.Visible = true;
                            Excel.Workbook workbook = excelApp.Workbooks.Add();
                            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                            int totalColumns = dtgvReportePuestos.Columns.Count;
                            string lastColumnLetter = GetExcelColumnLetter(totalColumns);

                            // 🔹 **Generar el número de reporte**
                            string codigoReporte = "RPTP001";  // El número de reporte siempre será RPTA001

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
                            worksheet.Cells[7, 1] = "Reporte de Puestos";
                            Excel.Range titleRange = worksheet.Range["A7", lastColumnLetter + "7"];
                            titleRange.Merge();
                            titleRange.Font.Size = 12;
                            titleRange.Font.Bold = true;
                            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // ==== ENCABEZADOS DE COLUMNA ====
                            for (int i = 1; i <= totalColumns; i++)
                            {
                                worksheet.Cells[9, i] = dtgvReportePuestos.Columns[i - 1].HeaderText;
                                Excel.Range headerCell = worksheet.Cells[9, i];

                                headerCell.Interior.Color = Excel.XlRgbColor.rgbLightSteelBlue;
                                headerCell.Font.Bold = true;
                                headerCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                headerCell.Borders.Color = System.Drawing.Color.Black;

                                if (dtgvReportePuestos.Columns[i - 1].HeaderText.ToUpper().Contains("DNI"))
                                {
                                    worksheet.Columns[i].NumberFormat = "@";  // DNI como texto
                                }
                            }

                            // ==== DATOS DE LA TABLA ====
                            int rowIndex = 10;  // Comenzamos a partir de la fila 10
                            foreach (DataGridViewRow row in dtgvReportePuestos.Rows)
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

        // Método que obtiene la letra de la columna en Excel según su índice numérico.
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

        // Evento que se ejecuta al cargar el formulario y carga los datos iniciales.
        //Documentado por: Astrid Gonzales
        private void Reporte_Puestos_Load(object sender, EventArgs e)
        {
            CargarVista();
            CargarPuestos();
            cbxfiltroRPuestos.SelectedIndexChanged += cbxfiltroRPuestos_SelectedIndexChanged;

        }

        // Método que carga los datos en el DataGridView.
        //Documentado por: Astrid Gonzales
        public void CargarVista()
        { 
            dtgvReportePuestos.DataSource = n_Puestos.MostrarReportePuesto();
        }

        // Método que carga los puestos en el ComboBox. 
        //Documentado por: Astrid Gonzales
        private void CargarPuestos()
        {
            try
            {
                Dictionary<int, string> puestos = n_Puestos.ObtenerPuestos();
                cbxfiltroRPuestos.Items.Clear();
                List<Puesto> listaPuestos = new List<Puesto>();

                // Agregar la opción "Todos"
                listaPuestos.Add(new Puesto { ID_PUESTO = 0, NombrePuesto = "Todos" });

                // Agregar los puestos de la base de datos, asegurándonos de no agregar duplicados
                foreach (var puesto in puestos)
                {
                    if (!cbxfiltroRPuestos.Items.Contains(puesto.Value)) // Verificar si el puesto ya está
                    {
                        listaPuestos.Add(new Puesto { ID_PUESTO = puesto.Key, NombrePuesto = puesto.Value });
                    }
                }

                // Asignar los datos al ComboBox
                cbxfiltroRPuestos.DataSource = listaPuestos;
                cbxfiltroRPuestos.DisplayMember = "NombreEmpleado";
                cbxfiltroRPuestos.ValueMember = "";
                cbxfiltroRPuestos.SelectedIndex = 0; // Seleccionar "Todos" por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
        }

        // Evento que se ejecuta cuando se cambia la selección del ComboBox.
        //Documentado por: Astrid Gonzales
        private void cbxfiltroRPuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string puestoSeleccionado = cbxfiltroRPuestos.SelectedItem.ToString();

            if (puestoSeleccionado == "Todos")
            {
                FiltrarAccesosPorTodos();
            }
            else
            {
                FiltrarAccesosPorUsuario(puestoSeleccionado);
            }
        }

        // Método que muestra todas las filas en el DataGridView.
        //Documentado por: Astrid Gonzales
        private void FiltrarAccesosPorTodos()
        {
            foreach (DataGridViewRow row in dtgvReportePuestos.Rows)
            {
                row.Visible = true;
            }
        }

        // Método que filtra los accesos en el DataGridView según el puesto seleccionado.
        //Documentado por: Astrid Gonzales
        private void FiltrarAccesosPorUsuario(string puesto)
        {
            if (dtgvReportePuestos.IsCurrentCellInEditMode)
            {
                dtgvReportePuestos.EndEdit();
            }

            dtgvReportePuestos.ClearSelection();
            dtgvReportePuestos.CurrentCell = null;

            foreach (DataGridViewRow row in dtgvReportePuestos.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    string usuarioEnFila = row.Cells[1].Value.ToString().Trim();
                    row.Visible = puesto == "Todos" || usuarioEnFila.Equals(puesto, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    row.Visible = puesto == "Todos";
                }
            }
        }

        private void btnReportePuestos_Click(object sender, EventArgs e)
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
