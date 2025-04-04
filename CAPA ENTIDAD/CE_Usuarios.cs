using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capa_Entidad
{
    // Contiene la estructura para los usuarios dentro del sistema, incluyendo sus datos de acceso y bitácora de actividades.
    // Documentado por: Olman Martinez
    public class CE_Usuarios
    {
        // Identificador único del usuario.
        // Se utiliza para hacer referencia única al usuario dentro del sistema.
        public int ID_USUARIO { get; set; }
        // Nombre de usuario utilizado para iniciar sesión.
        // Este campo permite identificar al usuario que accede al sistema.
        public string USUARIO { get; set; }
        // Contraseña asociada al usuario para la autenticación.
        // Se utiliza para verificar la identidad del usuario al iniciar sesión.
        public string CONTRASENA { get; set; }
        // Identificador del empleado al que está asociado el usuario.
        // Relaciona el usuario con los datos del empleado en el sistema.
        public int ID_EMPLEADO { get; set; }
        // Identificador del puesto del usuario.
        // Indica el puesto o cargo del usuario dentro de la organización.
        public int ID_PUESTO { get; set; }
        // Estado del usuario (activo/inactivo).
        // Permite controlar si el usuario tiene acceso al sistema o está deshabilitado.
        public string ESTADO { get; set; }
        // Identificador único de la bitácora asociada al usuario.
        // Permite registrar y hacer un seguimiento de las acciones realizadas por el usuario en el sistema.
        public int ID_BITACORA { get; set; }
        // Fecha en la que se registró la acción en la bitácora.
        // Sirve para tener un registro de cuándo ocurrió la actividad.
        public string FECHA { get; set; }
        // Descripción de la acción realizada por el usuario.
        // Proporciona detalles sobre lo que hizo el usuario en el sistema en un momento dado.
        public string ACCION { get; set; }

    }
}
