using Capa_Entidad;
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
using static REAUTO_APP.Crear_Usuario;

namespace REAUTO_APP
{
    public partial class Auditoria : Form
    {
        // Variables de control para la interfaz de usuario y objetos de acceso a datos
        // 1. `sidebarExpand`: Controla si la barra lateral está expandida o no.
        // 2. `homeCollapsed`: Controla si la página de inicio está colapsada o no.
        // 3. `nusuario`: Instancia de la clase de negocios `CN_Usuarios` para manejar la lógica relacionada con los usuarios.
        // 4. `eusuarios`: Instancia de la clase de entidad `CE_Usuarios` para representar los datos de usuario.
        // Documentado por: Miguel Flores
        bool sidebarExpand;
        bool homeCollapsed;
        CN_Usuarios nusuario = new CN_Usuarios();
        CE_Usuarios eusuarios = new CE_Usuarios();

        private ToolTip toolTip1 = new ToolTip();
        public Auditoria()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Help, "Ayuda");
        }

        // Documentado por: Miguel Flores
        // Método que carga la información de auditoría y los usuarios cuando se carga el formulario
        private void Auditoria_Load(object sender, EventArgs e)
        {
            LblUsuario.Text = VariableGlobal.variableusuario.ToString();

            CargarAuditoria();
            CargarUsuarios();
            cbxfiltroAuditoria.SelectedIndexChanged += cbxfiltroAuditoria_SelectedIndexChanged;
        }

        // Método que carga los datos de auditoría en el DataGridView
        // Documentado por: Miguel Flores
        private void CargarAuditoria()
        {
            dtgvAuditoria.DataSource = nusuario.MostrarAuditoria();
        }

        // Método que se ejecuta cuando el botón de impresión es presionado
        // Documentado por: Miguel Flores
        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            Autorizacion frmvalidacion = new Autorizacion();

            if (frmvalidacion.ShowDialog() == DialogResult.OK)
            {
                int valor = frmvalidacion.RetornarValor();

                if (valor == 1) // Usuario autorizado (Administrador)
                {
                    eusuarios.USUARIO = VariableGlobal.variableusuario;
                    eusuarios.ACCION = "Impresion de PDF Reporte de Auditoria";
                    nusuario.GenerarAuditoria(eusuarios);

                    if (dtgvAuditoria.Rows.Count > 0)
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

                                    string codigoReporte = "RPTA001";

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

                                    Paragraph title = new Paragraph("Reporte de Auditoria", titleFont)
                                    {
                                        Alignment = Element.ALIGN_CENTER
                                    };
                                    document.Add(title);
                                    document.Add(new Paragraph("\n"));

                                    PdfPTable table = new PdfPTable(dtgvAuditoria.Columns.Count);
                                    table.WidthPercentage = 100;

                                    foreach (DataGridViewColumn col in dtgvAuditoria.Columns)
                                    {
                                        PdfPCell headerCell = new PdfPCell(new Phrase(col.HeaderText, headerFont))
                                        {
                                            BackgroundColor = new BaseColor(200, 200, 255),
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        };
                                        table.AddCell(headerCell);
                                    }

                                    for (int i = 0; i < dtgvAuditoria.Rows.Count; i++)
                                    {
                                        if (dtgvAuditoria.Rows[i].Visible)
                                        {
                                            for (int j = 0; j < dtgvAuditoria.Columns.Count; j++)
                                            {
                                                PdfPCell dataCell = new PdfPCell(new Phrase(dtgvAuditoria.Rows[i].Cells[j].Value?.ToString(), dataFont))
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

        // Documentado por: Miguel Flores
        // Este método maneja la exportación de datos de auditoría a un archivo Excel.
        // Verifica si el usuario está autorizado antes de continuar con la exportación.
        // Si el usuario está autorizado, se registra la acción de impresión en el sistema de auditoría.
        // Luego, genera el archivo Excel con los datos de la tabla de auditoría, ajustando el formato según sea necesario,
        // y asegura que las columnas estén autoajustadas para facilitar la visualización.
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
                    eusuarios.ACCION = "Impresion de Excel Reporte de Auditoria";
                    nusuario.GenerarAuditoria(eusuarios);

                    if (dtgvAuditoria.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application excelApp = new Excel.Application();
                            excelApp.Visible = true;
                            Excel.Workbook workbook = excelApp.Workbooks.Add();
                            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                            int totalColumns = dtgvAuditoria.Columns.Count;
                            string lastColumnLetter = GetExcelColumnLetter(totalColumns);

                            // 🔹 **Generar el número de reporte**
                            string codigoReporte = "RPTA001";  // El número de reporte siempre será RPTA001

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
                            worksheet.Cells[7, 1] = "Reporte de Auditoria";
                            Excel.Range titleRange = worksheet.Range["A7", lastColumnLetter + "7"];
                            titleRange.Merge();
                            titleRange.Font.Size = 12;
                            titleRange.Font.Bold = true;
                            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // ==== ENCABEZADOS DE COLUMNA ====
                            for (int i = 1; i <= totalColumns; i++)
                            {
                                worksheet.Cells[9, i] = dtgvAuditoria.Columns[i - 1].HeaderText;
                                Excel.Range headerCell = worksheet.Cells[9, i];

                                headerCell.Interior.Color = Excel.XlRgbColor.rgbLightSteelBlue;
                                headerCell.Font.Bold = true;
                                headerCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                headerCell.Borders.Color = System.Drawing.Color.Black;

                                if (dtgvAuditoria.Columns[i - 1].HeaderText.ToUpper().Contains("DNI"))
                                {
                                    worksheet.Columns[i].NumberFormat = "@";  // DNI como texto
                                }
                            }

                            // ==== DATOS DE LA TABLA ====
                            int rowIndex = 10;  // Comenzamos a partir de la fila 10
                            foreach (DataGridViewRow row in dtgvAuditoria.Rows)
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

        // Documentado por: Miguel Flores
        // Este método convierte un número de columna en su respectiva letra de columna en Excel.
        // Se utiliza para traducir el número de columna a una referencia de columna al estilo de Excel (por ejemplo, 1 = A, 27 = AA).
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

        // Documentado por: Miguel Flores
        // Este método maneja el evento de clic en un botón, abriendo una nueva ventana de administración (Inico_Admin)
        // y ocultando la ventana actual. Es útil para navegar entre formularios de la aplicación.
        private void x_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método maneja el evento de cambio de selección en un ComboBox. Dependiendo de la opción seleccionada,
        // se filtra la información de auditoría ya sea por todos los usuarios o por un usuario específico.
        private void cbxfiltroAuditoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string usuarioSeleccionado = cbxfiltroAuditoria.SelectedItem.ToString();

            if (usuarioSeleccionado == "Todos")
            {
                FiltrarAccesosPorTodos();
            }
            else
            {
                FiltrarAccesosPorUsuario(usuarioSeleccionado);
            }
        }

        // Documentado por: Miguel Flores
        // Este método controla la animación de expansión y contracción de la sección de Reportes,
        // aumentando o disminuyendo su altura de manera gradual. Detiene el temporizador cuando se alcanza el tamaño máximo o mínimo.
        private void ReportTimer_Tick_1(object sender, EventArgs e)
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

        // Documentado por: Miguel Flores
        // Este método maneja la animación de expansión y contracción de la barra lateral, ajustando su ancho
        // hasta alcanzar el tamaño máximo o mínimo, deteniendo el temporizador una vez completada la animación.
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

        // Documentado por: Miguel Flores
        // Este método inicia la animación para expandir o contraer la barra lateral
        private void menuButton_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        // Documentado por: Miguel Flores
        // Este método inicia la animación para expandir o contraer la sección de Reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportTimer.Start();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana del administrador y oculta la ventana actual
        private void button1_Click(object sender, EventArgs e)
        {
            Inico_Admin inico_Admin = new Inico_Admin();
            inico_Admin.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para crear un nuevo puesto y oculta la ventana actual
        private void btnCrearPuesto_Click(object sender, EventArgs e)
        {
            Crear_Puesto crear_Puesto = new Crear_Puesto();
            crear_Puesto.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para registrar un nuevo empleado y oculta la ventana actual
        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Registrar_Empleado registrar_Empleado = new Registrar_Empleado();
            registrar_Empleado.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para crear un nuevo usuario y oculta la ventana actual
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Crear_Usuario crear_Usuario = new Crear_Usuario();
            crear_Usuario.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para gestionar solicitudes de vacaciones y oculta la ventana actual
        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            Solicitudes_Vacaciones solicitudes_Vacaciones = new Solicitudes_Vacaciones();
            solicitudes_Vacaciones.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para gestionar contratos y oculta la ventana actual
        private void btnContrato_Click(object sender, EventArgs e)
        {
            Contratos contratos = new Contratos();
            contratos.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para realizar un respaldo y oculta la ventana actual
        private void Btn_Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para generar el reporte de vacaciones y oculta la ventana actual
        private void btnReporteVacaciones_Click(object sender, EventArgs e)
        {
            Reporte_Vacaciones reporte_Vacaciones = new Reporte_Vacaciones();
            reporte_Vacaciones.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para generar el reporte de empleados y oculta la ventana actual
        private void btnReporteEmpleados_Click(object sender, EventArgs e)
        {
            Reporte_Empleados reporte_Empleados = new Reporte_Empleados();
            reporte_Empleados.Show();
            this.Hide();
        }

        private void btnReporteAuditoria_Click(object sender, EventArgs e)
        {
            
        }

        // Documentado por: Miguel Flores
        // Este método abre la ventana para generar el reporte de puestos y oculta la ventana actual

        private void btnReportePuestos_Click(object sender, EventArgs e)
        {
            Reporte_Puestos reporte_Puestos = new Reporte_Puestos();
            reporte_Puestos.Show();
            this.Hide();
        }

        // Documentado por: Miguel Flores
        // Este método carga los usuarios desde la base de datos y los muestra en el ComboBox para ser filtrados en la auditoría

        private void CargarUsuarios()
        {
            try
            {
                Dictionary<int, string> empleados = nusuario.Obtenerusuarios();
                cbxfiltroAuditoria.Items.Clear();
                List<Empleado> listaUsuarios = new List<Empleado>();

                // Agregar la opción "Todos"
                listaUsuarios.Add(new Empleado { ID_EMPLEADO = 0, NombreEmpleado = "Todos" });

                // Agregar los empleados de la base de datos, asegurándonos de no agregar duplicados
                foreach (var empleado in empleados)
                {
                    if (!cbxfiltroAuditoria.Items.Contains(empleado.Value)) // Verificar si el usuario ya está
                    {
                        listaUsuarios.Add(new Empleado { ID_EMPLEADO = empleado.Key, NombreEmpleado = empleado.Value });
                    }
                }

                // Asignar los datos al ComboBox
                cbxfiltroAuditoria.DataSource = listaUsuarios;
                cbxfiltroAuditoria.DisplayMember = "NombreEmpleado";
                cbxfiltroAuditoria.ValueMember = "";
                cbxfiltroAuditoria.SelectedIndex = 0; // Seleccionar "Todos" por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
        }

        // Documentado por: Miguel Flores
        // Este método establece la visibilidad de todas las filas en el DataGridView cuando se selecciona la opción "Todos"
        private void FiltrarAccesosPorTodos()
        {
            foreach (DataGridViewRow row in dtgvAuditoria.Rows)
            {
                row.Visible = true;
            }
        }


        // Documentado por: Miguel Flores
        // Este método filtra las filas del DataGridView basándose en el usuario seleccionado del ComboBox
        // Si el usuario es "Todos", se muestran todas las filas
        private void FiltrarAccesosPorUsuario(string usuario)
        {
            if (dtgvAuditoria.IsCurrentCellInEditMode)
            {
                dtgvAuditoria.EndEdit();
            }

            dtgvAuditoria.ClearSelection();
            dtgvAuditoria.CurrentCell = null;

            foreach (DataGridViewRow row in dtgvAuditoria.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    string usuarioEnFila = row.Cells[2].Value.ToString().Trim();
                    row.Visible = usuario == "Todos" || usuarioEnFila.Equals(usuario, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    row.Visible = usuario == "Todos";
                }
            }
        }

        // Documentado por: Miguel Flores
        // Este método permite cerrar sesión y redirige al usuario a la pantalla de login
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
