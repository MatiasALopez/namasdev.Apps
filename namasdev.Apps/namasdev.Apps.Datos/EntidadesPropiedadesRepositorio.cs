using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using namasdev.Core.Types;
using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesPropiedadesRepositorio : IRepositorio<EntidadPropiedad, Guid>
    {
        IEnumerable<EntidadPropiedad> ObtenerPorEntidad(Guid entidadId, string busqueda = null, OrdenYPaginacionParametros op = null, bool cargarDatosAdicionales = false);
        EntidadPropiedad Obtener(Guid entidadPropiedadId, bool cargarDatosAdicionales = false);
        IEnumerable<PropiedadTipo> ObtenerTipos();
    }

    public class EntidadesPropiedadesRepositorio : Repositorio<SqlContext, EntidadPropiedad, Guid>, IEntidadesPropiedadesRepositorio
    {
        public IEnumerable<EntidadPropiedad> ObtenerPorEntidad(
            Guid entidadId,
            string busqueda = null,
            OrdenYPaginacionParametros op = null, 
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesPropiedades
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .Where(ep => ep.EntidadId == entidadId && !ep.Borrado)
                    .WhereIf(ep => ep.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(EntidadPropiedad.Orden))
                    .ToList();
            }
        }

        public EntidadPropiedad Obtener(
            Guid entidadPropiedadId, 
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesPropiedades
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .FirstOrDefault(ep => ep.Id == entidadPropiedadId && !ep.Borrado);
            }
        }

        public IEnumerable<PropiedadTipo> ObtenerTipos()
        {
            using (var ctx = new SqlContext())
            {
                return ctx.PropiedadTipos.ToList();
            }
        }

        private IEnumerable<Expression<Func<EntidadPropiedad, object>>> CrearPathsDatosAdicionales()
        {
            return new Expression<Func<EntidadPropiedad, object>>[]
            {
                ep => ep.Entidad,
                ep => ep.Tipo,
            };
        }
    }
}
