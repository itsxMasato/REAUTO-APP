using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Inicio
    {
        // Se crea una nueva instancia de la clase de conexión para gestionar las operaciones con la base de datos.
        // Documentado por: Miguel Flores
        CD_Conexion dconexion = new CD_Conexion();

        // Método que obtiene todas las solicitudes de vacaciones con estado 'Pendiente' desde la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre las solicitudes pendientes.
        // Documentado por: Miguel Flores
        public DataTable MostrarInicio()
        {
            dconexion.Conectar();
            string query = "select * from SOLICITUD_VACACIONES where ESTADO = 'Pendiente'";
            SqlDataAdapter ObtenerInicio = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Inicio = new DataTable();
            ObtenerInicio.Fill(Inicio);
            return Inicio;
        }

        // Método que obtiene las solicitudes de vacaciones con detalles del empleado que las solicitó,
        // incluyendo fecha de inicio, fecha final y días solicitados, utilizando un JOIN entre las tablas relacionadas.
        // Documentado por: Miguel Flores
        public DataTable MostrarSolicitudVacaciones()
        {
            dconexion.Conectar();
            string query = "select SOLICITUD_VACACIONES.ID_SOLICITUD_V, SOLICITUD_VACACIONES.ESTADO,\r\nCONCAT(DATOS_SOLICITANTE.NOMBRE, ' ', DATOS_SOLICITANTE.APELLIDO)[EMPLEADO],\r\nSOLICITUD_VACACIONES.FECHA_INICIO, SOLICITUD_VACACIONES.FECHA_FINAL,\r\nSOLICITUD_VACACIONES.DIAS_SOLICITADOS FROM SOLICITUD_VACACIONES INNER JOIN EMPLEADO\r\nON SOLICITUD_VACACIONES.ID_EMPLEADO = EMPLEADO.ID_EMPLEADO INNER JOIN DATOS_SOLICITANTE\r\nON EMPLEADO.ID_DATOS = DATOS_SOLICITANTE.ID_DATOS_SOLI";
            SqlDataAdapter ObtenerSolicitudes = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Solicitudes = new DataTable();
            ObtenerSolicitudes.Fill(Solicitudes);
            return Solicitudes;
        }

        // Método que obtiene los reportes de vacantes desde la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre vacantes disponibles.
        // Documentado por: Miguel Flores
        public DataTable MostrarVAcantes()
        {
            dconexion.Conectar();
            string query = "Select * from VW_Reporte_Vacantes\r\n";
            SqlDataAdapter ObtenerVacantes = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Vacantes = new DataTable();
            ObtenerVacantes.Fill(Vacantes);
            return Vacantes;
        }

        // Método que obtiene los reportes de vacaciones desde la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre las vacaciones tomadas.
        // Documentado por: Miguel Flores
        public DataTable MostrarVacaciones()
        {
            dconexion.Conectar();
            string query = "Select * from VW_Reporte_Vacaciones\r\n";
            SqlDataAdapter ObtenerVacaciones = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Vacaciones = new DataTable();
            ObtenerVacaciones.Fill(Vacaciones);
            return Vacaciones;
        }

        // Método que obtiene los reportes de empleados desde la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre empleados.
        // Documentado por: Miguel Flores
        public DataTable MostrarEmpleados()
        {
            dconexion.Conectar();
            string query = "Select * from VW_Reporte_Vacaciones\r\n";
            SqlDataAdapter ObtenerEmpleados = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Empleados = new DataTable();
            ObtenerEmpleados.Fill(Empleados);
            return Empleados;
        }
    }
}

