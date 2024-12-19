using System;
using namasdev.Data;
using namasdev.Apps.Entidades;
using namasdev.Core.Types;
using System.Collections.Generic;

namespace namasdev.Apps.Datos
{
    public interface IAplicacionesRepositorio : IRepositorio<Aplicacion, Guid>, IDisposable
    {
        IEnumerable<Aplicacion> ObtenerLista(string busqueda = null, OrdenYPaginacionParametros op = null);
    }
}
