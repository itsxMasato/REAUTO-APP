using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Negocio
{
    // Gestiona las solicitudes de vacaciones realizadas por los empleados.
    // Documentado por: Olman Martinez
    public class CN_SolicitudVacaciones
    {
        // Instancia de la capa de datos para manejar las solicitudes de vacaciones.
        // Documentado por: Olman Martinez
        CD_Solicitud_Vacaciones nsolicitud = new CD_Solicitud_Vacaciones();

        // Registra una nueva solicitud de vacaciones en la base de datos.
        // Retorna el ID de la solicitud registrada.
        // Documentado por: Olman Martinez
        public int RegistrarSolicitud(CE_Solicitud_Vacaciones esolicitud)
        {

            return nsolicitud.RegistrarSolicitud(esolicitud);
        }

        // Obtiene el ID del empleado a partir del nombre de usuario.
        // Retorna el ID del empleado asociado al usuario.
        // Documentado por: Olman Martinez
        public int ObtenerIDEmpleadoPorUsuario(string nombreUsuario)
        {

            return nsolicitud.ObtenerIDEmpleadoPorUsuario(nombreUsuario);
        }

        // Verifica si un empleado tiene una solicitud de vacaciones pendiente.
        // Retorna true si existe una solicitud pendiente, de lo contrario, false.
        // Documentado por: Olman Martinez
        public bool ExisteSolicitudPendiente(int idEmpleado)
        {
            return nsolicitud.ExisteSolicitudPendiente(idEmpleado);
        }


        // Muestra las solicitudes de vacaciones aprobadas en un mes específico.
        // Retorna un DataTable con las solicitudes aprobadas del mes seleccionado.
        // Documentado por: Olman Martinez
        public DataTable MostrarAprobs(int mesSeleccionado)
        {
            return nsolicitud.MostrarAprobs(mesSeleccionado);
        }

        // Obtiene las fechas de inicio y finalización de una solicitud específica.
        // Retorna una tupla con las fechas correspondientes.
        // Documentado por: Olman Martinez
        public (DateTime fechaInicio, DateTime fechaFinal) ObtenerFechasSolicitud(int idSolicitud)
        {
            return nsolicitud.ObtenerFechasSolicitud(idSolicitud);
        }

        // Edita una solicitud de vacaciones actualizando sus fechas de inicio y fin.
        // Retorna la cantidad de registros afectados en la base de datos.
        // Documentado por: Olman Martinez
        public int EditarSolicitud(int idSolicitud, DateTime fechaInicio, DateTime fechaFinal)
        {
            return nsolicitud.EditarSolicitud(idSolicitud, fechaInicio, fechaFinal);
        }

    }
}
