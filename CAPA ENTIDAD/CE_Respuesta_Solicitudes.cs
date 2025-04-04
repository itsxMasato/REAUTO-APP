using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    // Contiene información sobre las respuestas a las solicitudes dentro del sistema.
    // Documentado por: Olman Martinez
    public class CE_Respuesta_Solicitudes
    {
        // Identificador único de la respuesta.
        // Permite identificar de manera única cada respuesta en la base de datos.
        public int ID_RESPUESTA { get; set; }
        // Identificador de la solicitud a la que se le está dando una respuesta.
        // Relaciona esta respuesta con la solicitud específica.
        public int ID_SOLICITUD_V { get; set; }
        // Respuesta proporcionada a la solicitud.
        // Almacena el contenido de la respuesta dada a la solicitud.
        public string RESPUESTA { get; set; }
    }
}
