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
    public interface IAplicacionesVersionesRepositorio : IRepositorio<AplicacionVersion, Guid>
    {
        IEnumerable<AplicacionVersion> ObtenerLista(string busqueda = null, OrdenYPaginacionParametros op = null);
    }

    public class AplicacionesVersionesRepositorio : Repositorio<SqlContext,AplicacionVersion,Guid>, IAplicacionesVersionesRepositorio
    {
        public IEnumerable<AplicacionVersion> ObtenerLista(
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.AplicacionesVersiones
                    .Where(a => !a.Borrado)
                    .WhereIf(a => a.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(AplicacionVersion.Nombre))
                    .ToList();
            }
        }
    }
}
