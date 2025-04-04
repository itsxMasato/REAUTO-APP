using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    // Maneja la lógica de negocio relacionada con los rangos dentro del sistema.
    // Documentado por: Olman Martinez
    public class CN_Rangos
    {
        // Instancia de la capa de datos para interactuar con la base de datos.
        // Documentado por: Olman Martinez
        CD_Rangos nrangos = new CD_Rangos();

        // Obtiene la lista de rangos registrados en la base de datos.
        // Retorna un DataTable con la información de los rangos.
        // Documentado por: Olman Martinez
        public DataTable MostrarRangos()
        {
            DataTable rangos = nrangos.MostrarRangos();
            return rangos;
        }
    }
}
