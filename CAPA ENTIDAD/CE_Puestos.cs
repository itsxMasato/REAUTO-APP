using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    // Clase que representa los puestos dentro de la aplicación.
    // Documentado por: Olman Martinez
    public class CE_Puestos
    {
        // Identificador único del puesto dentro del sistema.
        // Esto permite referenciar de manera única cada puesto en la base de datos o aplicación.
        public int ID_PUESTO { set; get; }
        // Nombre del puesto o cargo que se asigna al empleado.
        // Este campo permite identificar de manera clara el puesto dentro de la organización.
        public string NOMBRE_PUESTO { set; get; }
        // Descripción detallada de las funciones y responsabilidades asociadas al puesto.
        // Ayuda a entender el rol y las expectativas relacionadas con este puesto.
        public string DESCRIPCION_PUESTO { set; get; }
        // Sueldo asociado al puesto.
        // Indica la compensación económica que recibe el empleado por ocupar este puesto.
        public int SUELDO { set; get; }

        // Identificador de rango de salario relacionado con este puesto.
        // Este campo está relacionado con un rango de sueldo, facilitando la clasificación y administración salarial.
        public int ID_RANGO { set; get; }
    }
}
