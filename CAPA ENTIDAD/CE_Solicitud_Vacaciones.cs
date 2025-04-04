using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Entidad
{
    // Define la estructura para las solicitudes de vacaciones dentro del sistema.
    // Documentado por: Olman Martinez
    public class CE_Solicitud_Vacaciones
    {
        // Identificador único de la solicitud de vacaciones.
        // Se utiliza para hacer referencia única a cada solicitud en la base de datos.
        public int ID_SOLICITUD_V { get; set; }
        // Número de días solicitados para las vacaciones.
        // Indica la cantidad de días que el empleado solicita para descansar.
        public int DIAS_SOLICITADOS { get; set; }
        // Fecha de inicio de las vacaciones solicitadas.
        // Representa el primer día de descanso solicitado por el empleado.
        public DateTime FECHA_INICIO { get; set; }
        // Fecha final de las vacaciones solicitadas.
        // Especifica el último día de descanso solicitado por el empleado.
        public DateTime FECHA_FINAL { get; set; }
        // Identificador del empleado que realiza la solicitud de vacaciones.
        // Relaciona la solicitud con un empleado específico en la base de datos.
        public int ID_EMPLEADO { get; set; }
        // Estado de la solicitud de vacaciones (aprobada, pendiente, rechazada).
        // Ayuda a determinar en qué etapa se encuentra la solicitud dentro del proceso.
        public string ESTADO { get; set; }

    }
}
