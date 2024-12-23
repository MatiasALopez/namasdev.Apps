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
        IEnumerable<AplicacionVersion> ObtenerLista(Guid aplicacionId, string busqueda = null, OrdenYPaginacionParametros op = null);
        AplicacionVersion Obtener(Guid aplicacionVersionId, bool cargarDatosAdicionales = false);
    }

    public class AplicacionesVersionesRepositorio : Repositorio<SqlContext,AplicacionVersion,Guid>, IAplicacionesVersionesRepositorio
    {
        public IEnumerable<AplicacionVersion> ObtenerLista(
            Guid aplicacionId,
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.AplicacionesVersiones
                    .Where(av => av.AplicacionId == aplicacionId && !av.Borrado)
                    .WhereIf(av => av.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(AplicacionVersion.Nombre))
                    .ToList();
            }
        }

        public AplicacionVersion Obtener(Guid aplicacionVersionId,
            bool cargarDatosAdicionales = false)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.AplicacionesVersiones
                    .IncludeIf(av => av.Aplicacion, cargarDatosAdicionales)
                    .FirstOrDefault(av => av.Id == aplicacionVersionId && !av.Borrado);
            }
        }
    }
}
