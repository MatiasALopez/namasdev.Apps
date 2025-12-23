using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using namasdev.Core.Entity;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio.DTO.Entidades;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesNegocio
    {
        Entidad Agregar(AgregarParametros parametros);
        void Actualizar(ActualizarParametros parametros);
        void MarcarComoBorrado(MarcarComoBorradoParametros parametros);
        void DesmarcarComoBorrado(DesmarcarComoBorradoParametros parametros);
    }

    public class EntidadesNegocio : NegocioBase<IEntidadesRepositorio>, IEntidadesNegocio
    {
        private readonly IEntidadesEspecificacionesRepositorio _entidadesEspecificacionesRepositorio;

        public EntidadesNegocio(
            IEntidadesEspecificacionesRepositorio entidadesEspecificacionesRepositorio,
            IEntidadesRepositorio entidadesRepositorio, IErroresNegocio erroresNegocio, IMapper mapper)
            : base(entidadesRepositorio, erroresNegocio, mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesEspecificacionesRepositorio, nameof(entidadesEspecificacionesRepositorio));

            _entidadesEspecificacionesRepositorio = entidadesEspecificacionesRepositorio;
        }

        public Entidad Agregar(AgregarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            DateTime fechaHora = DateTime.Now;

            var entidad = Mapper.Map<Entidad>(parametros);
            entidad.Id = Guid.NewGuid();
            entidad.EstablecerDatosCreado(parametros.UsuarioLogueadoId, fechaHora);
            entidad.EstablecerDatosModificacion(parametros.UsuarioLogueadoId, fechaHora);
            
            ValidarDatos(entidad);

            entidad.Especificaciones = new EntidadEspecificaciones 
            {
                Id = entidad.Id,
                BajaTipoId = BajaTipos.LOGICA
            };

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Agregar(entidad);
                _entidadesEspecificacionesRepositorio.Agregar(entidad.Especificaciones);

                ts.Complete();
            }
            
            return entidad;
        }

        public void Actualizar(ActualizarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            DateTime fechaHora = DateTime.Now;

            var entidad = Obtener(parametros.Id);
            Mapper.Map(parametros, entidad);
            entidad.EstablecerDatosModificacion(parametros.UsuarioLogueadoId, fechaHora);
            
            ValidarDatos(entidad);

            Repositorio.Actualizar(entidad);
        }

        public void MarcarComoBorrado(MarcarComoBorradoParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            DateTime fechaHora = DateTime.Now;

            var entidad = Mapper.Map<Entidad>(parametros);
            entidad.EstablecerDatosBorrado(parametros.UsuarioLogueadoId, DateTime.Now);

            Repositorio.ActualizarDatosBorrado(entidad);
        }

        public void DesmarcarComoBorrado(DesmarcarComoBorradoParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Mapper.Map<Entidad>(parametros);
            Repositorio.ActualizarDatosBorrado(entidad);
        }

        private Entidad Obtener(Guid id,
            bool validarExistencia = true)
        {
            var entidad = Repositorio.Obtener(id);
            if (validarExistencia
                && entidad == null)
            {
                throw new Exception(Validador.MensajeEntidadInexistente(EntidadMetadata.ETIQUETA, id));
            }

            return entidad;
        }

        private void ValidarDatos(Entidad entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, EntidadMetadata.Propiedades.Nombre.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.Propiedades.Nombre.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.NombrePlural, EntidadMetadata.Propiedades.NombrePlural.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.Propiedades.NombrePlural.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.Etiqueta, EntidadMetadata.Propiedades.Etiqueta.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.Propiedades.Etiqueta.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.EtiquetaPlural, EntidadMetadata.Propiedades.EtiquetaPlural.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.Propiedades.EtiquetaPlural.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
