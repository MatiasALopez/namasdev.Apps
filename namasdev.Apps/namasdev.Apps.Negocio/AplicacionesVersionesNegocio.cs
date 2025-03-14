using System;
using System.Collections.Generic;

using namasdev.Core.Entity;
using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using System.Transactions;
using namasdev.Core.Transactions;

namespace namasdev.Apps.Negocio
{
    public interface IAplicacionesVersionesNegocio
    {
        AplicacionVersion Agregar(Guid aplicacionId, string nombre, string usuarioId, Guid? aplicacionVersionIdBase = null);
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

        public AplicacionVersion Agregar(Guid aplicacionId, string nombre, string usuarioId,
            Guid? aplicacionVersionIdBase = null)
        {
            DateTime fechaHora = DateTime.Now;

            var version = new AplicacionVersion 
            {
                Id = Guid.NewGuid(),
                AplicacionId = aplicacionId,
                Nombre = nombre
            };
            version.EstablecerDatosCreado(usuarioId, fechaHora);
            version.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(version);

            using (var ts = TransactionScopeFactory.Crear())
            {
                _aplicacionesVersionesRepositorio.Agregar(version);
                
                if (aplicacionVersionIdBase.HasValue)
                {
                    _aplicacionesVersionesRepositorio.Clonar(aplicacionVersionIdBase.Value, version.Id, version.CreadoPor, version.CreadoFecha);
                }

                ts.Complete();
            }

            return version;
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

            Validador.ValidarStringYAgregarAListaErrores(aplicacionVersion.Nombre, AplicacionVersionMetadata.Propiedades.Nombre.ETIQUETA, requerido: true, errores, tamañoMaximo: AplicacionVersionMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
