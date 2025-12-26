using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Data;
using namasdev.Data.Entity;

using namasdev.Apps.Entidades;
using namasdev.Apps.Datos.Sql;

namespace namasdev.Apps.Datos
{
    public interface IIdiomasRepositorio : IRepositorio<Idioma, string>
    {
        IEnumerable<IdiomaArticulo> ObtenerArticulos(Guid aplicacionId);
    }

    public class IdiomasRepositorio : Repositorio<SqlContext, Idioma, string>, IIdiomasRepositorio
    {
        public IEnumerable<IdiomaArticulo> ObtenerArticulos(Guid aplicacionId)
        {
            using (var ctx = CrearContext())
            {
                var idiomaIdQuery = ctx.Aplicaciones
                    .Where(av => av.Id == aplicacionId)
                    .Select(av => av.IdiomaId);

                return ctx.IdiomasArticulos
                    .Where(a => idiomaIdQuery.Contains(a.IdiomaId))
                    .OrderBy(a => a.Id)
                    .ToList();
            }
        }
    }
}
