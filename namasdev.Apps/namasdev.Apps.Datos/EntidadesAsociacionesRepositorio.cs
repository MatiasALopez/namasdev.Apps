using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesAsociacionesRepositorio : IRepositorio<EntidadAsociacion, Guid>
    {
        IEnumerable<EntidadAsociacion> ObtenerPorEntidad(Guid entidadId, bool cargarDatosAdicionales = false);
        EntidadAsociacion Obtener(Guid id, bool cargarDatosAdicionales = false);
        bool ExistenPorPropiedadOrigen(Guid origenEntidadPropiedadId);
        IEnumerable<AsociacionMultiplicidad> ObtenerMultiplicidades();
        IEnumerable<AsociacionRegla> ObtenerReglas();
    }

    public class EntidadesAsociacionesRepositorio : Repositorio<SqlContext, EntidadAsociacion, Guid>, IEntidadesAsociacionesRepositorio
    {
        public IEnumerable<EntidadAsociacion> ObtenerPorEntidad(
            Guid entidadId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = CrearContext())
            {
                return ctx.EntidadesAsociaciones
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .Where(ep => ep.OrigenEntidadId == entidadId)
                    .OrderBy(ao => ao.OrigenPropiedad.Orden)
                    .ToList();
            }
        }

        public EntidadAsociacion Obtener(Guid id, 
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = CrearContext())
            {
                return ctx.EntidadesAsociaciones
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .FirstOrDefault(ea => ea.Id == id);
            }
        }

        public bool ExistenPorPropiedadOrigen(Guid origenEntidadPropiedadId)
        {
            using (var ctx = CrearContext())
            {
                return ctx.EntidadesAsociaciones
                    .Any(ea => ea.OrigenEntidadPropiedadId == origenEntidadPropiedadId);
            }
        }

        public IEnumerable<AsociacionMultiplicidad> ObtenerMultiplicidades()
        {
            using (var ctx = CrearContext())
            {
                return ctx.AsociacionMultiplicidades.ToArray();
            }
        }

        public IEnumerable<AsociacionRegla> ObtenerReglas()
        {
            using (var ctx = CrearContext())
            {
                return ctx.AsociacionReglas.ToArray();
            }
        }

        private IEnumerable<Expression<Func<EntidadAsociacion, object>>> CrearPathsDatosAdicionales()
        {
            return new Expression<Func<EntidadAsociacion, object>>[]
            {
                ea => ea.OrigenEntidad,
                ea => ea.OrigenPropiedad,
                ea => ea.OrigenMultiplicidad,
                ea => ea.DestinoPropiedad.Entidad,
                ea => ea.DestinoMultiplicidad,
                ea => ea.DeleteRegla,
                ea => ea.UpdateRegla,
            };
        }
    }
}
