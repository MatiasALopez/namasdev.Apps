using System;
using System.Collections.Generic;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Core.Entity;
using namasdev.Core.Validation;

namespace namasdev.Apps.Negocio
{
    public interface IAplicacionesVersionesNegocio
    {
        AplicacionVersion Agregar(Guid aplicacionId, string nombre, string usuarioId);
        void Actualizar(AplicacionVersion aplicacionVersion, string usuarioId);
        void MarcarComoBorrado(Guid aplicacionVersionId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid aplicacionVersionId);
    }

    public class AplicacionesVersionesNegocio : IAplicacionesVersionesNegocio
    {
        private IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;

        public AplicacionesVersionesNegocio(IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));

            _aplicacionesVersionesRepositorio = aplicacionesVersionesRepositorio;
        }

        public AplicacionVersion Agregar(Guid aplicacionId, string nombre, string usuarioId)
        {
            DateTime fechaHora = DateTime.Now;

            var aplicacionVersion = new AplicacionVersion 
            {
                Id = Guid.NewGuid(),
                AplicacionId = aplicacionId,
                Nombre = nombre
            };
            aplicacionVersion.EstablecerDatosCreado(usuarioId, fechaHora);
            aplicacionVersion.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(aplicacionVersion);

            _aplicacionesVersionesRepositorio.Agregar(aplicacionVersion);

            return aplicacionVersion;
        }

        public void Actualizar(AplicacionVersion aplicacionVersion, string usuarioId)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacionVersion, nameof(aplicacionVersion));

            DateTime fechaHora = DateTime.Now;

            aplicacionVersion.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(aplicacionVersion);

            _aplicacionesVersionesRepositorio.Actualizar(aplicacionVersion);
        }

        public void MarcarComoBorrado(Guid aplicacionVersionId, string usuarioLogueadoId)
        {
            var aplicacion = new AplicacionVersion
            {
                Id = aplicacionVersionId
            };
            aplicacion.EstablecerDatosBorrado(usuarioLogueadoId, DateTime.Now);
            _aplicacionesVersionesRepositorio.ActualizarDatosBorrado(aplicacion);
        }

        public void DesmarcarComoBorrado(Guid aplicacionVersionId)
        {
            _aplicacionesVersionesRepositorio.ActualizarDatosBorrado(new AplicacionVersion { Id = aplicacionVersionId });
        }
        
        private void ValidarDatos(AplicacionVersion aplicacionVersion)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(aplicacionVersion.Nombre, Entidades.Metadata.AplicacionVersionMetadata.Nombre.DISPLAY_NAME, requerido: true, errores, tamañoMaximo: Entidades.Metadata.AplicacionVersionMetadata.Nombre.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
