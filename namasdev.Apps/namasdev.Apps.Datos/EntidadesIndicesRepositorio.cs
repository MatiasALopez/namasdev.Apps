using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using namasdev.Data;
using namasdev.Data.Entity;

using namasdev.Apps.Entidades;
using namasdev.Apps.Datos.Sql;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesIndicesRepositorio :IRepositorio<EntidadIndice, Guid>
    {
        IEnumerable<EntidadIndice> ObtenerPorEntidad(Guid entidadId, bool cargarDatosAdicionales = false);
        EntidadIndice Obtener(Guid id, bool cargarDatosAdicionales = false);
    }

    public class EntidadesIndicesRepositorio : Repositorio<SqlContext,EntidadIndice, Guid>, IEntidadesIndicesRepositorio
    {
        public IEnumerable<EntidadIndice> ObtenerPorEntidad(
            Guid entidadId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesIndices
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .Where(ei => ei.EntidadId == entidadId)
                    .OrderBy(i => i.EsUnique ? 0 : 1)
                    .ThenBy(i => i.Propiedades.Min(p => p.Propiedad.Orden))
                    .ToList();
            }
        }

        public EntidadIndice Obtener(Guid id,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesIndices
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .FirstOrDefault(ei => ei.Id == id);
            }
        }

        private IEnumerable<Expression<Func<EntidadIndice, object>>> CrearPathsDatosAdicionales()
        {
            return new Expression<Func<EntidadIndice, object>>[]
            {
                ei => ei.Entidad,
                ei => ei.Propiedades.Select(p => p.Propiedad.Tipo),
            };
        }
    }
}
