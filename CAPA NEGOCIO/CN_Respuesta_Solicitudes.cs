using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Capa_Datos.CD_Respuesta_Solicitudes;

namespace Capa_Negocio
{
    // Maneja la lógica de negocio relacionada con las respuestas a solicitudes.
    // Documentado por: Olman Martinez
    public class CN_Respuesta_Solicitudes
    {
        // Instancia de la capa de datos para gestionar respuestas de solicitudes.
        // Documentado por: Olman Martinez
        CD_Respuesta_Solicitudes nrespuesta = new CD_Respuesta_Solicitudes();

        // Registra una nueva respuesta a una solicitud.
        // Retorna el ID de la respuesta registrada.
        // Documentado por: Olman Martinez
        public int RegistrarSolicitud(CE_Respuesta_Solicitudes erespuesta)
        {
            return nrespuesta.RegistrarRespuesta(erespuesta);
        }

        // Verifica si una solicitud específica existe en la base de datos.
        // Retorna true si la solicitud está registrada, de lo contrario, false.
        // Documentado por: Olman Martinez
        public bool ExisteSolicitud(int idSolicitud)
        {
            
            return nrespuesta.ExisteSolicitud(idSolicitud); // Si count > 0, la solicitud existe.
        }

        // Obtiene la lista de respuestas a solicitudes almacenadas en la base de datos.
        // Retorna un DataTable con la información de las respuestas.
        // Documentado por: Olman Martinez
        public DataTable MostrarSolicitudes()
        {
            return nrespuesta.MostrarSolicitudes();
        }

        // Valida si la fecha de una solicitud cumple con las condiciones requeridas.
        // Retorna true si la fecha es válida, de lo contrario, false.
        // Documentado por: Olman Martinez
        public bool ValidarFechaSolicitud(int idSolicitud)
        {
            return nrespuesta.ValidarFechaSolicitud(idSolicitud);
        }

        // Método de la capa de negocio que gestiona la actualización automática  
        // de solicitudes de vacaciones vencidas.  
        // Llama al método de la capa de datos que actualiza las solicitudes con fecha de inicio pasada.  
        // Documentado por: Olman Martinez
        public void ActualizarSolicitudesVencidas()
        {
            nrespuesta.ActualizarSolicitudesVencidas();
        }
    }
}
