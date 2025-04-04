using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Datos;
using System.Data.SqlClient;
using System.Data;
using iText.Kernel.Utils;

namespace Capa_Datos
{
    public class CD_EMPLEADO
    {

        CD_Conexion dconexion = new CD_Conexion();

        // Registra un nuevo solicitante en la base de datos.
        // Se espera que los datos del solicitante sean validados antes de llamar a este método.
        //Documentado por: Miguel Flores
        public int RegistrarSolicitante(CE_Empleado eempleado)
        {
            dconexion.Conectar();
            string sql = "insert into DATOS_SOLICITANTE values (@ID_DATOS_SOLI, @NO_IDENTIDAD, @RTN, @NOMBRE, @APELLIDO, @FECHA_NACIMIENTO, @FORMACION_ACADEMICA, @EXPERIENCIA_LABORAL, @IDIOMAS, @CURSOS, @ESTADO)";
            SqlCommand InsertarSolicitante = new SqlCommand(sql, dconexion.Conectar());
            InsertarSolicitante.Parameters.AddWithValue("@ID_DATOS_SOLI", eempleado.ID_DATOS_SOLI);
            InsertarSolicitante.Parameters.AddWithValue("@NO_IDENTIDAD", eempleado.NO_IDENTIDAD);
            InsertarSolicitante.Parameters.AddWithValue("@RTN", eempleado.RTN);
            InsertarSolicitante.Parameters.AddWithValue("@NOMBRE", eempleado.NOMBRE);
            InsertarSolicitante.Parameters.AddWithValue("@APELLIDO", eempleado.APELLIDO);
            InsertarSolicitante.Parameters.Add("@FECHA_NACIMIENTO", SqlDbType.Date).Value = eempleado.FECHA_DE_NACIMIENTO;
            InsertarSolicitante.Parameters.AddWithValue("@FORMACION_ACADEMICA", eempleado.FORMACION_ACADEMICA);
            InsertarSolicitante.Parameters.AddWithValue("@EXPERIENCIA_LABORAL", eempleado.EXPERIENCIA_LABORAL);
            InsertarSolicitante.Parameters.AddWithValue("@IDIOMAS", eempleado.IDIOMAS);
            InsertarSolicitante.Parameters.AddWithValue("@CURSOS", eempleado.CURSOS);
            InsertarSolicitante.Parameters.AddWithValue("@ESTADO", eempleado.ESTADO);
            var resultado = InsertarSolicitante.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }

        //Función para editar un solicitante en la base de datos en caso de un error u modificacion
        //Documentado por: Kenny Arias
        public int EditarSolicitante(CE_Empleado eempleado)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string sql = "update DATOS_SOLICITANTE set NO_IDENTIDAD = @IDENTIDAD, RTN = @RTN, NOMBRE = @NOMBRE,\r\nAPELLIDO = @APELLIDO, FECHA_NACIMIENTO = @FECHA_NAC, FORMACION_ACADEMICA = @FORMA,\r\nEXPERIENCIA_LABORAL = @EXPL, IDIOMAS = @IDIOMAS, CURSOS = @CURSOS\r\nWHERE ID_DATOS_SOLI = @ID_DATOS_SOLI";
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@ID_DATOS_SOLI", eempleado.ID_DATOS_SOLI);
                    comando.Parameters.AddWithValue("@IDENTIDAD", eempleado.NO_IDENTIDAD);
                    comando.Parameters.AddWithValue("@RTN", eempleado.RTN);
                    comando.Parameters.AddWithValue("@NOMBRE", eempleado.NOMBRE);
                    comando.Parameters.AddWithValue("@APELLIDO", eempleado.APELLIDO);
                    comando.Parameters.Add("@FECHA_NAC", SqlDbType.Date).Value = eempleado.FECHA_DE_NACIMIENTO;
                    comando.Parameters.AddWithValue("@FORMA", eempleado.FORMACION_ACADEMICA);
                    comando.Parameters.AddWithValue("@EXPL", eempleado.EXPERIENCIA_LABORAL);
                    comando.Parameters.AddWithValue("@IDIOMAS", eempleado.IDIOMAS);
                    comando.Parameters.AddWithValue("@CURSOS", eempleado.CURSOS);

                    return comando.ExecuteNonQuery(); // Ejecuta la consulta ANTES de desconectarte
                }
            }

        }

        //Este metodo obtiene directamente de la base de datos la contidad de id puesto que hay en la tabla EMPLEADO.
        //Este se obtiene atraves de una consulta MAX en sql para que recupere el numero maximo.
        public int IdEmpleado()
        {
            int ultimoID = 0;
            dconexion.Conectar();

            string query = "SELECT MAX(ID_EMPLEADO) FROM EMPLEADO";
            SqlCommand comando = new SqlCommand(query, dconexion.Conectar());
            object resultado = comando.ExecuteScalar();
            if (resultado != DBNull.Value)
            {
                ultimoID = Convert.ToInt32(resultado);
            }
            return ultimoID + 1;
        }


        //Este metodo obtiene directamente de la base de datos la contidad de id puesto que hay en la tabla EMPLEADO.
        //Este se obtiene atraves de una consulta MAX en sql para que recupere el numero maximo.
        public int IdDatosSoli()
        {
            int ultimoID = 0;
            dconexion.Conectar();

            string query = "SELECT MAX(ID_DATOS_SOLI) FROM DATOS_SOLICITANTE";
            SqlCommand comando = new SqlCommand(query, dconexion.Conectar());
            object resultado = comando.ExecuteScalar();
            if (resultado != DBNull.Value)
            {
                ultimoID = Convert.ToInt32(resultado);
            }
            return ultimoID + 1;
        }

        // Verifica si un solicitante ya existe en la base de datos antes de registrarlo.
        // Se busca coincidencia por ID o número de identidad, ya que ambos deben ser únicos.
        public bool VerificarExistenciaSoli(int idSolicitante, string no_identidad)
        {
            dconexion.Conectar();

            string query = "SELECT COUNT(*) FROM DATOS_SOLICITANTE WHERE id_datos_soli = @id_datos_soli OR NO_IDENTIDAD = @no_identidad";
            SqlCommand cmd = new SqlCommand(query, dconexion.Conectar());
            cmd.Parameters.AddWithValue("@id_datos_soli", idSolicitante);
            cmd.Parameters.AddWithValue("@no_identidad", no_identidad);

            int existe = (int)cmd.ExecuteScalar();
            dconexion.Desconectar();

            return existe > 0;
        }

        // Obtiene una lista de solicitantes cuyo estado es 'Pendiente'.
        // Este estado indica que aún no han sido contratados.
        public DataTable MostrarSolicitantes()
        {
            dconexion.Conectar();
            string query = "select * from DATOS_SOLICITANTE WHERE DATOS_SOLICITANTE.ESTADO = 'Pendiente'";
            SqlDataAdapter ObtenerSolicitante = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Solicitante = new DataTable();
            ObtenerSolicitante.Fill(Solicitante);
            dconexion.Desconectar();
            return Solicitante;
        }


        // Registra un nuevo empleado en la base de datos.
        // Un empleado debe estar previamente registrado como solicitante.
        public int RegistrarEmpleados(CE_Empleado eempleado)
        {
            dconexion.Conectar();
            string sql = "insert into EMPLEADO values (@ID_EMPLEADO, @ID_DATOS, @FECHA_CONTRATACION)";
            SqlCommand InsertarEmpleado = new SqlCommand(sql, dconexion.Conectar());
            InsertarEmpleado.Parameters.AddWithValue("@ID_EMPLEADO", eempleado.ID_EMPLEADO);
            InsertarEmpleado.Parameters.AddWithValue("@ID_DATOS", eempleado.ID_DATOS);
            InsertarEmpleado.Parameters.AddWithValue("@FECHA_CONTRATACION", Convert.ToDateTime(eempleado.FECHA_CONTRATACION));
            var resultado = InsertarEmpleado.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }

        // Actualiza la fecha de contratación de un empleado en la base de datos.
        // Se debe asegurar que el ID del empleado existe antes de ejecutar este método.
        //Documentado por: Kenny Arias
        public int EditarEmpleado(CE_Empleado eempleado)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string sql = "UPDATE EMPLEADO SET ID_EMPLEADO = @ID_EMPLEADO,  FECHA_CONTRATACION = @FECHA_CONTRATACION WHERE ID_EMPLEADO = @ID_EMPLEADO";
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@ID_EMPLEADO", eempleado.ID_EMPLEADO);
                    comando.Parameters.AddWithValue("@FECHA_CONTRATACION", Convert.ToDateTime(eempleado.FECHA_CONTRATACION));

                    return comando.ExecuteNonQuery(); // Ejecuta la consulta ANTES de desconectarte
                }
            }

        }

        // Obtiene una lista de empleados con su información básica.
        // Se unen datos del empleado con su información personal del solicitante.
        public DataTable MostrarEmpleados()
        {
            dconexion.Conectar();
            string query = "select EMPLEADO.ID_EMPLEADO, DATOS_SOLICITANTE.NO_IDENTIDAD[DNI], \r\nCONCAT(DATOS_SOLICITANTE.NOMBRE,' ' ,DATOS_SOLICITANTE.APELLIDO)[Nombre Empleado], \r\nEMPLEADO.FECHA_CONTRATACION from EMPLEADO inner join DATOS_SOLICITANTE\r\non EMPLEADO.ID_DATOS = DATOS_SOLICITANTE.ID_DATOS_SOLI";
            SqlDataAdapter ObtenerEmpleado = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Empleado = new DataTable();
            ObtenerEmpleado.Fill(Empleado);
            dconexion.Desconectar();
            return Empleado;
        }

        // Filtra empleados según su estado (Activos o Inactivos) usando una vista en la base de datos.
        public DataTable FiltrarEmpleados(string estado)
        {
            dconexion.Conectar();
            string query = "SELECT * FROM VW_REPORTE_DE_EMPLEADOS";

            if (estado == "Activos")
            {
                query += " WHERE ESTADO = 'Activo'";
            }
            else if (estado == "Inactivos")
            {
                query += " WHERE ESTADO = 'Inactivo'";
            }

            SqlDataAdapter ObtenerEmpleado = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Empleado = new DataTable();
            ObtenerEmpleado.Fill(Empleado);
            dconexion.Desconectar();
            return Empleado;
        }

        

        // Método que filtra los solicitantes según el ID_DATOS_SOLI.
        // Si no se pasa un ID, retorna todos los solicitantes.
        // Documentado por: Miguel Flores
        public DataTable FiltrarSolicitantes(int? idSolicitante = null)
        {
            string query = "SELECT * FROM Datos_Solicitante WHERE ESTADO = 'PENDIENTE'"; // Filtramos por estado 'PENDIENTE'

            // Si se pasa un ID de solicitante, agregamos la condición WHERE adicional
            if (idSolicitante.HasValue)
            {
                query += " AND ID_DATOS_SOLI = @ID_DATOS_SOLI"; // Aseguramos que se filtre por ID además de estado
            }

            using (SqlConnection conexion = dconexion.Conectar()) // Usando tu método de conexión
            {
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Si hay un ID, agregamos el parámetro correspondiente
                    if (idSolicitante.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@ID_DATOS_SOLI", idSolicitante.Value);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader()) // Ejecutamos la consulta y leemos los resultados
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader); // Cargamos los resultados en un DataTable
                        return dt; // Devolvemos el DataTable
                    }
                }
            }
        }


    }
}
