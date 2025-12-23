using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Types;
using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesRepositorio : IRepositorio<Entidad, Guid>
    {
        IEnumerable<Entidad> ObtenerPorVersion(Guid aplicacionVersionId, ICargaPropiedades<Entidad> cargarPropiedades = null, string busqueda = null, OrdenYPaginacionParametros op = null);
        IEnumerable<BajaTipo> ObtenerBajaTipos();
        IEnumerable<IdiomaArticulo> ObtenerArticulos();
    }

    public class EntidadesRepositorio : Repositorio<SqlContext, Entidad, Guid>, IEntidadesRepositorio
    {
        public IEnumerable<Entidad> ObtenerPorVersion(
            Guid aplicacionVersionId,
            ICargaPropiedades<Entidad> cargarPropiedades = null,
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Entidades
                    .IncludeMultiple(cargarPropiedades)
                    .Where(e => e.AplicacionVersionId == aplicacionVersionId && !e.Borrado)
                    .WhereIf(e => e.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(Entidad.Nombre))
                    .ToList();
            }
        }

        public IEnumerable<BajaTipo> ObtenerBajaTipos()
        {
            using (var ctx = new SqlContext())
            {
                return ctx.BajaTipos
                    .OrderBy(bt => bt.Id)
                    .ToList();
            }
        }

        public IEnumerable<IdiomaArticulo> ObtenerArticulos()
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Articulos
                    .OrderBy(a => a.Id)
                    .ToList();
            }
        }
    }
}
