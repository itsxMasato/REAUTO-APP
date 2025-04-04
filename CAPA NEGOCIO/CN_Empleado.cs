using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Datos;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using static Capa_Negocio.CN_Empleado;

namespace Capa_Negocio
{
    // Proporciona métodos para gestionar los empleados dentro del sistema.
    // Documentado por: Olman Martinez
    public class CN_Empleado
    {
        public class CN_Empleados 
        {
            // Instancia de la capa de datos para interactuar con la base de datos.
            // Documentado por: Olman Martinez
            CD_EMPLEADO nempleados = new CD_EMPLEADO();


            // Registra un nuevo solicitante en la base de datos.
            // Retorna el identificador del solicitante registrado.
            //Documentado por: Kenny Arias
            public int RegistrarSolicitante(CE_Empleado eempleado)
            {
                return nempleados.RegistrarSolicitante(eempleado);
            }

            // Registra un nuevo solicitante en la base de datos.
            // Retorna el identificador del solicitante registrado.
            //Documentado por: Kenny Arias
            public int EditarSolicitante(CE_Empleado eempleado)
            {
                return nempleados.EditarSolicitante(eempleado);
            }


            // Obtiene la lista de solicitantes registrados.
            // Retorna un DataTable con la información de los solicitantes.
            // Documentado por: Olman Martinez
            public DataTable MostrarSolicitante()
            {
                nempleados.MostrarSolicitantes();
                return nempleados.MostrarSolicitantes();
            }

            // Verifica si un solicitante ya está registrado en la base de datos.
            // Retorna true si el solicitante existe, de lo contrario, false.
            // Documentado por: Olman Martinez
            public bool VerificarExistenciaSoli(int idSolicitante, string no_identidad)
            {
                return nempleados.VerificarExistenciaSoli(idSolicitante, no_identidad);
            }

            // Registra un nuevo empleado en la base de datos.
            // Retorna el identificador del empleado registrado.
            // Documentado por: Olman Martinez
            public int RegistrarEmpleado(CE_Empleado eempleado)
            {
                return nempleados.RegistrarEmpleados(eempleado);
            }


            public int EditarEmpleado(CE_Empleado eempleado)
            {
                return nempleados.EditarEmpleado(eempleado);
            }

            // Obtiene la lista de empleados registrados en la base de datos.
            // Retorna un DataTable con la información de los empleados.
            // Documentado por: Olman Martinez
            public DataTable MostrarEmpleado()
            {
                nempleados.MostrarEmpleados();
                return nempleados.MostrarEmpleados();
            }

            public int IdDatosSoli() 
            { 
                return nempleados.IdDatosSoli();
            }

            // Filtra los empleados según su estado (activo/inactivo).
            // Retorna un DataTable con los empleados que coinciden con el estado especificado.
            // Documentado por: Olman Martinez
            public DataTable FiltrarEmpleados(string estado)
            {
                return nempleados.FiltrarEmpleados(estado);
            }

            // Método que invoca la capa de datos para obtener los solicitantes filtrados.
            // Llama al método FiltrarSolicitantes, pasando el ID del solicitante si es proporcionado.
            // Documentado por: Miguel Flores
            public DataTable MostrarSolicitanteFiltrado(int? idSolicitante = null)
            {
                return new CD_EMPLEADO().FiltrarSolicitantes(idSolicitante); // CD_Empleados es la clase de la capa de datos
            }

        }
    }
}
