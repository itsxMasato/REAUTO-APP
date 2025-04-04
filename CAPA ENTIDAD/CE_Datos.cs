using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    // Representa los datos del administrador y empleado.
    // Documentado por: Olman Martinez
    public class CE_Datos
    {
        // Identificador único del administrador.
        public int ID_ADMIN { get; set; }
        // Nombre del administrador.
        public string NOMBRE_ADMIN { get; set; }
        // Apellido del administrador.
        public string APELLIDO_ADMIN { get; set; }
        // Identificador del empleado asociado al administrador.
        public string ID_EMPLEADO { get; set; }

    }
}
