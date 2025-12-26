using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using namasdev.Core.Types;
using namasdev.Data;
using namasdev.Data.Entity;

using namasdev.Apps.Entidades;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesPropiedadesRepositorio : IRepositorio<EntidadPropiedad, Guid>
    {
        IEnumerable<EntidadPropiedad> ObtenerPorEntidad(Guid entidadId, string busqueda = null, OrdenYPaginacionParametros op = null, bool cargarDatosAdicionales = false);
        EntidadPropiedad Obtener(Guid entidadPropiedadId, bool cargarDatosAdicionales = false);
        short ObtenerProximoOrdenDisponible(Guid entidadId);
        IEnumerable<PropiedadTipo> ObtenerTipos();
        void ActualizarOrdenes(IEnumerable<EntidadPropiedad> propiedades);
    }

    public class EntidadesPropiedadesRepositorio : Repositorio<SqlContext, EntidadPropiedad, Guid>, IEntidadesPropiedadesRepositorio
    {
        public IEnumerable<EntidadPropiedad> ObtenerPorEntidad(
            Guid entidadId,
            string busqueda = null,
            OrdenYPaginacionParametros op = null,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = CrearContext())
            {
                return ctx.EntidadesPropiedades
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .Where(ep => ep.EntidadId == entidadId)
                    .WhereIf(ep => ep.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(EntidadPropiedad.Orden))
                    .ToList();
            }
        }

        public EntidadPropiedad Obtener(
            Guid entidadPropiedadId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = CrearContext())
            {
                return ctx.EntidadesPropiedades
                    .IncludeMultipleIf(CrearPathsDatosAdicionales(), cargarDatosAdicionales)
                    .FirstOrDefault(ep => ep.Id == entidadPropiedadId);
            }
        }

        public short ObtenerProximoOrdenDisponible(Guid entidadId)
        {
            using (var ctx = CrearContext())
            {
                var orden = ctx.EntidadesPropiedades
                    .Where(ep =>
                        ep.EntidadId == entidadId
                        && ep.PropiedadCategoriaId == PropiedadCategorias.GENERAL)
                    .Max(ep => (short?)ep.Orden) ?? 0;

                return (short)(orden + 1);
            }
        }

        public IEnumerable<PropiedadTipo> ObtenerTipos()
        {
            using (var ctx = CrearContext())
            {
                return ctx.PropiedadTipos.ToList();
            }
        }

        public void ActualizarOrdenes(IEnumerable<EntidadPropiedad> propiedades)
        {
            ActualizarPropiedades(
                propiedades,
                propiedades: new string[]
                {
                    nameof(EntidadPropiedad.Orden)
                });
        }

        private IEnumerable<Expression<Func<EntidadPropiedad, object>>> CrearPathsDatosAdicionales()
        {
            return new Expression<Func<EntidadPropiedad, object>>[]
            {
                ep => ep.Entidad.Claves,
                ep => ep.Tipo,
            };
        }
    }
}
