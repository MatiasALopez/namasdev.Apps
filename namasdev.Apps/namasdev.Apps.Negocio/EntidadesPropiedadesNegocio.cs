using System;
using System.Collections.Generic;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Core.Entity;
using namasdev.Core.Validation;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesPropiedadesNegocio
    {
        EntidadPropiedad Agregar(Guid entidadId, string nombre, string etiqueta, short propiedadTipoId, IPropiedadTipoEspecificaciones propiedadTipoEspecificaciones, short orden, bool permiteNull, bool generadaAlCrear, bool editable, string calculadaFormula, string usuarioId);
        void Actualizar(EntidadPropiedad propiedad, IPropiedadTipoEspecificaciones propiedadTipoEspecificaciones, string usuarioId);
        void MarcarComoBorrado(Guid entidadPropiedadId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid entidadPropiedadId);
    }

    public class EntidadesPropiedadesNegocio : NegocioBase, IEntidadesPropiedadesNegocio
    {
        private IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesPropiedadesNegocio(IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        public EntidadPropiedad Agregar(Guid entidadId, string nombre, string etiqueta, short propiedadTipoId, IPropiedadTipoEspecificaciones propiedadTipoEspecificaciones, short orden, bool permiteNull, bool generadaAlCrear, bool editable, string calculadaFormula, string usuarioId)
        {
            DateTime fechaHora = DateTime.Now;

            var propiedad = new EntidadPropiedad
            {
                Id = Guid.NewGuid(),
                EntidadId = entidadId,
                Nombre = nombre,
                Etiqueta = etiqueta,
                PropiedadTipoId = propiedadTipoId,
                PropiedadTipoEspecificaciones = SerializarPropiedadTipoEspecificaciones(propiedadTipoEspecificaciones),
                Orden = orden,
                PermiteNull = permiteNull,
                GeneradaAlCrear = generadaAlCrear,
                Editable = editable,
                CalculadaFormula = calculadaFormula,
            };
            propiedad.EstablecerDatosCreado(usuarioId, fechaHora);
            propiedad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(propiedad);

            _entidadesPropiedadesRepositorio.Agregar(propiedad);

            return propiedad;
        }

        public void Actualizar(EntidadPropiedad propiedad, IPropiedadTipoEspecificaciones propiedadTipoEspecificaciones, string usuarioId)
        {
            Validador.ValidarArgumentRequeridoYThrow(propiedad, nameof(propiedad));

            DateTime fechaHora = DateTime.Now;

            propiedad.PropiedadTipoEspecificaciones = SerializarPropiedadTipoEspecificaciones(propiedadTipoEspecificaciones);
            propiedad.EstablecerDatosModificacion(usuarioId, fechaHora);
            ValidarDatos(propiedad);

            _entidadesPropiedadesRepositorio.Actualizar(propiedad);
        }

        public void MarcarComoBorrado(Guid entidadPropiedadId, string usuarioLogueadoId)
        {
            var propiedad = new EntidadPropiedad 
            {
                Id = entidadPropiedadId
            };
            propiedad.EstablecerDatosBorrado(usuarioLogueadoId, DateTime.Now);
            _entidadesPropiedadesRepositorio.ActualizarDatosBorrado(propiedad);
        }

        public void DesmarcarComoBorrado(Guid entidadPropiedadId)
        {
            _entidadesPropiedadesRepositorio.ActualizarDatosBorrado(new EntidadPropiedad { Id = entidadPropiedadId });
        }

        private void ValidarDatos(EntidadPropiedad entidad)
        {
            var errores = new List<string>();

            Validador.ValidarStringYAgregarAListaErrores(entidad.Nombre, Entidades.Metadata.EntidadPropiedadMetadata.Nombre.DISPLAY_NAME, requerido: true, errores, tamañoMaximo: Entidades.Metadata.EntidadPropiedadMetadata.Nombre.TAMAÑO_MAX, regEx: Entidades.Metadata.EntidadPropiedadMetadata.Nombre.REG_EX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.Etiqueta, Entidades.Metadata.EntidadPropiedadMetadata.Etiqueta.DISPLAY_NAME, requerido: true, errores, tamañoMaximo: Entidades.Metadata.EntidadPropiedadMetadata.Etiqueta.TAMAÑO_MAX);
            Validador.ValidarStringYAgregarAListaErrores(entidad.PropiedadTipoEspecificaciones, Entidades.Metadata.EntidadPropiedadMetadata.PropiedadTipoEspecificaciones.DISPLAY_NAME, requerido: false, errores);
            Validador.ValidarStringYAgregarAListaErrores(entidad.CalculadaFormula, Entidades.Metadata.EntidadPropiedadMetadata.CalculadaFormula.DISPLAY_NAME, requerido: false, errores, tamañoMaximo: Entidades.Metadata.EntidadPropiedadMetadata.CalculadaFormula.TAMAÑO_MAX);

            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }
    }
}
