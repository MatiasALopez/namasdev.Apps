using System;
using System.Collections.Generic;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Core.Entity;
using namasdev.Core.Validation;

namespace namasdev.Apps.Negocio
{
    public class AplicacionesNegocio : IAplicacionesNegocio, IDisposable
    {
        private IAplicacionesRepositorio _aplicacionesRepositorio;

        public AplicacionesNegocio(IAplicacionesRepositorio aplicacionesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesRepositorio, nameof(aplicacionesRepositorio));

            _aplicacionesRepositorio = aplicacionesRepositorio;
        }

        public Aplicacion Agregar(string nombre, string usuarioId)
        {
            DateTime fechaHora = DateTime.Now;

            var aplicacion = new Aplicacion 
            {
                Id = Guid.NewGuid(),
                Nombre = nombre
            };
            aplicacion.EstablecerDatosCreado(usuarioId, fechaHora);
            aplicacion.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(aplicacion);

            _aplicacionesRepositorio.Agregar(aplicacion);

            return aplicacion;
        }

        public void Actualizar(Aplicacion aplicacion, string usuarioId)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacion, nameof(aplicacion));

            DateTime fechaHora = DateTime.Now;

            aplicacion.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(aplicacion);

            _aplicacionesRepositorio.Actualizar(aplicacion);
        }

        public void MarcarComoBorrado(Guid aplicacionId, string usuarioLogueadoId)
        {
            var aplicacion = new Aplicacion 
            {
                Id = aplicacionId
            };
            aplicacion.EstablecerDatosBorrado(usuarioLogueadoId, DateTime.Now);
            _aplicacionesRepositorio.ActualizarDatosBorrado(aplicacion);
        }

        public void DesmarcarComoBorrado(Guid aplicacionId)
        {
            _aplicacionesRepositorio.ActualizarDatosBorrado(new Aplicacion { Id = aplicacionId });
        }

        public void Dispose()
        {
            if (_aplicacionesRepositorio != null)
            {
                _aplicacionesRepositorio.Dispose();
            }
        }

        private void ValidarDatos(Aplicacion aplicacion)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(aplicacion.Nombre, Aplicacion.DISPLAY_NAME_NOMBRE, requerido: true, errores, 
                tamañoMaximo: 100);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
