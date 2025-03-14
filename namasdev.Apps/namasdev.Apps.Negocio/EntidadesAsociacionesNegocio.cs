using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using namasdev.Core.Entity;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesAsociacionesNegocio
    {
        EntidadAsociacion Agregar(
            Guid entidadOrigenId, Guid entidadPropiedadId, short origenMultiplicidadId,
            Guid destinoEntidadPropiedadId, short deleteAsociacionReglaId, short updateAsociacionReglaId,
            string usuarioId,
            string tablaAuxiliarNombre = null);

        void Actualizar(EntidadAsociacion asociacion, string usuarioId);

        void Eliminar(Guid asociacionId, string usuarioId);
    }

    public class EntidadesAsociacionesNegocio : IEntidadesAsociacionesNegocio
    {
        private IEntidadesAsociacionesRepositorio _entidadesAsociacionesRepositorio;

        public EntidadesAsociacionesNegocio(
            IEntidadesAsociacionesRepositorio entidadesAsociacionesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesAsociacionesRepositorio, nameof(entidadesAsociacionesRepositorio));

            _entidadesAsociacionesRepositorio = entidadesAsociacionesRepositorio;
        }

        public EntidadAsociacion Agregar(
            Guid entidadOrigenId, Guid entidadPropiedadId, short origenMultiplicidadId, 
            Guid destinoEntidadPropiedadId, short deleteAsociacionReglaId, short updateAsociacionReglaId,
            string usuarioId,
            string tablaAuxiliarNombre = null)
        {
            DateTime fechaHora = DateTime.Now;

            var asociacion = new EntidadAsociacion
            {
                Id = Guid.NewGuid(),
                OrigenEntidadId = entidadOrigenId,
                OrigenEntidadPropiedadId = entidadPropiedadId,
                OrigenAsociacionMultiplicidadId = origenMultiplicidadId,
                DestinoEntidadPropiedadId = destinoEntidadPropiedadId,
                TablaAuxiliarNombre = tablaAuxiliarNombre,
                DeleteAsociacionReglaId = deleteAsociacionReglaId,
                UpdateAsociacionReglaId = updateAsociacionReglaId,
            };
            ValidarDatos(asociacion);

            _entidadesAsociacionesRepositorio.Agregar(asociacion);

            return asociacion;
        }

        public void Actualizar(EntidadAsociacion asociacion, string usuarioId)
        {
            Validador.ValidarArgumentRequeridoYThrow(asociacion, nameof(asociacion));

            DateTime fechaHora = DateTime.Now;

            //asociacion.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(asociacion);

            _entidadesAsociacionesRepositorio.Actualizar(asociacion);
        }

        public void Eliminar(Guid asociacionId, string usuarioId)
        {
            _entidadesAsociacionesRepositorio.Eliminar(new EntidadAsociacion { Id = asociacionId });
        }

        private void ValidarDatos(EntidadAsociacion entidad)
        {
            var errores = new List<string>();

            // TODO (ML) completar

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
