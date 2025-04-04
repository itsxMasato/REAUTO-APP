using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REAUTO_APP
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args) // Aceptar argumentos
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form mainForm;

            // Verificar si se pasó el argumento --backup
            if (args.Contains("--backup"))
            {
                // Abrir directamente el formulario de respaldo
                mainForm = new Backup();
            }
            else
            {
                // Ejecutar la aplicación normalmente
                mainForm = new login();
            }

            // Suscribir al evento FormClosed para cerrar la aplicación cuando se cierre el formulario
            mainForm.FormClosed += MainForm_FormClosed;

            Application.Run(mainForm);
        }

        private static void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Finalizar la aplicación cuando se cierre el formulario principal
            Application.Exit();
        }
    }

}
