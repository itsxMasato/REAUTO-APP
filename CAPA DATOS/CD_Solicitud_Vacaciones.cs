using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Solicitud_Vacaciones
    {
        // Se crea una nueva instancia de la clase de conexión para gestionar las operaciones con la base de datos.
        // Documentado por: Miguel Flores
        CD_Conexion dconexion = new CD_Conexion();


        // Método que registra una solicitud de vacaciones en la base de datos.
        // Inserta los datos de la solicitud, como las fechas, días solicitados, el ID del empleado y el estado.
        // Documentado por: Miguel Flores
        public int RegistrarSolicitud(CE_Solicitud_Vacaciones esolicitud)
        {
            dconexion.Conectar();
            string sql = "INSERT INTO SOLICITUD_VACACIONES VALUES (@FECHA_INICIO, @FECHA_FINAL, @DIAS_SOLICITADOS, @ID_EMPLEADO, @ESTADO)";
            SqlCommand InsertarRespuesta = new SqlCommand(sql, dconexion.Conectar());
            InsertarRespuesta.Parameters.AddWithValue("@fecha_inicio", SqlDbType.Date).Value = esolicitud.FECHA_INICIO;
            InsertarRespuesta.Parameters.AddWithValue("@fecha_final", SqlDbType.Date).Value = esolicitud.FECHA_FINAL;
            InsertarRespuesta.Parameters.AddWithValue("@dias_solicitados", esolicitud.DIAS_SOLICITADOS);
            InsertarRespuesta.Parameters.AddWithValue("@id_empleado", esolicitud.ID_EMPLEADO);
            InsertarRespuesta.Parameters.AddWithValue("@estado", esolicitud.ESTADO);
            var resultado = InsertarRespuesta.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }

        // Método que muestra todas las solicitudes de vacaciones para un empleado específico.
        // Devuelve un DataTable con la información de las solicitudes del empleado.
        // Documentado por: Miguel Flores
        public DataTable MostrarSolicitudesE(int ID_EMPLEADO)
        {
            dconexion.Conectar();
            string query = "SELECT ID_SOLICITUD_V as ID, DIAS_SOLICITADOS, FECHA_INICIO, FECHA_FINAL, ESTADO " +
                           "FROM SOLICITUD_VACACIONES " +
                           "WHERE ID_EMPLEADO = @id_empleado";

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());
            command.Parameters.AddWithValue("@id_empleado", ID_EMPLEADO);

            SqlDataAdapter ObtenerSolicitudes = new SqlDataAdapter(command);

            DataTable Solicitud = new DataTable();
            ObtenerSolicitudes.Fill(Solicitud);

            dconexion.Desconectar();
            return Solicitud;
        }

        // Método que muestra todas las solicitudes de vacaciones aprobadas.
        // Realiza una consulta con un JOIN entre las tablas EMPLEADO, DATOS_SOLICITANTE y SOLICITUD_VACACIONES.
        // Documentado por: Miguel Flores
        public DataTable MostrarAprobadas()
        {
            dconexion.Conectar();
            string query = "select DATOS_SOLICITANTE.NO_IDENTIDAD[DNI], CONCAT(DATOS_SOLICITANTE.NOMBRE,' ' ,DATOS_SOLICITANTE.APELLIDO)[Nombre Empleado],\r\nSOLICITUD_VACACIONES.FECHA_INICIO, SOLICITUD_VACACIONES.FECHA_FINAL from DATOS_SOLICITANTE inner join EMPLEADO\r\non DATOS_SOLICITANTE.ID_DATOS_SOLI = EMPLEADO.ID_DATOS inner join SOLICITUD_VACACIONES\r\non EMPLEADO.ID_EMPLEADO = SOLICITUD_VACACIONES.ID_EMPLEADO\r\nWHERE SOLICITUD_VACACIONES.ESTADO = 'Solicitud Aprobada'";

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());

            SqlDataAdapter ObtenerAprobadas = new SqlDataAdapter(command);

            DataTable SolisAprobs = new DataTable();
            ObtenerAprobadas.Fill(SolisAprobs);

            dconexion.Desconectar();
            return SolisAprobs;
        }


        // Método que obtiene el ID de empleado a partir del nombre de usuario.
        // Realiza una consulta a la tabla USUARIOS y devuelve el ID_EMPLEADO correspondiente al usuario.
        // Documentado por: Miguel Flores
        public int ObtenerIDEmpleadoPorUsuario(string nombreUsuario)
        {
            int idEmpleado = 0;

            using (SqlConnection conexion = dconexion.Conectar())
            {
                string query = "SELECT ID_EMPLEADO FROM USUARIOS WHERE USUARIO = @usuario";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@usuario", nombreUsuario);

                object resultado = command.ExecuteScalar();

                if (resultado != null && resultado != DBNull.Value)
                {
                    idEmpleado = Convert.ToInt32(resultado);
                }
            }

            return idEmpleado;

        }


        // Método que verifica si un empleado tiene alguna solicitud de vacaciones pendiente.
        // Realiza una consulta en la tabla SOLICITUD_VACACIONES y devuelve true si hay solicitudes pendientes para el empleado.
        // Documentado por: Miguel Flores
        public bool ExisteSolicitudPendiente(int idEmpleado)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string query = "SELECT COUNT(*) FROM SOLICITUD_VACACIONES WHERE ID_EMPLEADO = @IdEmpleado AND ESTADO = 'Pendiente'";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        // Método que muestra las solicitudes de vacaciones aprobadas para un mes específico.
        // Si se selecciona un mes, la consulta filtra las solicitudes por ese mes.
        // Documentado por: Miguel Flores
        public DataTable MostrarAprobs(int mesSeleccionado)
        {
            dconexion.Conectar();

            // Modificar la consulta para obtener los meses en español
            string query = "SELECT DATOS_SOLICITANTE.NO_IDENTIDAD[DNI], " +
                           "CONCAT(DATOS_SOLICITANTE.NOMBRE, ' ', DATOS_SOLICITANTE.APELLIDO)[Nombre Empleado], " +
                           "SOLICITUD_VACACIONES.FECHA_INICIO, " +
                           "SOLICITUD_VACACIONES.FECHA_FINAL, " +
                           "DATENAME(MONTH, SOLICITUD_VACACIONES.FECHA_INICIO) AS Mes " +
                           "FROM DATOS_SOLICITANTE " +
                           "INNER JOIN EMPLEADO ON DATOS_SOLICITANTE.ID_DATOS_SOLI = EMPLEADO.ID_DATOS " +
                           "INNER JOIN SOLICITUD_VACACIONES ON EMPLEADO.ID_EMPLEADO = SOLICITUD_VACACIONES.ID_EMPLEADO WHERE SOLICITUD_VACACIONES.ESTADO = 'Solicitud Aprobada' ";

            // Si se seleccionó un mes diferente a "Todos los meses" (mesSeleccionado > 0), agregamos el filtro
            if (mesSeleccionado > 0)
            {
                query += "AND MONTH(SOLICITUD_VACACIONES.FECHA_INICIO) = @Mes ";
            }

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());

            // Si se seleccionó un mes, lo añadimos como parámetro de la consulta
            if (mesSeleccionado > 0)
            {
                command.Parameters.AddWithValue("@Mes", mesSeleccionado);
            }

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dconexion.Desconectar();
            return dt;
        }

        // Método que obtiene las fechas de inicio y final de una solicitud de vacaciones específica.
        // Se utiliza un parámetro de ID de solicitud para recuperar las fechas de la base de datos.
        // Documentado por: Miguel Flores
        public (DateTime fechaInicio, DateTime fechaFinal) ObtenerFechasSolicitud(int idSolicitud)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string sql = "SELECT FECHA_INICIO, FECHA_FINAL FROM SOLICITUD_VACACIONES WHERE ID_SOLICITUD_V = @idSolicitud";
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                Convert.ToDateTime(reader["FECHA_INICIO"]),
                                Convert.ToDateTime(reader["FECHA_FINAL"])
                            );
                        }
                    }
                }
            }
            return (DateTime.MinValue, DateTime.MinValue); // Si no hay datos, devolver valores predeterminados
        }


        // Método que permite editar las fechas de una solicitud de vacaciones ya existente.
        // Recibe el ID de la solicitud, y las nuevas fechas de inicio y final, y actualiza los datos en la base de datos.
        // Documentado por: Miguel Flores
        public int EditarSolicitud(int idSolicitud, DateTime fechaInicio, DateTime fechaFinal)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string sql = "UPDATE SOLICITUD_VACACIONES SET FECHA_INICIO = @fechaInicio, FECHA_FINAL = @fechaFinal WHERE ID_SOLICITUD_V = @idSolicitud";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@fechaFinal", fechaFinal);
                    comando.Parameters.AddWithValue("@idSolicitud", idSolicitud);

                    return comando.ExecuteNonQuery(); // Retorna el número de filas afectadas
                }
            }
        }


    }
}
