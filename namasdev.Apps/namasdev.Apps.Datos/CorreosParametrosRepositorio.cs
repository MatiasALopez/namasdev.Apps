using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface ICorreosParametrosRepositorio : IRepositorioSoloLectura<CorreoParametros, short>
    {
    }

    public class CorreosParametrosRepositorio : RepositorioSoloLectura<SqlContext, CorreoParametros, short>, ICorreosParametrosRepositorio
    {
    }
}
