using System;

using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesPropiedadesDefaultRepositorio : IRepositorio<EntidadPropiedadesDefault, Guid>
    {
    }

    public class EntidadesPropiedadesDefaultRepositorio : Repositorio<SqlContext, EntidadPropiedadesDefault, Guid>, IEntidadesPropiedadesDefaultRepositorio
    {
    }
}
