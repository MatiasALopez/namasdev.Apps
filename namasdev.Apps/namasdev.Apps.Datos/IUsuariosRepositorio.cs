using System.Collections.Generic;

using namasdev.Core.Types;
using namasdev.Data;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IUsuariosRepositorio : IRepositorio<Usuario, string>
    {
        List<string> ObtenerEmailsPorRol(string rolNombre);
        string ObtenerNombreCompletoPorId(string usuarioId);
        Usuario Obtener(string usuarioId, bool cargarRoles = false);
        bool ExisteBorradoPorEmail(string email, out string usuarioId);
        List<AspNetRole> ObtenerRoles();
        List<Usuario> ObtenerListado(string busqueda = null, string rol = null, bool cargarRoles = false, OrdenYPaginacionParametros op = null);
        string ObtenerRolDeUsuario(string usuarioId);

    }
}
