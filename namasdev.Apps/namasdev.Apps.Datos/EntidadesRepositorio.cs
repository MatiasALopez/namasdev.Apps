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
    public interface IEntidadesRepositorio : IRepositorio<Entidad, Guid>
    {
        IEnumerable<Entidad> ObtenerPorVersion(Guid aplicacionVersionId, bool cargarDatosAdicionales = false, string busqueda = null, OrdenYPaginacionParametros op = null);
        Entidad Obtener(Guid entidadId, bool cargarDatosAdicionales = false);
    }

    public class EntidadesRepositorio : Repositorio<SqlContext, Entidad, Guid>, IEntidadesRepositorio
    {
        public IEnumerable<Entidad> ObtenerPorVersion(
            Guid aplicacionVersionId,
            bool cargarDatosAdicionales = false,
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Entidades
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .Where(e => e.AplicacionVersionId == aplicacionVersionId && !e.Borrado)
                    .WhereIf(e => e.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(Entidad.Nombre))
                    .ToList();
            }
        }

        public Entidad Obtener(Guid entidadId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                var entidad = ctx.Entidades
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .FirstOrDefault(e => e.Id == entidadId && !e.Borrado);

                entidad.EliminarPropiedadesBorradas();

                return entidad;
            }
        }

        private IEnumerable<Expression<Func<Entidad, object>>> CrearPathsDatosAdicionales()
        {
            return new Expression<Func<Entidad, object>>[]
            {
                e => e.AplicacionVersion.Aplicacion,
                e => e.PropiedadesDefault.IDTipo,
                e => e.Propiedades.Select(p => p.Tipo),
                e => e.Claves.Select(c => c.Propiedad),
                e => e.AsociacionesOrigen.Select(a => a.OrigenEntidad),
                e => e.AsociacionesOrigen.Select(a => a.OrigenPropiedad),
                e => e.AsociacionesOrigen.Select(a => a.OrigenMultiplicidad),
                e => e.AsociacionesOrigen.Select(a => a.DestinoPropiedad.Entidad),
                e => e.AsociacionesOrigen.Select(a => a.DestinoMultiplicidad),
                e => e.AsociacionesOrigen.Select(a => a.DeleteRegla),
                e => e.AsociacionesOrigen.Select(a => a.UpdateRegla),
                e => e.AsociacionesDestino.Select(a => a.OrigenEntidad),
                e => e.AsociacionesDestino.Select(a => a.OrigenPropiedad),
                e => e.AsociacionesDestino.Select(a => a.OrigenMultiplicidad),
                e => e.AsociacionesDestino.Select(a => a.DestinoPropiedad.Entidad),
                e => e.AsociacionesDestino.Select(a => a.DestinoMultiplicidad),
                e => e.AsociacionesDestino.Select(a => a.DeleteRegla),
                e => e.AsociacionesDestino.Select(a => a.UpdateRegla),
                e => e.Indices.Select(i => i.Propiedades.Select(p => p.Propiedad)),
            };
        }
    }
}
