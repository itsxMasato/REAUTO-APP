using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Puestos
    {
        // Se crea una nueva instancia de la clase de conexión para gestionar las operaciones con la base de datos.
        // Documentado por: Miguel Flores
        CD_Conexion dconexion = new CD_Conexion();

        // Método que registra un nuevo puesto en la base de datos.
        // Inserta los datos proporcionados en la tabla PUESTOS utilizando un SqlCommand con parámetros.
        // Documentado por: Miguel Flores
        public int RegistrarPuestos(CE_Puestos epuesto)
        {
            dconexion.Conectar();
            string sql = "insert into PUESTOS values (@id_puesto, @nombre_puesto, @descripcion_puesto, @sueldo, @id_rango)";
            SqlCommand InsertarPuesto = new SqlCommand(sql, dconexion.Conectar());
            InsertarPuesto.Parameters.AddWithValue("@id_puesto", epuesto.ID_PUESTO);
            InsertarPuesto.Parameters.AddWithValue("@nombre_puesto", epuesto.NOMBRE_PUESTO);
            InsertarPuesto.Parameters.AddWithValue("@descripcion_puesto", epuesto.DESCRIPCION_PUESTO);
            InsertarPuesto.Parameters.AddWithValue("@sueldo", epuesto.SUELDO);
            InsertarPuesto.Parameters.AddWithValue("@id_rango", epuesto.ID_RANGO);
            var resultado = InsertarPuesto.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }


        //Este metodo obtiene directamente de la base de datos la contidad de id puesto que hay en la tabla Puestos.
        //Este se obtiene atraves de una consulta MAX en sql para que recupere el numero maximo.
        public int IdPuesto() 
        {
            int ultimoID = 0;
            dconexion.Conectar();

            string query = "SELECT MAX(ID_PUESTO) FROM PUESTOS";
            SqlCommand comando = new SqlCommand(query, dconexion.Conectar());
            object resultado = comando.ExecuteScalar();
            if (resultado != DBNull.Value)
            {
                ultimoID = Convert.ToInt32(resultado);
            }
            return ultimoID + 1;
        }
           
        // Método que obtiene la lista de puestos registrados en la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre los puestos.
        // Documentado por: Miguel Flores
        public DataTable MostrarPuesto()
        {
            dconexion.Conectar();
            string query = "select PUESTOS.ID_PUESTO, PUESTOS.NOMBRE_PUESTO, PUESTOS.DESCRIPCION_PUESTO from PUESTOS";
            SqlDataAdapter ObtenerPuesto = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Puesto = new DataTable();
            ObtenerPuesto.Fill(Puesto);
            return Puesto;
        }

        // Método que actualiza los datos de un puesto en la base de datos.
        // Utiliza un SqlCommand con parámetros para modificar el nombre, sueldo y descripción de un puesto específico.
        // Documentado por: Miguel Flores
        public int EditarPuesto(CE_Puestos puesto)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string query = "UPDATE PUESTOS SET NOMBRE_PUESTO = @NOMBRE, SUELDO = @SUELDO, DESCRIPCION_PUESTO = @DESCRIPCION WHERE ID_PUESTO = @ID";

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@ID", puesto.ID_PUESTO);
                    command.Parameters.AddWithValue("@NOMBRE", puesto.NOMBRE_PUESTO);
                    command.Parameters.AddWithValue("SUELDO", puesto.SUELDO);
                    command.Parameters.AddWithValue("@DESCRIPCION", puesto.DESCRIPCION_PUESTO);

                    return command.ExecuteNonQuery();
                }
            }
        }

        // Método que verifica si un puesto con un ID específico ya existe en la base de datos.
        // Realiza una consulta de conteo y devuelve true si el puesto existe, de lo contrario, devuelve false.
        // Documentado por: Miguel Flores
        public bool ExisteIdPuesto(int idPuesto)
        {
            dconexion.Conectar();
            string sql = "SELECT COUNT(*) FROM PUESTOS WHERE ID_PUESTO = @id_puesto";
            SqlCommand cmd = new SqlCommand(sql, dconexion.Conectar());
            cmd.Parameters.AddWithValue("@id_puesto", idPuesto);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            dconexion.Desconectar();

            return count > 0;
        }

        // Método que verifica si un puesto con un nombre específico ya existe en la base de datos.
        // Realiza una consulta de conteo y devuelve true si el nombre ya existe, de lo contrario, devuelve false.
        // Documentado por: Miguel Flores
        public bool ExisteNombrePuesto(string nombrePuesto)
        {
            dconexion.Conectar();
            string sql = "SELECT COUNT(*) FROM PUESTOS WHERE NOMBRE_PUESTO = @nombre_puesto";
            SqlCommand cmd = new SqlCommand(sql, dconexion.Conectar());
            cmd.Parameters.AddWithValue("@nombre_puesto", nombrePuesto);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            dconexion.Desconectar();

            return count > 0; // Si count > 0, el nombre ya existe
        }

        // Método que obtiene el sueldo de un puesto específico por su ID desde la base de datos.
        // Ejecuta una consulta para obtener el sueldo de un puesto dado su ID.
        // Documentado por: Miguel Flores
        public int ObtenerSueldoPorID(int idPuesto)
        {
            int sueldo = 0;

            using (SqlConnection conexion = dconexion.Conectar())
            {
                string sql = "SELECT SUELDO FROM PUESTOS WHERE ID_PUESTO = @id_puesto";
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id_puesto", idPuesto);
                    object resultado = comando.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        sueldo = Convert.ToInt32(resultado);  // Usamos Convert.ToInt32 para asegurarnos de que se convierte correctamente a int
                    }
                }
            }

            return sueldo;
        }


        //----------------------------------------------------------------------------------------------------------------------------------

        // Método que obtiene los datos de todos los puestos desde una vista de reporte en la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre los puestos en la vista.
        // Documentado por: Miguel Flores
        public DataTable MostrarReportePuesto()
        {
            dconexion.Conectar();
            string query = "select * from Vista_Reporte_Puestos";
            SqlDataAdapter ObtenerPuesto = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Puesto = new DataTable();
            ObtenerPuesto.Fill(Puesto);
            return Puesto;
        }


        // Método que obtiene un diccionario con los ID y nombres de los puestos registrados.
        // Utiliza un SqlDataReader para leer los datos de la tabla PUESTOS y los agrega a un diccionario.
        // Documentado por: Miguel Flores
        public Dictionary<int, string> ObtenerPuestos()
        {
            Dictionary<int, string> puestos = new Dictionary<int, string>();
            dconexion.Conectar();
            string query = "SELECT ID_PUESTO , NOMBRE_PUESTO  FROM PUESTOS";

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID_PUESTO"]);
                string nombrePuesto = reader["NOMBRE_PUESTO"].ToString();

                puestos.Add(id, nombrePuesto);
            }

            reader.Close();
            dconexion.Desconectar();
            return puestos;
        }


    }
}
