using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Rangos
    {
        // Se crea una nueva instancia de la clase de conexión para gestionar las operaciones con la base de datos.
        // Documentado por: Miguel Flores
        CD_Conexion dconexion = new CD_Conexion();


        // Método que obtiene todos los rangos registrados en la base de datos.
        // Utiliza un SqlDataAdapter para llenar un DataTable con los resultados de la consulta sobre la tabla RANGOS.
        // Documentado por: Miguel Flores
        public DataTable MostrarRangos()
        {
            dconexion.Conectar();
            string query = "select * from RANGOS";
            SqlDataAdapter ObtenerRangos = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Rangos = new DataTable();
            ObtenerRangos.Fill(Rangos);
            return Rangos;
        }
    }
}
