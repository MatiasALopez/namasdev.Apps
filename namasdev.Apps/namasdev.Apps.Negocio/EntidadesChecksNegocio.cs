using System;
using System.Collections.Generic;

using AutoMapper;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio.DTO.EntidadesChecks;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesChecksNegocio
    {
        EntidadCheck Agregar(AgregarParametros parametros);
        void Actualizar(ActualizarParametros parametros);
    }

    public class EntidadesChecksNegocio : NegocioBase<IEntidadesChecksRepositorio>, IEntidadesChecksNegocio
    {
        public EntidadesChecksNegocio(
            IEntidadesChecksRepositorio repositorio,
            IErroresNegocio erroresNegocio, 
            IMapper mapper)
            : base(repositorio, erroresNegocio, mapper)
        {
        }

        public EntidadCheck Agregar(AgregarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Mapper.Map<EntidadCheck>(parametros);
            entidad.Id = Guid.NewGuid();

            ValidarDatos(entidad);

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Agregar(entidad);

                ts.Complete();
            }

            return entidad;
        }

        public void Actualizar(ActualizarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Obtener(parametros.Id);
            Mapper.Map(parametros, entidad);

            ValidarDatos(entidad);

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Actualizar(entidad);

                ts.Complete();
            }
        }

        private void ValidarDatos(EntidadCheck entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, EntidadCheckMetadata.Propiedades.Nombre.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadCheckMetadata.Propiedades.Nombre.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.Expresion, EntidadCheckMetadata.Propiedades.ExpresionNombre.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadCheckMetadata.Propiedades.ExpresionNombre.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }

        private EntidadCheck Obtener(Guid id,
            bool validarExistencia = true)
        {
            var entidad = Repositorio.Obtener(id);
            if (validarExistencia
                && entidad == null)
            {
                throw new Exception(Validador.MensajeEntidadInexistente(EntidadCheckMetadata.ETIQUETA, id));
            }

            return entidad;
        }
    }
}

