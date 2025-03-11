using System;
using System.Collections.Generic;

using namasdev.Core.Entity;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesNegocio
    {
        Entidad Agregar(Guid aplicacionVersionId, string nombre, string nombrePlural, string etiqueta, string etiquetaPlural, string usuarioId, EntidadAltaOpciones opciones = null);
        void Actualizar(Entidad aplicacion, string usuarioId);
        void MarcarComoBorrado(Guid entidadId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid entidadId);
    }

    public class EntidadesNegocio : IEntidadesNegocio
    {
        private IEntidadesRepositorio _entidadesRepositorio;
        private IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesNegocio(IEntidadesRepositorio entidadesRepositorio, IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        public Entidad Agregar(Guid aplicacionVersionId, string nombre, string nombrePlural, string etiqueta, string etiquetaPlural, string usuarioId,
            EntidadAltaOpciones opciones = null)
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

            var propiedades = 
                opciones != null
                ? CrearEntidadesPropiedades(entidad, opciones)
                : null;

            using (var ts = TransactionScopeFactory.Crear())
            {
                _entidadesRepositorio.Agregar(entidad);

                if (propiedades != null)
                {
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

        private IEnumerable<EntidadPropiedad> CrearEntidadesPropiedades(Entidad entidad, EntidadAltaOpciones opciones)
        {
            var propiedades = new List<EntidadPropiedad>();

            if (opciones.PropiedadesCrearId)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = $"Id",
                    Etiqueta = "ID",
                    PropiedadTipoId = PropiedadTipos.GUID,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
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
            if (opciones.PropiedadesCrearAuditoriaCreado)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = "CreadoPor",
                    Etiqueta = "Fecha creación",
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
                    Nombre = "CreadoFecha",
                    Etiqueta = "Fecha/hora creación",
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

            if (opciones.PropiedadesCrearAuditoriaUltimaModificacion)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = "UltimaModificacionPor",
                    Etiqueta = "Fecha creación",
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
                    Nombre = "UltimaModificacionFecha",
                    Etiqueta = "Fecha/hora última modificación",
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

            if (opciones.PropiedadesCrearAuditoriaBorrado)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Nombre = "BorradoPor",
                    Etiqueta = "Fecha borrado",
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
                    Nombre = "BorradoFecha",
                    Etiqueta = "Fecha/hora borrado",
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
                    Nombre = "Borrado",
                    Etiqueta = "Borrado",
                    PropiedadTipoId = PropiedadTipos.BOOLEANO,
                    PermiteNull = false,
                    CalculadaFormula = "ISNULL(CONVERT(bit,CASE WHEN [BorradoFecha] IS NULL THEN 0 ELSE 1 END), 0)",
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
