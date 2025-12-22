using namasdev.Data;
using namasdev.Data.Entity;

using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IIdiomasRepositorio : IRepositorio<Idioma, string>
    {
    }

    public class IdiomasRepositorio : Repositorio<SqlContext, Idioma, string>, IIdiomasRepositorio
    {
    }
}
