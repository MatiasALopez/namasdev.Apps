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
    public interface IEntidadesPropiedadesRepositorio : IRepositorio<EntidadPropiedad, Guid>
    {
        IEnumerable<EntidadPropiedad> ObtenerLista(Guid entidadId, string busqueda = null, OrdenYPaginacionParametros op = null, bool cargarDatosAdicionales = false);
        EntidadPropiedad Obtener(Guid entidadPropiedadId, bool cargarDatosAdicionales = false);
        IEnumerable<PropiedadTipo> ObtenerTipos();
    }

    public class EntidadesPropiedadesRepositorio : Repositorio<SqlContext, EntidadPropiedad, Guid>, IEntidadesPropiedadesRepositorio
    {
        public IEnumerable<EntidadPropiedad> ObtenerLista(
            Guid entidadId,
            string busqueda = null,
            OrdenYPaginacionParametros op = null, 
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesPropiedades
                    .IncludeIf(ep => ep.Entidad, cargarDatosAdicionales)
                    .IncludeIf(ep => ep.Tipo, cargarDatosAdicionales)
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
                    .IncludeIf(ep => ep.Entidad, cargarDatosAdicionales)
                    .IncludeIf(ep => ep.Tipo, cargarDatosAdicionales)
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
    }
}
