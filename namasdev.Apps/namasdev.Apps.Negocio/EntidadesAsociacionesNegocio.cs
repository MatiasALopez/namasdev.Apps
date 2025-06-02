using System;
using System.Collections.Generic;

using AutoMapper;

using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio.DTO.EntidadesAsociaciones;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesAsociacionesNegocio
    {
        EntidadAsociacion Agregar(AgregarParametros parametros);
        void Actualizar(ActualizarParametros parametros);
    }

    public class EntidadesAsociacionesNegocio : NegocioBase<IEntidadesAsociacionesRepositorio>, IEntidadesAsociacionesNegocio
    {
        public EntidadesAsociacionesNegocio(IEntidadesAsociacionesRepositorio repositorio, IErroresNegocio erroresNegocio, IMapper mapper)
            : base(repositorio, erroresNegocio, mapper)
        {
        }

        public EntidadAsociacion Agregar(AgregarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Mapper.Map<EntidadAsociacion>(parametros);
            entidad.Id = Guid.NewGuid();

            ValidarDatos(entidad);

            Repositorio.Agregar(entidad);

            return entidad;
        }

        public void Actualizar(ActualizarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Obtener(parametros.Id);
            Mapper.Map(parametros, entidad);

            ValidarDatos(entidad);

            Repositorio.Actualizar(entidad);
        }

        private EntidadAsociacion Obtener(Guid id,
            bool validarExistencia = true)
        {
            var entidad = Repositorio.Obtener(id);
            if (validarExistencia
                && entidad == null)
            {
                throw new Exception(Validador.MensajeEntidadInexistente(EntidadAsociacionMetadata.ETIQUETA, id));
            }

            return entidad;
        }

        private void ValidarDatos(EntidadAsociacion entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.OrigenEntidadPropiedadNavegacionNombre, EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadNavegacionNombre.ETIQUETA, requerido: false, errores, tamañoMaximo: EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadNavegacionNombre.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.DestinoEntidadPropiedadNavegacionNombre, EntidadAsociacionMetadata.Propiedades.DestinoEntidadPropiedadNavegacionNombre.ETIQUETA, requerido: false, errores, tamañoMaximo: EntidadAsociacionMetadata.Propiedades.DestinoEntidadPropiedadNavegacionNombre.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.TablaAuxiliarNombre, EntidadAsociacionMetadata.Propiedades.TablaAuxiliarNombre.ETIQUETA, requerido: false, errores, tamañoMaximo: EntidadAsociacionMetadata.Propiedades.TablaAuxiliarNombre.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}

