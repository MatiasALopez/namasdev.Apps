using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Data;
using namasdev.Data.Entity;

using namasdev.Apps.Entidades;
using namasdev.Apps.Datos.Sql;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesIndicesPropiedadesRepositorio :IRepositorio<EntidadIndicePropiedad, Guid>
    {
        IEnumerable<EntidadIndicePropiedad> ObtenerPorIndice(Guid entidadIndiceId);
    }

    public class EntidadesIndicesPropiedadesRepositorio : Repositorio<SqlContext,EntidadIndicePropiedad, Guid>, IEntidadesIndicesPropiedadesRepositorio
    {
        public IEnumerable<EntidadIndicePropiedad> ObtenerPorIndice(Guid entidadIndiceId)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.EntidadesIndicesPropiedades
                    .Where(e => e.EntidadIndiceId == entidadIndiceId)
                    .ToArray();
            }
        }
    }
}
