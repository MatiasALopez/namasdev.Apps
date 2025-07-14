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
    public interface IEntidadesChecksRepositorio : IRepositorio<EntidadCheck, Guid>
    {
        IEnumerable<EntidadCheck> ObtenerPorEntidad(Guid entidadId, bool cargarDatosAdicionales = false);
        EntidadCheck Obtener(Guid id, bool cargarDatosAdicionales = false);
    }

    public class EntidadesChecksRepositorio : Repositorio<SqlContext, EntidadCheck, Guid>, IEntidadesChecksRepositorio
    {
        public IEnumerable<EntidadCheck> ObtenerPorEntidad(
            Guid entidadId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesChecks
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .Where(ei => ei.EntidadId == entidadId)
                    .OrderBy(i => i.Nombre)
                    .ToList();
            }
        }

        public EntidadCheck Obtener(Guid id,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesChecks
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .FirstOrDefault(ei => ei.Id == id);
            }
        }

        private IEnumerable<Expression<Func<EntidadCheck, object>>> CrearPathsDatosAdicionales()
        {
            return new Expression<Func<EntidadCheck, object>>[]
            {
                ei => ei.Entidad
            };
        }
    }
}
