using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Newtonsoft.Json;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio.DTO.EntidadesPropiedades;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesPropiedadesNegocio
    {
        EntidadPropiedad Agregar(AgregarParametros parametros);
        void Actualizar(ActualizarParametros parametros);
        void Eliminar(EliminarParametros parametros);
        void EstablecerComoClave(EstablecerComoClaveParametros parametros);
        void ActualizarOrden(ActualizarOrdenParametros parametros);
    }

    public class EntidadesPropiedadesNegocio : NegocioBase<IEntidadesPropiedadesRepositorio>, IEntidadesPropiedadesNegocio
    {
        private IEntidadesClavesRepositorio _entidadesClavesRepositorio;

        public EntidadesPropiedadesNegocio(
            IEntidadesClavesRepositorio entidadesClavesRepositorio,
            IEntidadesPropiedadesRepositorio repositorio, IErroresNegocio erroresNegocio, IMapper mapper)
            : base(repositorio, erroresNegocio, mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesClavesRepositorio, nameof(entidadesClavesRepositorio));

            _entidadesClavesRepositorio = entidadesClavesRepositorio;
        }


        public EntidadPropiedad Agregar(AgregarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Mapper.Map<EntidadPropiedad>(parametros);
            
            entidad.Id = Guid.NewGuid();
            entidad.PropiedadTipoEspecificaciones = SerializarPropiedadTipoEspecificaciones(parametros.EspecificacionesSegunTipo);
            entidad.Orden = Repositorio.ObtenerProximoOrdenDisponible(parametros.EntidadId);
            entidad.PropiedadCategoriaId = PropiedadCategorias.GENERAL;
                                        
            ValidarDatos(entidad);

            Repositorio.Agregar(entidad);

            return entidad;
        }

        public void Actualizar(ActualizarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Obtener(parametros.Id);
            Mapper.Map(parametros, entidad);
            
            entidad.PropiedadTipoEspecificaciones = SerializarPropiedadTipoEspecificaciones(parametros.EspecificacionesSegunTipo);

            ValidarDatos(entidad);

            Repositorio.Actualizar(entidad);
        }
                
        public void Eliminar(EliminarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));
    
            Repositorio.EliminarPorId(parametros.Id);
        }

        public void EstablecerComoClave(EstablecerComoClaveParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));
            Validador.ValidarArgumentListaRequeridaYThrow(parametros.PropiedadesIds, nameof(parametros.PropiedadesIds), validarNoVacia: false);

            var claves = parametros.PropiedadesIds
                .Select(id =>
                    new EntidadClave
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = parametros.EntidadId,
                        EntidadPropiedadId = id
                    })
                .ToArray();

            using (var ts = TransactionScopeFactory.Crear())
            {
                _entidadesClavesRepositorio.EliminarPorEntidad(parametros.EntidadId);
                _entidadesClavesRepositorio.Agregar(claves);

                ts.Complete();
            }
        }

        public void ActualizarOrden(ActualizarOrdenParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));
            Validador.ValidarArgumentListaRequeridaYThrow(parametros.PropiedadesIdsYOrdenes, nameof(parametros.PropiedadesIdsYOrdenes));

            var propiedades = parametros.PropiedadesIdsYOrdenes
                .Select(item =>
                    new EntidadPropiedad
                    {
                        Id = item.Key,
                        Orden = item.Value
                    })
                .ToArray();

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.ActualizarOrdenes(propiedades);

                ts.Complete();
            }
        }

        private EntidadPropiedad Obtener(Guid id,
            bool validarExistencia = true)
        {
            var entidad = Repositorio.Obtener(id);
            if (validarExistencia
                && entidad == null)
            {
                throw new Exception(Validador.MensajeEntidadInexistente(EntidadPropiedadMetadata.ETIQUETA, id));
            }

            return entidad;
        }

        private void ValidarDatos(EntidadPropiedad entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, EntidadPropiedadMetadata.Propiedades.Nombre.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadPropiedadMetadata.Propiedades.Nombre.TAMAÑO_MAX, regEx: EntidadPropiedadMetadata.Propiedades.Nombre.REG_EX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.Etiqueta, EntidadPropiedadMetadata.Propiedades.Etiqueta.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadPropiedadMetadata.Propiedades.Etiqueta.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.PropiedadTipoEspecificaciones, EntidadPropiedadMetadata.Propiedades.PropiedadTipoEspecificaciones.ETIQUETA, requerido: false, errores);
            Validador.ValidarStringYAgregarAListaErrores(entidad.CalculadaFormula, EntidadPropiedadMetadata.Propiedades.CalculadaFormula.ETIQUETA, requerido: false, errores);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }

        private string SerializarPropiedadTipoEspecificaciones(IPropiedadTipoEspecificaciones especificaciones)
        {
            return especificaciones != null
                ? JsonConvert.SerializeObject(especificaciones)
                : null;
        }
    }
}
