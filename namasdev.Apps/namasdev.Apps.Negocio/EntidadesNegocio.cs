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
        private readonly IEntidadesPropiedadesDefaultRepositorio _entidadesPropiedadesDefaultRepositorio;
        private readonly IEntidadesClavesRepositorio _entidadesClavesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesNegocio(
            IEntidadesPropiedadesDefaultRepositorio entidadesPropiedadesDefaultRepositorio,
            IEntidadesClavesRepositorio entidadesClavesRepositorio,
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IEntidadesRepositorio entidadesRepositorio, IErroresNegocio erroresNegocio, IMapper mapper)
            : base(entidadesRepositorio, erroresNegocio, mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesDefaultRepositorio, nameof(entidadesPropiedadesDefaultRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesClavesRepositorio, nameof(entidadesClavesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesPropiedadesDefaultRepositorio = entidadesPropiedadesDefaultRepositorio;
            _entidadesClavesRepositorio = entidadesClavesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
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

            IEnumerable<EntidadPropiedad> propiedades = null;
            IEnumerable<EntidadClave> claves = null;
            if (parametros.PropiedadesDefault != null)
            {
                entidad.PropiedadesDefault.Id = entidad.Id;

                propiedades = CrearPropiedadesParaEntidad(entidad, entidad.PropiedadesDefault);
                claves = CrearClavesParaEntidad(entidad, entidad.PropiedadesDefault, propiedades);
            }

            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Agregar(entidad);
                _entidadesPropiedadesDefaultRepositorio.Agregar(entidad.PropiedadesDefault);

                if (propiedades != null && propiedades.Any())
                {
                    _entidadesPropiedadesRepositorio.Agregar(propiedades);
                }
                if (claves != null && claves.Any())
                {
                    _entidadesClavesRepositorio.Agregar(claves);
                }

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

        private IEnumerable<EntidadPropiedad> CrearPropiedadesParaEntidad(Entidad entidad, EntidadPropiedadesDefault propiedadesDefault)
        {
            var propiedades = new List<EntidadPropiedad>();

            if (propiedadesDefault.IDPropiedadTipoId.HasValue)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedad.IdNombre(entidad),
                    Etiqueta = EntidadPropiedades.Id.ETIQUETA,
                    PropiedadTipoId = propiedadesDefault.IDPropiedadTipoId.Value,
                    PropiedadTipoEspecificaciones = CrearPropiedadEspecificacionesDefault(propiedadesDefault.IDPropiedadTipoId.Value),
                    PermiteNull = false,
                    Orden = 1,
                    GeneradaAlCrear = true,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });
            }

            short orden = 101;
            if (propiedadesDefault.AuditoriaCreado)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.CreadoPor.NOMBRE,
                    Etiqueta = EntidadPropiedades.CreadoPor.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = false,
                    Orden = orden++,
                    GeneradaAlCrear = true,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.CreadoFecha.NOMBRE,
                    Etiqueta = EntidadPropiedades.CreadoFecha.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                    PermiteNull = false,
                    Orden = orden++,
                    GeneradaAlCrear = true,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });
            }

            if (propiedadesDefault.AuditoriaUltimaModificacion)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.UltimaModificacionPor.NOMBRE,
                    Etiqueta = EntidadPropiedades.UltimaModificacionPor.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = false,
                    Orden = orden++,
                    GeneradaAlCrear = true,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.UltimaModificacionFecha.NOMBRE,
                    Etiqueta = EntidadPropiedades.UltimaModificacionFecha.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                    PermiteNull = false,
                    Orden = orden++,
                    GeneradaAlCrear = true,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });
            }

            if (propiedadesDefault.AuditoriaBorrado)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.BorradoPor.NOMBRE,
                    Etiqueta = EntidadPropiedades.BorradoPor.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = true,
                    Orden = orden++,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.BorradoFecha.NOMBRE,
                    Etiqueta = EntidadPropiedades.BorradoFecha.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                    PermiteNull = true,
                    Orden = orden++,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.Borrado.NOMBRE,
                    Etiqueta = EntidadPropiedades.Borrado.ETIQUETA,
                    PropiedadTipoId = PropiedadTipos.BOOLEANO,
                    PermiteNull = false,
                    CalculadaFormula = EntidadPropiedades.Borrado.CALCULADA_FORMULA,
                    Orden = orden++,
                    Editable = false,
                    CreadoPor = entidad.CreadoPor,
                    CreadoFecha = entidad.CreadoFecha,
                    UltimaModificacionPor = entidad.UltimaModificacionPor,
                    UltimaModificacionFecha = entidad.UltimaModificacionFecha
                });
            }

            return propiedades;
        }

        private string CrearPropiedadEspecificacionesDefault(short propiedadTipoId)
        {
            switch (propiedadTipoId)
            {
                case PropiedadTipos.TEXTO:
                    return PropiedadTiposEspecificaciones.ID_TEXTO_DEFAULT;
                
                case PropiedadTipos.ENTERO:
                case PropiedadTipos.ENTERO_CORTO:
                case PropiedadTipos.ENTERO_LARGO:
                    return PropiedadTiposEspecificaciones.ID_ENTERO_DEFAULT;
                
                case PropiedadTipos.DECIMAL:
                case PropiedadTipos.DECIMAL_FLOTANTE:
                    return PropiedadTiposEspecificaciones.ID_DECIMAL_DEFAULT;
                
                default:
                    return null;
            }
        }

        private IEnumerable<EntidadClave> CrearClavesParaEntidad(Entidad entidad, EntidadPropiedadesDefault propiedadesDefault, IEnumerable<EntidadPropiedad> propiedades)
        {
            if (propiedadesDefault.IDPropiedadTipoId.HasValue)
            {
                var propiedadId =
                    propiedades != null
                    ? propiedades.FirstOrDefault()
                    : null;

                if (propiedadId != null)
                {
                    return new[]
                    {
                        new EntidadClave
                        {
                            Id = Guid.NewGuid(),
                            EntidadId = entidad.Id,
                            EntidadPropiedadId = propiedadId.Id
                        }
                    };
                }
            }

            return null;
        }
    }
}
