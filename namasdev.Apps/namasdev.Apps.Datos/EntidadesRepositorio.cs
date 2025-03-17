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
        IEnumerable<Entidad> ObtenerLista(Guid aplicacionVersionId, string busqueda = null, OrdenYPaginacionParametros op = null);
        Entidad Obtener(Guid entidadId, bool cargarDatosAdicionales = false);
    }

    public class EntidadesRepositorio : Repositorio<SqlContext, Entidad, Guid>, IEntidadesRepositorio
    {
        public IEnumerable<Entidad> ObtenerLista(
            Guid aplicacionVersionId,
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Entidades
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
                return ctx.Entidades
                    .IncludeIf(e => e.AplicacionVersion.Aplicacion, cargarDatosAdicionales)
                    .IncludeIf(e => e.PropiedadesDefault.IDTipo, cargarDatosAdicionales)
                    .IncludeIf(e => e.Propiedades.Select(p => p.Tipo), cargarDatosAdicionales)
                    .IncludeIf(e => e.Claves.Select(c => c.Propiedad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.OrigenEntidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.OrigenPropiedad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.OrigenMultiplicidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.DestinoPropiedad.Entidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.DestinoMultiplicidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.DeleteRegla), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesOrigen.Select(a => a.UpdateRegla), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.OrigenEntidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.OrigenPropiedad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.OrigenMultiplicidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.DestinoPropiedad.Entidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.DestinoMultiplicidad), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.DeleteRegla), cargarDatosAdicionales)
                    .IncludeIf(e => e.AsociacionesDestino.Select(a => a.UpdateRegla), cargarDatosAdicionales)
                    .IncludeIf(e => e.Indices.Select(i => i.Propiedades.Select(p => p.Propiedad)), cargarDatosAdicionales)
                    .FirstOrDefault(e => e.Id == entidadId && !e.Borrado);
            }
        }
    }
}
