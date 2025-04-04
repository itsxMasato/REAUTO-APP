using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    // Gestiona la lógica relacionada con los puestos dentro del sistema.
    // Documentado por: Olman Martinez
    public class CN_Puestos
    {
        // Instancia de la capa de datos para interactuar con la base de datos.
        // Documentado por: Olman Martinez
        CD_Puestos npuesto = new CD_Puestos();

        // Registra un nuevo puesto en la base de datos.
        // Retorna el identificador del puesto registrado.
        // Documentado por: Olman Martinez
        public int RegistrarPuesto(CE_Puestos epuesto)
        {
            return npuesto.RegistrarPuestos(epuesto);
        }

        public int IdPuesto() 
        {
            return npuesto.IdPuesto();
        }

        // Obtiene la lista de puestos registrados en la base de datos.
        // Retorna un DataTable con la información de los puestos.
        // Documentado por: Olman Martinez
        public DataTable MostrarPuesto()
        {
            npuesto.MostrarPuesto();
            return npuesto.MostrarPuesto();
        }

        // Modifica los datos de un puesto existente en la base de datos.
        // Retorna el número de filas afectadas en la base de datos.
        // Documentado por: Olman Martinez
        public int EditarPuesto(CE_Puestos puesto)
        {
            return npuesto.EditarPuesto(puesto);
        }

        // Verifica si un puesto con el ID especificado existe en la base de datos.
        // Retorna true si el puesto existe, de lo contrario, false.
        // Documentado por: Olman Martinez
        public bool ExisteIdPuesto(int idPuesto)
        {
            return npuesto.ExisteIdPuesto(idPuesto);
        }

        // Valida si el nombre de un puesto ya está registrado en la base de datos.
        // Retorna true si el nombre existe, de lo contrario, false.
        // Documentado por: Olman Martinez
        public bool ValidarNombrePuesto(string nombrePuesto)
        {
            return npuesto.ExisteNombrePuesto(nombrePuesto);
        }


        // Obtiene un reporte con información detallada sobre los puestos.
        // Retorna un DataTable con los datos del reporte.
        // Documentado por: Olman Martinez
        public DataTable MostrarReportePuesto()
        {
            return npuesto.MostrarReportePuesto();
        }

        // Obtiene una lista de puestos en formato de diccionario (ID - Nombre).
        // Retorna un diccionario con los puestos disponibles.
        // Documentado por: Olman Martinez
        public Dictionary<int, string> ObtenerPuestos()
        {
            return npuesto.ObtenerPuestos();
        }

        // Obtiene el sueldo asociado a un puesto específico según su ID.
        // Retorna el monto del sueldo correspondiente.
        // Documentado por: Olman Martinez
        public int ObtenerSueldoPorID(int idPuesto)
        {
            return npuesto.ObtenerSueldoPorID(idPuesto);
        }

    }
}
