using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Capa_Datos
{
    public class CD_Conexion
    {
        // Declaración de las variables necesarias para manejar la conexión a la base de datos y ejecutar comandos SQL.
        // `conecta` es una instancia de SqlConnection para gestionar la conexión.
        // `cmd` es una instancia de SqlCommand para ejecutar consultas y comandos SQL.
        // Documentado por: Miguel Flores
        public SqlConnection conecta;
        SqlCommand cmd;

        // Este método establece una conexión con la base de datos, utilizando una cadena de conexión específica. 
        // Si ocurre un error durante la conexión, se lanza una excepción con el mensaje de error correspondiente.
        // Documentado por: Miguel Flores
        public SqlConnection Conectar()
        {
            try
            {
                AdjuntarBaseDeDatos();

                // Ajusta la cadena de conexión para que apunte al archivo .mdf de la base de datos
                string sql = @"Server=(localdb)\MSSQLLocalDB;Database=REAUTO;Integrated Security=True;"; 

                conecta = new SqlConnection(sql);
                conecta.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }

            return conecta;
        }


        // Método para probar la conexión con el servidor especificado.
        // Intenta abrir una conexión con el servidor usando la cadena de conexión proporcionada.
        // Retorna 'true' si la conexión es exitosa, 'false' si ocurre un error.
        // Documentado por: Miguel Flores
        private bool ProbarConexion(string servidor)
        {
            string testConnectionString = $"Server={servidor};Integrated Security=True;";
            try
            {

                using (SqlConnection connection = new SqlConnection(testConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Método para desconectar la base de datos si está abierta.
        // Verifica que la conexión no sea nula y esté abierta antes de cerrarla.
        // Documentado por: Miguel Flores
        public void Desconectar()
        {
            if (conecta != null && conecta.State == System.Data.ConnectionState.Open)
            {
                conecta.Close();
            }
        }


        //Método para verificar si la base de datos está adjunta a la instancia de sql.
        //Si la base de datos no está adjunta, la adjunta a la instancia de sql para poder trabajar con ella.
        //Documentado por: Kenny Arias
        public void AdjuntarBaseDeDatos()
        {
            string conexionMaster = @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;";
            string rutaMDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "REAUTO.mdf");
            string rutaLDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "REAUTO_log.ldf");

            try
            {
                using (SqlConnection conexion = new SqlConnection(conexionMaster))
                {
                    conexion.Open();

                    // Verificar si la base de datos ya está adjunta
                    string verificarDB = "IF DB_ID('REAUTO') IS NULL SELECT 0 ELSE SELECT 1;";
                    int existe = (int)new SqlCommand(verificarDB, conexion).ExecuteScalar();

                    if (existe == 0) // Si no está adjunta, la adjuntamos
                    {
                        string adjuntarBD = $"CREATE DATABASE REAUTO ON (FILENAME = '{rutaMDF}'), (FILENAME = '{rutaLDF}') FOR ATTACH;";
                        new SqlCommand(adjuntarBD, conexion).ExecuteNonQuery();
                        MessageBox.Show("Base de datos REAUTO adjuntada automáticamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al adjuntar la base de datos:\n{ex.Message}");
            }
        }


    }

}

           
