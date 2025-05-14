using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio.DTO.EntidadesIndices;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesIndicesNegocio
    {
        EntidadIndice Agregar(AgregarParametros parametros);
        void Actualizar(ActualizarParametros parametros);
    }

    public class EntidadesIndicesNegocio : NegocioBase<IEntidadesIndicesRepositorio>, IEntidadesIndicesNegocio
    {
        private readonly IEntidadesIndicesPropiedadesRepositorio _entidadesIndicesPropiedadesRepositorio;

        public EntidadesIndicesNegocio(
            IEntidadesIndicesRepositorio repositorio,
            IEntidadesIndicesPropiedadesRepositorio entidadesIndicesPropiedadesRepositorio,
            IErroresNegocio erroresNegocio, 
            IMapper mapper)
            : base(repositorio, erroresNegocio, mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesIndicesPropiedadesRepositorio, nameof(entidadesIndicesPropiedadesRepositorio));

            _entidadesIndicesPropiedadesRepositorio = entidadesIndicesPropiedadesRepositorio;
        }

        public EntidadIndice Agregar(AgregarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Mapper.Map<EntidadIndice>(parametros);
            entidad.Id = Guid.NewGuid();

            ValidarDatos(entidad, parametros.EntidadesPropiedadesIds);

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Agregar(entidad);
                AsignarPropiedadesDeIndice(entidad.Id, parametros.EntidadesPropiedadesIds);

                ts.Complete();
            }

            return entidad;
        }

        public void Actualizar(ActualizarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Obtener(parametros.Id);
            Mapper.Map(parametros, entidad);

            ValidarDatos(entidad, parametros.EntidadesPropiedadesIds);

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Actualizar(entidad);
                AsignarPropiedadesDeIndice(entidad.Id, parametros.EntidadesPropiedadesIds);

                ts.Complete();
            }
        }

        private void ValidarDatos(EntidadIndice entidad, IEnumerable<Guid> propiedadesIds)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, EntidadIndiceMetadata.Propiedades.Nombre.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadIndiceMetadata.Propiedades.Nombre.TAMAÑO_MAX, regEx: EntidadIndiceMetadata.Propiedades.Nombre.REG_EX);

            if (propiedadesIds == null || !propiedadesIds.Any())
            {
                errores.Add("Debe seleccionar al menos una propiedad.");
            }

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }

        private void AsignarPropiedadesDeIndice(Guid entidadIndiceId, IEnumerable<Guid> propiedadesIds)
        {
            var nuevas = propiedadesIds
                .Select(id => new EntidadIndicePropiedad 
                {
                    Id = Guid.NewGuid(),
                    EntidadIndiceId = entidadIndiceId,
                    EntidadPropiedadId = id
                })
                .ToArray();

            var existentes = _entidadesIndicesPropiedadesRepositorio.ObtenerPorIndice(entidadIndiceId);

            _entidadesIndicesPropiedadesRepositorio.Eliminar(existentes);
            _entidadesIndicesPropiedadesRepositorio.Agregar(nuevas);
        }

        private EntidadIndice Obtener(Guid id,
            bool validarExistencia = true)
        {
            var entidad = Repositorio.Obtener(id);
            if (validarExistencia
                && entidad == null)
            {
                throw new Exception(Validador.MensajeEntidadInexistente(EntidadIndiceMetadata.ETIQUETA, id));
            }

            return entidad;
        }
    }
}

