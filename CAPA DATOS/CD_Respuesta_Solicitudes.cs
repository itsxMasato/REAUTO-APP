using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_Respuesta_Solicitudes
    {
        // Se crea una nueva instancia de la clase de conexión para gestionar las operaciones con la base de datos.
        // Documentado por: Miguel Flore
        CD_Conexion dconexion = new CD_Conexion();

        // Método que registra una respuesta para una solicitud de vacaciones.
        // Inserta una nueva respuesta en la tabla RESPUESTAS en la base de datos.
        // Documentado por: Miguel Flores
        public int RegistrarRespuesta(CE_Respuesta_Solicitudes erespuesta)
        {
            dconexion.Conectar();
            string sql = "insert into RESPUESTAS values (@ID_SOLICITUD_V, @RESPUESTA)";
            SqlCommand InsertarRespuesta = new SqlCommand(sql, dconexion.Conectar());
            InsertarRespuesta.Parameters.AddWithValue("@id_solicitud_v", erespuesta.ID_SOLICITUD_V);
            InsertarRespuesta.Parameters.AddWithValue("@respuesta", erespuesta.RESPUESTA);

            var resultado = InsertarRespuesta.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }

        // Método que valida si una solicitud de vacaciones existe en la base de datos.
        // Verifica si una solicitud con el ID proporcionado existe en la tabla SOLICITUD_VACACIONES.
        // Documentado por: Miguel Flores
        public bool ExisteSolicitud(int idSolicitud)
        {
            dconexion.Conectar();
            string query = "SELECT COUNT(*) FROM SOLICITUD_VACACIONES WHERE ID_SOLICITUD_V = @ID_SOLICITUD_V";
            SqlCommand command = new SqlCommand(query, dconexion.Conectar());
            command.Parameters.AddWithValue("@ID_SOLICITUD_V", idSolicitud);
            int count = (int)command.ExecuteScalar();  // Ejecuta la consulta y obtiene el resultado
            dconexion.Desconectar();
            return count > 0; // Si count > 0, la solicitud existe.
        }

        // Método que obtiene todas las solicitudes de vacaciones pendientes.
        // Se utiliza un SqlDataAdapter para llenar un DataTable con las solicitudes.
        // Documentado por: Miguel Flores
        public DataTable MostrarSolicitudes()
        {
            dconexion.Conectar();
            string query = "select SOLICITUD_VACACIONES.ID_SOLICITUD_V AS ID, SOLICITUD_VACACIONES.DIAS_SOLICITADOS, SOLICITUD_VACACIONES.FECHA_INICIO, SOLICITUD_VACACIONES.FECHA_FINAL, SOLICITUD_VACACIONES.ESTADO from SOLICITUD_VACACIONES";
            SqlDataAdapter ObtenerSolicitudes = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Solicitud = new DataTable();
            ObtenerSolicitudes.Fill(Solicitud);
            dconexion.Desconectar();
            return Solicitud;
        }

        // Método que valida si la fecha de una solicitud de vacaciones es válida.
        // Verifica si la fecha de inicio de la solicitud ya ha pasado.
        // Documentado por: Miguel Flores
        public bool ValidarFechaSolicitud(int idSolicitud)
        {
            bool esValida = false;
            dconexion.Conectar(); // Usa la conexión existente
            try
            {
                string query = @"
            SELECT FECHA_INICIO 
            FROM SOLICITUD_VACACIONES 
            WHERE ID_SOLICITUD_V = @ID_SOLICITUD_V";

                using (SqlCommand cmd = new SqlCommand(query, dconexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@ID_SOLICITUD_V", idSolicitud);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        DateTime fechaInicio = Convert.ToDateTime(result);

                        // Verifica si la fecha ya pasó
                        if (fechaInicio < DateTime.Now.Date)
                        {
                            return false; // No es válida porque la fecha ya pasó
                        }
                        esValida = true; // Es válida si la fecha aún no ha pasado
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar la fecha: " + ex.Message);
            }
            finally
            {
                dconexion.Desconectar(); // Asegura que la conexión se cierre
            }
            return esValida;
        }

        // Método que actualiza el estado de las solicitudes de vacaciones vencidas.  
        // Si la fecha de inicio de la solicitud ya ha pasado y su estado sigue siendo "Pendiente",  
        // se actualiza automáticamente a "Rechazada".  
        // Documentado por: Astrid Gonzales
        public void ActualizarSolicitudesVencidas()
        {
            dconexion.Conectar();
            string query = "UPDATE SOLICITUD_VACACIONES SET ESTADO = 'Rechazada' WHERE ESTADO = 'Pendiente' AND FECHA_INICIO < GETDATE();";

            using (SqlCommand comando = new SqlCommand(query, dconexion.Conectar()))
            {
                comando.ExecuteNonQuery();
                dconexion.Desconectar();
            }
        }

    }
}
