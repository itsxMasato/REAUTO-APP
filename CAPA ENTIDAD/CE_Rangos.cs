using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    // Clase que representa los rangos en la aplicación.
    // Documentado por: Olman Martinez
    public class CE_Rangos
    {

        // Permite identificar de manera única cada rango en la base de datos.
        public int ID_RANGO { get; set; }

        // Este campo describe el nombre o título del rango.
        public string NOMBRE_RANGO { get; set; }
    }
}
