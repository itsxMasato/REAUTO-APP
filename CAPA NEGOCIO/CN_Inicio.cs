using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    // Proporciona métodos para obtener información que se muestra en la pantalla de inicio.
    // Documentado por: Olman Martinez
    public class CN_Inicio
    {
        // Instancia de la capa de datos para acceder a la información de la base de datos.
        // Documentado por: Olman Martinez
        CD_Inicio ninicio = new CD_Inicio();

        // Obtiene los datos principales que se muestran en la pantalla de inicio.
        // Documentado por: Olman Martinez
        public DataTable MostrarInicio()
        {
            ninicio.MostrarInicio();
            return ninicio.MostrarInicio();
        }

        // Obtiene la lista de solicitudes de vacaciones registradas.
        // Documentado por: Olman Martinez
        public DataTable MostrarSolicitudVacacioneS()
        {
            ninicio.MostrarSolicitudVacaciones();
            return ninicio.MostrarSolicitudVacaciones();
        }

        // Obtiene la información de las vacaciones registradas en el sistema.
        // Documentado por: Olman Martinez
        public DataTable MostrarVacacciones()
        {
            ninicio.MostrarVacaciones();
            return ninicio.MostrarVacaciones();
        }

        // Obtiene la lista de empleados registrados en el sistema.
        // Documentado por: Olman Martinez
        public DataTable MostrarEmpleados()
        {
            ninicio.MostrarEmpleados();
            return ninicio.MostrarEmpleados();
        }
    }
}
