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
        IEnumerable<AplicacionVersion> ObtenerPorAplicacion(Guid aplicacionId, string busqueda = null, OrdenYPaginacionParametros op = null);
        void Clonar(Guid aplicacionVersionIdOrigen, Guid aplicacionVersionIdDestino, string usuarioId, DateTime fechaHora);
    }

    public class AplicacionesVersionesRepositorio : Repositorio<SqlContext,AplicacionVersion,Guid>, IAplicacionesVersionesRepositorio
    {
        public IEnumerable<AplicacionVersion> ObtenerPorAplicacion(
            Guid aplicacionId,
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = CrearContext())
            {
                return ctx.AplicacionesVersiones
                    .Where(av => av.AplicacionId == aplicacionId && !av.Borrado)
                    .WhereIf(av => av.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                    .OrdenarYPaginar(op, ordenDefault: nameof(AplicacionVersion.Nombre))
                    .ToList();
            }
        }

        public void Clonar(Guid aplicacionVersionIdOrigen, Guid aplicacionVersionIdDestino, string usuarioId, DateTime fechaHora)
        {
            using (var ctx = CrearContext())
            {
                ctx.uspClonarAplicacionVersion(aplicacionVersionIdOrigen, aplicacionVersionIdDestino, usuarioId, fechaHora);
            }
        }
    }
}
