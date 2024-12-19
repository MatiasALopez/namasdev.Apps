using System;
using System.Collections.Generic;

using namasdev.Core.Entity;
using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Datos;

namespace namasdev.Apps.Negocio
{
    public class UsuariosNegocio : IUsuariosNegocio
    {
        private IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosNegocio(IUsuariosRepositorio usuariosRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(usuariosRepositorio, nameof(usuariosRepositorio));

            _usuariosRepositorio = usuariosRepositorio;
        }

        public Usuario Agregar(string usuarioId, string nombres, string apellidos, string email, string usuarioLogueadoId)
        {
            DateTime fechaHora = DateTime.Now;

            var usuario = new Usuario
            {
                Id = usuarioId,
                Nombres = nombres,
                Apellidos = apellidos,
                Email = email,
            };
            usuario.EstablecerDatosCreado(usuarioLogueadoId, fechaHora);
            usuario.EstablecerDatosModificacion(usuarioLogueadoId, fechaHora);

            ValidarDatos(usuario);

            _usuariosRepositorio.Agregar(usuario);

            return usuario;
        }

        public void Actualizar(Usuario usuario, string usuarioLogueadoId)
        {
            usuario.EstablecerDatosModificacion(usuarioLogueadoId, DateTime.Now);

            ValidarDatos(usuario);

            _usuariosRepositorio.Actualizar(usuario);
        }

        public void MarcarComoBorrado(string usuarioId, string usuarioLogueadoId)
        {
            var usuario = new Usuario { Id = usuarioId };
            usuario.EstablecerDatosBorrado(usuarioLogueadoId, DateTime.Now);
            _usuariosRepositorio.ActualizarDatosBorrado(usuario);
        }

        public void DesmarcarComoBorrado(string usuarioId)
        {
            _usuariosRepositorio.ActualizarDatosBorrado(new Usuario { Id = usuarioId });
        }

        public void ValidarDatos(Usuario usuario)
        {
            var errores = new List<string>();

            Validador.ValidarEmailYAgregarAListaErrores(usuario.Email, Entidades.Metadata.Usuario.EMAIL_DISPLAY_NAME, requerido: true, errores);
            Validador.ValidarStringYAgregarAListaErrores(usuario.Nombres, Entidades.Metadata.Usuario.NOMBRES_DISPLAY_NAME, requerido: true, errores,
                tamañoMaximo: Entidades.Metadata.Usuario.NOMBRES_TAMAÑO_MAXIMO);
            Validador.ValidarStringYAgregarAListaErrores(usuario.Apellidos, Entidades.Metadata.Usuario.APELLIDOS_DISPLAY_NAME, requerido: true, errores,
                tamañoMaximo: Entidades.Metadata.Usuario.APELLIDOS_TAMAÑO_MAXIMO);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
