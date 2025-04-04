using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;
using static Capa_Negocio.CN_Empleado;

namespace Capa_Negocio
{
    // Gestiona las operaciones relacionadas con los usuarios en el sistema.
    // Documentado por: Olman Martinez
    public class CN_Usuarios
    {
        // Instancia de la capa de datos para manejar los usuarios.
        // Documentado por: Olman Martinez
        CD_Usuarios nusuario = new CD_Usuarios();

        // Registra un nuevo usuario en el sistema.
        // Retorna el ID del usuario registrado.
        // Documentado por: Olman Martinez
        public int RegistrarUsuario(CE_Usuarios eusuario)
        {
            return nusuario.RegistrarUsuarios(eusuario);
        }

        // Muestra la lista de usuarios registrados en el sistema.
        // Retorna un DataTable con los usuarios.
        // Documentado por: Olman Martinez
        public DataTable MostrarUsuario()
        {
            nusuario.MostrarUsuario();
            return nusuario.MostrarUsuario();
        }

        // Verifica si un usuario existe basado en su ID y nombre de usuario.
        // Retorna true si existe, de lo contrario false.
        // Documentado por: Olman Martinez
        public bool ExisteIDUsuario(int idUsuario, string usuario)
        {
            return new CD_Usuarios().VerificarIDUsuario(idUsuario, usuario);
        }

        // Edita los detalles de un usuario.
        // Retorna la cantidad de registros afectados.
        // Documentado por: Olman Martinez
        public int EditarUsuario(CE_Usuarios eusuario)
        {
            return nusuario.EditarUsuario(eusuario);
        }

        // Verifica si un usuario con un ID distinto ya existe.
        // Retorna true si existe un usuario con el ID distinto, de lo contrario false.
        // Documentado por: Olman Martinez
        public bool ExisteUsuarioConIDDistinto(int idUsuario, string usuario)
        {

            return nusuario.ExisteUsuarioConIDDistinto(idUsuario, usuario);
        }

        // Realiza el login de un usuario.
        // Retorna el ID del usuario si la autenticación es exitosa.
        // Los parámetros RANGO y ESTADO se utilizan para obtener el rango y estado del usuario.
        // Documentado por: Olman Martinez
        public int LoguearUsuario(string USUARIO, string CONTRASENA, out int RANGO, out string ESTADO)
        {
            return nusuario.LoginUsuario(USUARIO, CONTRASENA, out RANGO, out ESTADO);
        }

        public int IdUsuario()
        {
            return nusuario.IdUsuario();
        }

        //-----------------------------------------------------------------------------------------------------------------

        // Obtiene los nombres de los puestos en el sistema.
        // Retorna un diccionario con los IDs y nombres de los puestos.
        // Documentado por: Olman Martinez
        public Dictionary<int, string> ObtenerNombresPuestos()
        {
            CD_Usuarios cdUsuarios = new CD_Usuarios();
            return cdUsuarios.ObtenerPuestos();
        }

        // Obtiene los nombres de los empleados registrados.
        // Retorna un diccionario con los IDs y nombres de los empleados.
        // Documentado por: Olman Martinez
        public Dictionary<int, string> ObtenerNombresEmpleados()
        {
            CD_Usuarios cdUsuarios = new CD_Usuarios();
            return cdUsuarios.ObtenerEmpleados();
        }

        // Obtiene el sueldo de un empleado en base a su nombre de usuario.
        // Retorna el sueldo del empleado.
        // Documentado por: Olman Martinez
        public string ObtenerSueldoEmpleado(string USUARIO)
        {
            return nusuario.RecuperarSueldo(USUARIO);
        }

        //-----------------------------------------------------------------------------------------------------------------

        // Verifica si hay usuarios registrados en el sistema.
        // Retorna true si hay usuarios registrados, de lo contrario false.
        // Documentado por: Olman Martinez
        public bool HayUsuariosRegistrados()
        {
            return nusuario.HayUsuarios();
        }

        // Crea un usuario administrador con un nombre de usuario y una contraseña especificados.
        // Documentado por: Olman Martinez
        public void CrearUsuarioAdministrador(string usuario, string contraseña)
        {
            nusuario.CrearUsuarioAdministrador(usuario, contraseña);
        }

        // Genera un registro de auditoría (bitácora) para un usuario.
        // Retorna el ID de la bitácora generada.
        // Documentado por: Olman Martinez
        public int GenerarAuditoria(CE_Usuarios eusuario)
        {
            return nusuario.GenerarBitacora(eusuario);
        }

        // Muestra la lista de registros de auditoría.
        // Retorna un DataTable con los registros de auditoría.
        // Documentado por: Olman Martinez
        public DataTable MostrarAuditoria()
        {
            nusuario.MostrarAudi();
            return nusuario.MostrarAudi();
        }

        // Obtiene un diccionario con los IDs y nombres de los usuarios.
        // Documentado por: Olman Martinez
        public Dictionary<int, string> Obtenerusuarios()
        {
            return nusuario .Obtenerusuarios();
        }


    }
}

