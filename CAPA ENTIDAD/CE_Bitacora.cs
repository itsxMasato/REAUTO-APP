using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    // Representa la estructura de la bitácora, donde se registran las acciones realizadas por los usuarios.
    // Documentado por: Olman Martinez
    public class CE_Bitacora
    {
        // Identificador único de la bitácora.
        public int ID_BITACORA {  get; set; }
        // Fecha en la que se registró la acción en la bitácora.
        public string FECHA { get; set; }
        // Identificador del usuario que realizó la acción.
        public int ID_USUARIO { get; set; }
        // Descripción de la acción realizada por el usuario.
        public string ACCION { get; set; }
    }
}
