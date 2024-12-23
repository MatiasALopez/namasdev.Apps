using System;
using System.Collections.Generic;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Core.Entity;
using namasdev.Core.Validation;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesNegocio
    {
        Entidad Agregar(Guid aplicacionVersionId, string nombre, string usuarioId);
        void Actualizar(Entidad aplicacion, string usuarioId);
        void MarcarComoBorrado(Guid entidadId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid entidadId);
    }

    public class EntidadesNegocio : IEntidadesNegocio
    {
        private IEntidadesRepositorio _entidadesRepositorio;

        public EntidadesNegocio(IEntidadesRepositorio entidadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));

            _entidadesRepositorio = entidadesRepositorio;
        }

        public Entidad Agregar(Guid aplicacionVersionId, string nombre, string usuarioId)
        {
            DateTime fechaHora = DateTime.Now;

            var entidad = new Entidad
            {
                Id = Guid.NewGuid(),
                AplicacionVersionId = aplicacionVersionId,
                Nombre = nombre
            };
            entidad.EstablecerDatosCreado(usuarioId, fechaHora);
            entidad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(entidad);

            _entidadesRepositorio.Agregar(entidad);

            return entidad;
        }

        public void Actualizar(Entidad entidad, string usuarioId)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            DateTime fechaHora = DateTime.Now;

            entidad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(entidad);

            _entidadesRepositorio.Actualizar(entidad);
        }

        public void MarcarComoBorrado(Guid entidadId, string usuarioLogueadoId)
        {
            var entidad = new Entidad 
            {
                Id = entidadId
            };
            entidad.EstablecerDatosBorrado(usuarioLogueadoId, DateTime.Now);
            _entidadesRepositorio.ActualizarDatosBorrado(entidad);
        }

        public void DesmarcarComoBorrado(Guid entidadId)
        {
            _entidadesRepositorio.ActualizarDatosBorrado(new Entidad { Id = entidadId });
        }

        private void ValidarDatos(Entidad entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, Entidades.Metadata.EntidadMetadata.Nombre.DISPLAY_NAME, requerido: true, errores, tamañoMaximo: Entidades.Metadata.EntidadMetadata.Nombre.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
