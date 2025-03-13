using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesAsociacionesRepositorio : IRepositorio<EntidadAsociacion, Guid>
    {
        IEnumerable<EntidadAsociacion> ObtenerLista(Guid entidadId, bool cargarDatosAdicionales = false);
        EntidadPropiedad Obtener(Guid id, bool cargarDatosAdicionales = false);
        IEnumerable<AsociacionMultiplicidad> ObtenerMultiplicidades();
        IEnumerable<AsociacionRegla> ObtenerReglas();
    }

    public class EntidadesAsociacionesRepositorio : Repositorio<SqlContext, EntidadAsociacion, Guid>, IEntidadesAsociacionesRepositorio
    {
        public IEnumerable<EntidadAsociacion> ObtenerLista(
            Guid entidadId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesAsociaciones
                    .IncludeIf(ea => ea.OrigenEntidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.OrigenPropiedad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.OrigenMultiplicidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.DestinoPropiedad.Entidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.DestinoMultiplicidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.DeleteRegla, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.UpdateRegla, cargarDatosAdicionales)
                    .Where(ep => ep.OrigenEntidadId == entidadId)
                    .ToList();
            }
        }

        public EntidadAsociacion Obtener(Guid id, 
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesAsociaciones
                    .IncludeIf(ea => ea.OrigenEntidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.OrigenPropiedad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.OrigenMultiplicidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.DestinoPropiedad.Entidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.DestinoMultiplicidad, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.DeleteRegla, cargarDatosAdicionales)
                    .IncludeIf(ea => ea.UpdateRegla, cargarDatosAdicionales)
                    .FirstOrDefault(ea => ea.Id == id);
            }
        }

        public IEnumerable<AsociacionMultiplicidad> ObtenerMultiplicidades()
        {
            using (var ctx = new SqlContext())
            {
                return ctx.AsociacionMultiplicidades.ToArray();
            }
        }

        public IEnumerable<AsociacionRegla> ObtenerReglas()
        {
            using (var ctx = new SqlContext())
            {
                return ctx.AsociacionReglas.ToArray();
            }
        }
    }
}
