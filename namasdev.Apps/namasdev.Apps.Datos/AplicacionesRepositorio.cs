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
    public interface IAplicacionesRepositorio : IRepositorio<Aplicacion, Guid>
    {
        IEnumerable<Aplicacion> ObtenerLista(string busqueda = null, OrdenYPaginacionParametros op = null);
    }

    public class AplicacionesRepositorio : Repositorio<SqlContext,Aplicacion,Guid>, IAplicacionesRepositorio
    {
        public IEnumerable<Aplicacion> ObtenerLista(
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Aplicaciones
                    .Where(a => !a.Borrado)
                    .WhereIf(a => a.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(Aplicacion.Nombre))
                    .ToList();
            }
        }
    }
}
