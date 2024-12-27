using System;
using System.Collections.Generic;

using namasdev.Core.Entity;
using namasdev.Core.Transactions;
using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesNegocio
    {
        Entidad Agregar(Guid aplicacionVersionId, string nombre, string usuarioId, EntidadAltaOpciones opciones = null);
        void Actualizar(Entidad aplicacion, string usuarioId);
        void MarcarComoBorrado(Guid entidadId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid entidadId);
    }

    public class EntidadesNegocio : NegocioBase, IEntidadesNegocio
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

        public Entidad Agregar(Guid aplicacionVersionId, string nombre, string usuarioId,
            EntidadAltaOpciones opciones = null)
        {
            DateTime fechaHora = DateTime.Now;

            var entidad = new Entidad
            {
                Id = Guid.NewGuid(),
                AplicacionVersionId = aplicacionVersionId,
                Nombre = nombre
            };
            entidad.EstablecerDatosCreado(usuarioId, fechaHora);
            entidad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(entidad);

            using (var ts = TransactionScopeFactory.Crear())
            {
                _entidadesRepositorio.Agregar(entidad);

                if (opciones != null)
                {
                    _entidadesPropiedadesRepositorio.Agregar(CrearEntidadesPropiedades(entidad.Id, opciones));
                }
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

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, Entidades.Metadata.EntidadMetadata.Nombre.DISPLAY_NAME, requerido: true, errores, tamañoMaximo: Entidades.Metadata.EntidadMetadata.Nombre.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }

        private IEnumerable<EntidadPropiedad> CrearEntidadesPropiedades(Guid entidadId, EntidadAltaOpciones opciones)
        {
            var propiedades = new List<EntidadPropiedad>();

            if (opciones.PropiedadesCrearId)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = $"Id",
                    Etiqueta = "ID",
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = false,
                    Orden = 1000,
                    GeneradaAlCrear = true,
                    Editable = false,
                });
            }

            if (opciones.PropiedadesCrearAuditoriaCreado)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "CreadoPor",
                    Etiqueta = "Fecha creación",
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = false,
                    Orden = 1011,
                    GeneradaAlCrear = true,
                    Editable = false,
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "CreadoFecha",
                    Etiqueta = "Fecha/hora creación",
                    PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                    PermiteNull = false,
                    Orden = 1012,
                    GeneradaAlCrear = true,
                    Editable = false,
                });
            }

            if (opciones.PropiedadesCrearAuditoriaUltimaModificacion)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "UltimaModificacionPor",
                    Etiqueta = "Fecha creación",
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = false,
                    Orden = 1021,
                    GeneradaAlCrear = true,
                    Editable = false,
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "UltimaModificacionFecha",
                    Etiqueta = "Fecha/hora última modificación",
                    PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                    PermiteNull = false,
                    Orden = 1022,
                    GeneradaAlCrear = true,
                    Editable = false,
                });
            }

            if (opciones.PropiedadesCrearAuditoriaBorrado)
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "BorradoPor",
                    Etiqueta = "Fecha borrado",
                    PropiedadTipoId = PropiedadTipos.TEXTO,
                    PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                    PermiteNull = true,
                    Orden = 1031,
                    Editable = false,
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "BorradoFecha",
                    Etiqueta = "Fecha/hora borrado",
                    PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                    PermiteNull = false,
                    Orden = 1032,
                    Editable = false,
                });

                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidadId,
                    Nombre = "Borrado",
                    Etiqueta = "Borrado",
                    PropiedadTipoId = PropiedadTipos.BOOLEANO,
                    PermiteNull = false,
                    CalculadaFormula = "ISNULL(CONVERT(bit,CASE WHEN [BorradoFecha] IS NULL THEN 0 ELSE 1 END), 0)",
                    Orden = 1033,
                    Editable = false,
                });
            }

            return propiedades;
        }
    }
}
