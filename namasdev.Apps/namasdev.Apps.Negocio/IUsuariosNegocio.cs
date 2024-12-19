using namasdev.Apps.Entidades;
using System;

namespace namasdev.Apps.Negocio
{
    public interface IUsuariosNegocio
    {
        void Actualizar(Usuario usuario, string usuarioLogueadoId);
        Usuario Agregar(string usuarioId, string nombres, string apellidos, string email, string usuarioLogueadoId);
        void DesmarcarComoBorrado(string usuarioId);
        void MarcarComoBorrado(string usuarioId, string usuarioLogueadoId);
        void ValidarDatos(Usuario usuario);
    }
}