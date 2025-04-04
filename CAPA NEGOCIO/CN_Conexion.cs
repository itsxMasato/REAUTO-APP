using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Negocio
{
    // Gestiona la conexión a la base de datos para su uso en la capa de negocio.
    // Documentado por: Olman Martinez
    public class CN_Conexion
    {
        // Instancia de la clase de conexión a la base de datos.
        // Permite establecer y gestionar la conexión con el servidor SQL.
        // Documentado por: Olman Martinez
        public CD_Conexion cinicio = new CD_Conexion();

        // Obtiene una conexión activa con la base de datos.
        // Se utiliza para interactuar con la base de datos desde la capa de negocio.
        // Documentado por: Olman Martinez
        public SqlConnection ObtenerConexion()
        {
            return cinicio.Conectar();
        }

        // Abre la conexión a la base de datos si está cerrada.
        // Garantiza que la conexión esté disponible antes de ejecutar consultas.
        // Documentado por: Olman Martinez
        public void Open()
        {
            var conexion = ObtenerConexion();
            if (conexion != null && conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }

        // Cierra la conexión a la base de datos si está abierta.
        // Evita consumos innecesarios de recursos manteniendo conexiones activas sin necesidad.
        // Documentado por: Olman Martinez
        public void Close()
        {
            var conexion = ObtenerConexion();
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        //Se llama la funcion para verificar y adjuntar la base de datos al momento de interactuar directamente con ella.
        //Documentado por: Kenny Arias
        public void Adjuntar()
        {
            cinicio.AdjuntarBaseDeDatos();
        }
    }
}