using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;
using System.Drawing;

namespace Capa_Datos
{
    public class CD_Usuarios
    {
        // Se crea una nueva instancia de la clase de conexión para gestionar las operaciones con la base de datos.
        // Documentado por: Miguel Flores
        CD_Conexion dconexion = new CD_Conexion();

        // Método que registra un nuevo usuario en la base de datos.
        // La contraseña se encripta antes de ser almacenada en la base de datos.
        // Documentado por: Miguel Flores
        public int RegistrarUsuarios(CE_Usuarios eusuario)
        {
            dconexion.Conectar();
            string encrip = "";
            encrip = encrip = Convert.ToBase64String(Encoding.Unicode.GetBytes(eusuario.CONTRASENA));
            string sql = "insert into USUARIOS values (@id_usuario, @usuario, @contrasena, @id_empleado, @id_puesto, @estado)";
            SqlCommand InsertarUsuario = new SqlCommand(sql, dconexion.Conectar());
            InsertarUsuario.Parameters.AddWithValue("@id_usuario", eusuario.ID_USUARIO);
            InsertarUsuario.Parameters.AddWithValue("@usuario", eusuario.USUARIO);
            InsertarUsuario.Parameters.AddWithValue("@contrasena", encrip);
            InsertarUsuario.Parameters.AddWithValue("@id_empleado", eusuario.ID_EMPLEADO);
            InsertarUsuario.Parameters.AddWithValue("@id_puesto", eusuario.ID_PUESTO);
            InsertarUsuario.Parameters.AddWithValue("@estado", eusuario.ESTADO);
            var resultado = InsertarUsuario.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }

        // Método que genera un registro en la bitácora con la acción realizada por un usuario.
        // Documentado por: Miguel Flores
        public int GenerarBitacora(CE_Usuarios eusuario)
        {
            dconexion.Conectar();
            string query = "insert into Bitacora values (GETDATE(), @USUARIO, @ACCION)";
            SqlCommand insertarAuditoria = new SqlCommand(query, dconexion.Conectar());
            insertarAuditoria.Parameters.AddWithValue("@USUARIO", eusuario.USUARIO);
            insertarAuditoria.Parameters.AddWithValue("@ACCION", eusuario.ACCION);
            var resultado = insertarAuditoria.ExecuteNonQuery();
            dconexion.Desconectar();
            return resultado;
        }

        // Método que muestra todos los registros de la bitácora.
        // Documentado por: Miguel Flores
        public DataTable MostrarAudi()
        {
            dconexion.Conectar();
            string query = "select * from Bitacora";
            SqlDataAdapter ObtenerUsuario = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Usuario = new DataTable();
            ObtenerUsuario.Fill(Usuario);
            dconexion.Desconectar();
            return Usuario;
        }


        // Método que genera un reporte detallado de la bitácora con información adicional del empleado y su puesto.
        // Documentado por: Miguel Flores
        public DataTable ReporteAudi()
        {
            dconexion.Conectar();
            string query = "select BITACORA.FECHA, BITACORA.ACCION, DATOS_SOLICITANTE.NO_IDENTIDAD[DNI], DATOS_SOLICITANTE.NOMBRE, DATOS_SOLICITANTE.APELLIDO, \r\nPUESTOS.NOMBRE_PUESTO[PUESTO], USUARIOS.USUARIO\r\nFROM DATOS_SOLICITANTE INNER JOIN EMPLEADO\r\nON DATOS_SOLICITANTE.ID_DATOS_SOLI = EMPLEADO.ID_DATOS INNER JOIN USUARIOS\r\nON EMPLEADO.ID_EMPLEADO = USUARIOS.ID_EMPLEADO INNER JOIN PUESTOS\r\nON USUARIOS.ID_PUESTO = PUESTOS.ID_PUESTO LEFT JOIN BITACORA\r\nON USUARIOS.USUARIO = BITACORA.USUARIO";
            SqlDataAdapter ObtenerUsuario = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Usuario = new DataTable();
            ObtenerUsuario.Fill(Usuario);
            dconexion.Desconectar();
            return Usuario;
        }

        // Método para verificar si un ID de usuario o nombre de usuario ya existe en la base de datos.
        // Retorna 'true' si ya existe, 'false' si no.
        // Documentado por: Miguel Flores
        public bool VerificarIDUsuario(int idUsuario, string usuario)
        {
           
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM USUARIOS WHERE ID_USUARIO = @ID OR USUARIO = @Usuario", dconexion.Conectar());
            cmd.Parameters.AddWithValue("@ID", idUsuario);
            cmd.Parameters.AddWithValue("@Usuario", usuario.ToLower());

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            dconexion.Desconectar();
            return count > 0; // Retorna true si el ID y el usuario ya existe
        }

        // Método para obtener la lista de usuarios con su información básica.
        // Devuelve un DataTable con los usuarios registrados.
        // Documentado por: Miguel Flores
        public DataTable MostrarUsuario()
        {
            dconexion.Conectar();
            string query = "SELECT USUARIOS.ID_USUARIO, USUARIOS.USUARIO, CONCAT(DATOS_SOLICITANTE.NOMBRE,' ' , DATOS_SOLICITANTE.APELLIDO)[Empleado],\r\nPUESTOS.NOMBRE_PUESTO[Puesto], USUARIOS.ESTADO from USUARIOS INNER JOIN EMPLEADO\r\nON USUARIOS.ID_EMPLEADO = EMPLEADO.ID_EMPLEADO INNER JOIN DATOS_SOLICITANTE\r\nON EMPLEADO.ID_DATOS = DATOS_SOLICITANTE.ID_DATOS_SOLI INNER JOIN PUESTOS\r\nON USUARIOS.ID_PUESTO = PUESTOS.ID_PUESTO ";
            SqlDataAdapter ObtenerUsuario = new SqlDataAdapter(query, dconexion.Conectar());
            DataTable Usuario = new DataTable();
            ObtenerUsuario.Fill(Usuario);
            dconexion.Desconectar();
            return Usuario;
        }

        // Método para editar los datos de un usuario específico en la base de datos.
        // Recibe un objeto 'CE_Usuarios' con los datos a modificar.
        // Documentado por: Miguel Flores
        public int EditarUsuario(CE_Usuarios eusuario)
        {
            using (SqlConnection conexion = dconexion.Conectar())
            {
                string sql = "UPDATE USUARIOS SET usuario = @usuario, id_puesto = @id_puesto, estado = @estado WHERE id_usuario = @id_usuario";
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@USUARIO", eusuario.USUARIO);
                    comando.Parameters.AddWithValue("@id_puesto", eusuario.ID_PUESTO);
                    comando.Parameters.AddWithValue("@ESTADO", eusuario.ESTADO);
                    comando.Parameters.AddWithValue("@ID_USUARIO", eusuario.ID_USUARIO);

                    return comando.ExecuteNonQuery(); // Ejecuta la consulta ANTES de desconectarte
                }
            }

        }

        // Método para verificar si existe un usuario con un nombre de usuario distinto pero el mismo ID.
        // Esto es útil para validar que el usuario no se repita para un ID dado.
        // Documentado por: Miguel Flores
        public bool ExisteUsuarioConIDDistinto(int idUsuario, string usuario)
        {
            dconexion.Conectar(); // Conectar a la base de datos
            string sql = "SELECT COUNT(*) FROM USUARIOS WHERE usuario = @usuario AND id_usuario != @idUsuario";
            SqlCommand cmd = new SqlCommand(sql, dconexion.Conectar());
            cmd.Parameters.AddWithValue("@usuario", usuario); // El nombre de usuario que estás verificando
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario); // El ID del usuario actual para excluirlo
            // Ejecutar la consulta y verificar si hay resultados
            int count = (int)cmd.ExecuteScalar();
            dconexion.Desconectar(); // Desconectar de la base de datos
            // Si count > 0, significa que ya existe un usuario con ese nombre (excluyendo el actual)
            return count > 0;
        }

        // Método para iniciar sesión de un usuario.
        // Retorna 1 si el login es exitoso, 0 si no, y también obtiene el rango y el estado del usuario.
        // Documentado por: Miguel Flores
        public int LoginUsuario(string USUARIO, string CONTRASENA, out int rango, out string estado)
        {
            int resultado = 0;
            rango = 0;
            estado = "Inactivo"; // Valor por defecto

            using (SqlConnection conexion = dconexion.Conectar())
            {
                string encrip = Convert.ToBase64String(Encoding.Unicode.GetBytes(CONTRASENA)); // Codificación en Base64

                string query = "SELECT PUESTOS.ID_RANGO, USUARIOS.ESTADO FROM USUARIOS " +
                               "INNER JOIN PUESTOS ON USUARIOS.ID_PUESTO = PUESTOS.ID_PUESTO " +
                               "WHERE USUARIOS.USUARIO = @USUARIO AND USUARIOS.CONTRASENA = @CONTRASENA";

                using (SqlCommand cmdal = new SqlCommand(query, conexion))
                {
                    cmdal.Parameters.AddWithValue("@USUARIO", USUARIO);
                    cmdal.Parameters.AddWithValue("@CONTRASENA", encrip); // Comparación con Base64

                    using (SqlDataReader reader = cmdal.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rango = Convert.ToInt32(reader["ID_RANGO"]);
                            estado = reader["ESTADO"].ToString();

                            if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                            {
                                resultado = 1; // Login exitoso solo si el estado es Activo
                            }
                        }
                    }
                }
            }
            dconexion.Desconectar();
            return resultado;

        }

        //Este metodo obtiene directamente de la base de datos la contidad de id puesto que hay en la tabla Usuarios.
        //Este se obtiene atraves de una consulta MAX en sql para que recupere el numero maximo.
        public int IdUsuario()
        {
            int ultimoID = 0;
            dconexion.Conectar();

            string query = "SELECT MAX(ID_USUARIO) FROM USUARIOS";
            SqlCommand comando = new SqlCommand(query, dconexion.Conectar());
            object resultado = comando.ExecuteScalar();
            if (resultado != DBNull.Value)
            {
                ultimoID = Convert.ToInt32(resultado);
            }
            return ultimoID + 1;
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        // Método para obtener una lista de puestos de trabajo desde la base de datos.
        // Devuelve un diccionario donde la clave es el ID del puesto y el valor es el nombre del puesto.
        // Documentado por: Miguel Flores
        public Dictionary<int, string> ObtenerPuestos()
        {
            Dictionary<int, string> puestos = new Dictionary<int, string>();
            dconexion.Conectar();
            string query = "SELECT ID_PUESTO, NOMBRE_PUESTO FROM PUESTOS";

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int idPuesto = Convert.ToInt32(reader["ID_PUESTO"]);
                string nombrePuesto = reader["NOMBRE_PUESTO"].ToString();

                // Agregar al diccionario
                puestos.Add(idPuesto, nombrePuesto);
            }

            reader.Close();
            dconexion.Desconectar();
            return puestos;
        }

        // Método para obtener una lista de empleados que no tienen usuario asignado.
        // Devuelve un diccionario con el ID del empleado como clave y el nombre completo del empleado como valor.
        // Documentado por: Miguel Flores
        public Dictionary<int, string> ObtenerEmpleados()
        {
            Dictionary<int, string> empleados = new Dictionary<int, string>();
            dconexion.Conectar();
            string query = "SELECT EMPLEADO.ID_EMPLEADO, CONCAT(DATOS_SOLICITANTE.NOMBRE, ' ', DATOS_SOLICITANTE.APELLIDO) AS Empleado " +
                           "FROM DATOS_SOLICITANTE " +
                           "INNER JOIN EMPLEADO ON DATOS_SOLICITANTE.ID_DATOS_SOLI = EMPLEADO.ID_DATOS " +
                           "LEFT JOIN USUARIOS ON EMPLEADO.ID_EMPLEADO = USUARIOS.ID_EMPLEADO " +
                           "WHERE USUARIOS.USUARIO IS NULL";

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int idEmpleado = Convert.ToInt32(reader["ID_EMPLEADO"]);
                string nombreEmpleado = reader["Empleado"].ToString();

                // Agregar al diccionario
                empleados.Add(idEmpleado, nombreEmpleado);
            }

            reader.Close();
            dconexion.Desconectar();
            return empleados;
        }

        // Método para recuperar el sueldo de un usuario especificado.
        // Devuelve el sueldo del puesto del usuario.
        // Documentado por: Miguel Flores
        public string RecuperarSueldo(string USUARIO)
        {
            dconexion.Conectar();
            string query = "SELECT PUESTOS.SUELDO FROM PUESTOS INNER JOIN USUARIOS\r\nON PUESTOS.ID_PUESTO = USUARIOS.ID_PUESTO INNER JOIN EMPLEADO\r\nON USUARIOS.ID_EMPLEADO = EMPLEADO.ID_EMPLEADO\r\nWHERE USUARIOS.USUARIO = @USUARIO";
            SqlCommand ObtenerSueldo = new SqlCommand(query, dconexion.Conectar());
            ObtenerSueldo.Parameters.AddWithValue("@USUARIO", USUARIO);
            return ObtenerSueldo.ExecuteScalar().ToString();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Verifica si existen usuarios en la base de datos.
        // Usa `ExecuteScalar` para obtener el número de registros de usuarios.
        // Asegura que la conexión se maneje correctamente con un bloque `using`.
        // Si ocurre un error, lo captura y lo propaga con un mensaje claro.
        // Cierra la conexión en el bloque `finally`, independientemente de si hay un error o no.
        // Documentado por: Miguel Flores
        public bool HayUsuarios()
        {
            using (SqlConnection con = dconexion.Conectar())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Usuarios";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al verificar usuarios: " + ex.Message);
                }
                finally
                {
                    dconexion.Desconectar();
                }
            }
        }

        // Crea un usuario administrador en el sistema, insertando registros en varias tablas (RANGOS, PUESTOS, DATOS_SOLICITANTE, EMPLEADO, USUARIOS).
        // La contraseña se codifica en Base64 antes de ser insertada para protegerla de manera básica.
        // Si alguna de las entradas (como el rango o puesto) no existe, se insertan o actualizan según sea necesario.
        // Este método garantiza que solo se cree el administrador si no existe previamente en el sistema.
        // Documentado por: Miguel Flores
        public void CrearUsuarioAdministrador(string usuario, string contraseña)
        {
            // Codificar la contraseña en Base64 utilizando Encoding.Unicode
            string passwordBase64 = Convert.ToBase64String(Encoding.Unicode.GetBytes(contraseña));

            using (SqlConnection conn = dconexion.Conectar())
            {
                // Insertar en la tabla RANGOS (si no existe)
                string queryRango = @"
                IF NOT EXISTS (SELECT 1 FROM RANGOS WHERE ID_RANGO = 1)
                BEGIN
                    INSERT INTO RANGOS (ID_RANGO, NOMBRE_RANGO) VALUES (1, 'Administrador');
                END";

                // Insertar en la tabla PUESTOS (si no existe)
                string queryPuesto = @"
                IF NOT EXISTS (SELECT 1 FROM PUESTOS WHERE ID_PUESTO = 1)
                BEGIN
                    UPDATE PUESTOS
                    SET 
                        NOMBRE_PUESTO = 'CEO',
                        DESCRIPCION_PUESTO = 'Jefe de la empresa',
                        SUELDO = 25000,
                        ID_RANGO = 1
                    WHERE ID_PUESTO = 1;
                END";               

                // Insertar en la tabla DATOS_SOLICITANTE (si no existe)
                string querySolicitante = @"
                IF NOT EXISTS (SELECT 1 FROM DATOS_SOLICITANTE WHERE ID_DATOS_SOLI = 9)
                BEGIN
                    INSERT INTO DATOS_SOLICITANTE (ID_DATOS_SOLI, NO_IDENTIDAD, RTN, NOMBRE, APELLIDO, FECHA_NACIMIENTO, 
                        FORMACION_ACADEMICA, EXPERIENCIA_LABORAL, IDIOMAS, CURSOS, ESTADO)
                    VALUES 
                        (9, '0701196000194', '07011960001949', 'Miguel', 'Flores', '1995-01-14',
                        'Perito Industrial', 'Amplio conocimiento en el ámbito de la mecánica industrial', 'Español', 
                        'Gestión de equipo, Supervisión de proyectos', 'Contratado');
                END";

                // Insertar en la tabla EMPLEADO (si no existe)
                string queryEmpleado = @"
                IF NOT EXISTS (SELECT 1 FROM EMPLEADO WHERE ID_DATOS = 9)
                BEGIN
                    INSERT INTO EMPLEADO (ID_DATOS, FECHA_CONTRATACION)
                    VALUES (9, '2014-10-09');
                END";

                // Insertar en la tabla USUARIOS (asignando el ID_PUESTO y RANGO)
                string queryUsuario = @"
                INSERT INTO USUARIOS (ID_USUARIO, USUARIO, CONTRASENA, ID_EMPLEADO, ID_PUESTO, ESTADO)
                VALUES (1, @Usuario, @Contraseña, 9, 1, 'Activo')";

                // Ejecutar la consulta SQL con los parámetros
                using (SqlCommand cmd = new SqlCommand(queryRango + queryPuesto + querySolicitante + queryEmpleado + queryUsuario, conn))
                {
                    // Agregar los parámetros para el usuario y la contraseña
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contraseña", passwordBase64); // Contraseña en Base64 (encriptada)

                    // Ejecutar la consulta
                    cmd.ExecuteNonQuery();
                }
                dconexion.Desconectar();
            }
        }

        // Obtiene todos los usuarios del sistema, almacenándolos en un diccionario con el ID del usuario como clave 
        // y el nombre de usuario como valor. Este método es útil para obtener una lista rápida de usuarios para mostrar en 
        // interfaces de administración o realizar validaciones. 
        // Documentado por: Miguel Flores
        public Dictionary<int, string> Obtenerusuarios()
        {
            Dictionary<int, string> usuarios = new Dictionary<int, string>();
            dconexion.Conectar();
            string query = "SELECT ID_USUARIO, USUARIO FROM USUARIOS";

            SqlCommand command = new SqlCommand(query, dconexion.Conectar());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID_USUARIO"]);
                string nombreUsuario = reader["USUARIO"].ToString();

                usuarios.Add(id, nombreUsuario);
            }

            reader.Close();
            dconexion.Desconectar();
            return usuarios;
        }


    }
}
                    
