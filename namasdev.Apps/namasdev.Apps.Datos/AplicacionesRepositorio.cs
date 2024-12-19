using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Types;
using namasdev.Core.Validation;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public class AplicacionesRepositorio : Repositorio<SqlContext,Aplicacion,Guid>, IAplicacionesRepositorio, IDisposable
    {
        private SqlContext _sqlContext;

        public AplicacionesRepositorio(SqlContext sqlContext)
        {
            Validador.ValidarArgumentRequeridoYThrow(sqlContext, nameof(sqlContext));
            _sqlContext = sqlContext;
        }

        public IEnumerable<Aplicacion> ObtenerLista(
            string busqueda = null,
            OrdenYPaginacionParametros op = null)
        {
            return _sqlContext.Aplicaciones
                .Where(a => !a.Borrado)
                .WhereIf(a => a.Nombre.Contains(busqueda), !string.IsNullOrWhiteSpace(busqueda))
                .OrdenarYPaginar(op, ordenDefault: nameof(Aplicacion.Nombre))
                .ToList();
        }

        public void Dispose()
        {
            if (_sqlContext != null)
            {
                _sqlContext.Dispose();
            }
        }
    }
}
