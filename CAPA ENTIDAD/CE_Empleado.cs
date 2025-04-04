using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    // Representa la información de un empleado dentro del sistema.
    // Documentado por: Olman Martinez
    public class CE_Empleado
    {
        // Identificador único del empleado.
        public int ID_EMPLEADO { get; set; }
        // Identificador relacionado con los datos generales del empleado.
        public int ID_DATOS {  get; set; }
        // Fecha en la que el empleado fue contratado.
        public String FECHA_CONTRATACION { get; set; }
        // Identificador de los datos adicionales del empleado solicitados.
        public int ID_DATOS_SOLI { get; set; }
        // Número de identidad del empleado.
        public string NO_IDENTIDAD { get; set; }
        // Registro Tributario Nacional del empleado.
        public string RTN { get; set; }
        // Nombre del empleado.
        public string NOMBRE { get; set; }
        // Apellido del empleado.
        public string APELLIDO { get; set; }
        // Fecha de nacimiento del empleado.
        public string FECHA_DE_NACIMIENTO { get; set; }
        // Formación académica del empleado.
        public string FORMACION_ACADEMICA { get; set; }
        // Experiencia laboral del empleado.
        public string EXPERIENCIA_LABORAL { get; set; }
        // Idiomas que domina el empleado.
        public string IDIOMAS { get; set; }
        // Cursos realizados por el empleado.
        public string CURSOS { get; set; }
        // Estado actual del empleado dentro de la empresa (activo/inactivo).
        public string ESTADO { get; set; }
    }
}
