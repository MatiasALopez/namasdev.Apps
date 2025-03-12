using System;
using System.Collections.Generic;

using namasdev.Core.Entity;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesNegocio
    {
        Entidad Agregar(Guid aplicacionVersionId, string nombre, string nombrePlural, string etiqueta, string etiquetaPlural, string usuarioId, EntidadPropiedadesDefault propiedadesDefault);
        void Actualizar(Entidad aplicacion, string usuarioId);
        void MarcarComoBorrado(Guid entidadId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid entidadId);
    }

    public class EntidadesNegocio : IEntidadesNegocio
    {
        private IEntidadesRepositorio _entidadesRepositorio;
        private IEntidadesPropiedadesDefaultRepositorio _entidadesPropiedadesDefaultRepositorio;
        private IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesNegocio(IEntidadesRepositorio entidadesRepositorio, IEntidadesPropiedadesDefaultRepositorio entidadesPropiedadesDefaultRepositorio, IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesDefaultRepositorio, nameof(entidadesPropiedadesDefaultRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesDefaultRepositorio = entidadesPropiedadesDefaultRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        public Entidad Agregar(Guid aplicacionVersionId, string nombre, string nombrePlural, string etiqueta, string etiquetaPlural, string usuarioId,
            EntidadPropiedadesDefault propiedadesDefault)
        {
            DateTime fechaHora = DateTime.Now;

            var entidad = new Entidad
            {
                Id = Guid.NewGuid(),
                AplicacionVersionId = aplicacionVersionId,
                Nombre = nombre,
                NombrePlural = nombrePlural,
                Etiqueta = etiqueta,
                EtiquetaPlural = etiquetaPlural
            };
            entidad.EstablecerDatosCreado(usuarioId, fechaHora);
            entidad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(entidad);

            propiedadesDefault = propiedadesDefault ?? new EntidadPropiedadesDefault();
            propiedadesDefault.Id = entidad.Id;

            var propiedades = CrearPropiedadesDefaultParaEntidad(entidad, propiedadesDefault);

            using (var ts = TransactionScopeFactory.Crear())
            {
                _entidadesRepositorio.Agregar(entidad);

                if (propiedades != null)
                {
                    _entidadesPropiedadesDefaultRepositorio.Agregar(propiedadesDefault);
                    _entidadesPropiedadesRepositorio.Agregar(propiedades);
                }

                ts.Complete();
            }

            return entidad;
        }

        public void Actualizar(Entidad entidad, string usuarioId)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            DateTime fechaHora = DateTime.Now;

            entidad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(entidad);

            _entidadesRepositorio.Actualizar(entidad);
        }

        public void MarcarComoBorrado(Guid entidadId, string usuarioLogueadoId)
        {
            var entidad = new Entidad 
            {
                Id = entidadId
            };
            entidad.EstablecerDatosBorrado(usuarioLogueadoId, DateTime.Now);
            _entidadesRepositorio.ActualizarDatosBorrado(entidad);
        }

        public void DesmarcarComoBorrado(Guid entidadId)
        {
            _entidadesRepositorio.ActualizarDatosBorrado(new Entidad { Id = entidadId });
        }

        private void ValidarDatos(Entidad entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, EntidadMetadata.Nombre.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.Nombre.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.NombrePlural, EntidadMetadata.NombrePlural.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.NombrePlural.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.Etiqueta, EntidadMetadata.Etiqueta.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.Etiqueta.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.EtiquetaPlural, EntidadMetadata.EtiquetaPlural.ETIQUETA, requerido: true, errores, tamañoMaximo: EntidadMetadata.EtiquetaPlural.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }

        private IEnumerable<EntidadPropiedad> CrearPropiedadesDefaultParaEntidad(Entidad entidad, EntidadPropiedadesDefault propiedadesDefault)
        {
            var propiedades = new List<EntidadPropiedad>();

            if (propiedadesDefault.IDPropiedadTipoId.HasValue)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = EntidadPropiedades.Id.Nombre(entidad),
                    Etiqueta = EntidadPropiedades.Id.ETIQUETA,
                    PropiedadTipoId = propiedadesDefault.IDPropiedadTipoId.Value,
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
    }
}
