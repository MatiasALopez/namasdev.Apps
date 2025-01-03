using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.Models.Entidades;
using namasdev.Apps.Web.Portal.ViewModels.Entidades;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class EntidadesMapper
    {
        public static List<EntidadItemModel> MapearEntidadesAModelos(IEnumerable<Entidad> entidades)
        {
            Validador.ValidarArgumentListaRequeridaYThrow(entidades, nameof(entidades), validarNoVacia: false);

            return entidades
                .Select(e => new EntidadItemModel
                {
                    EntidadId = e.Id,
                    AplicacionVersionId = e.AplicacionVersionId,
                    Nombre = e.Nombre,
                    Etiqueta = e.Etiqueta
                })
                .ToList();
        }

        public static Entidad MapearEntidadViewModelAEntidad(EntidadViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            return new Entidad
            {
                Id = modelo.EntidadId ?? Guid.Empty,
                AplicacionVersionId = modelo.AplicacionVersionId,
                Nombre = modelo.Nombre,
                NombrePlural = modelo.NombrePlural,
                Etiqueta = modelo.Etiqueta,
                EtiquetaPlural = modelo.EtiquetaPlural
            };
        }

        public static EntidadAltaOpciones MapearEntidadViewModelAEntidadAltaOpciones(EntidadViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            return new EntidadAltaOpciones
            {
                PropiedadesCrearId = modelo.AltaOpcionesPropiedadesCrearId,
                PropiedadesCrearAuditoriaCreado = modelo.AltaOpcionesPropiedadesCrearAuditoriaCreado,
                PropiedadesCrearAuditoriaUltimaModificacion = modelo.AltaOpcionesPropiedadesCrearAuditoriaUltimaModificacion,
                PropiedadesCrearAuditoriaBorrado = modelo.AltaOpcionesPropiedadesCrearAuditoriaBorrado
            };
        }

        public static EntidadViewModel MapearEntidadAEntidadViewModel(Entidad entidad)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            return new EntidadViewModel
            {
                EntidadId = entidad.Id,
                AplicacionId = entidad.AplicacionVersion.AplicacionId,
                AplicacionNombre = entidad.AplicacionVersion.Aplicacion.Nombre,
                AplicacionVersionId = entidad.AplicacionVersionId,
                AplicacionVersionNombre = entidad.AplicacionVersion.Nombre,
                Nombre = entidad.Nombre,
                NombrePlural = entidad.NombrePlural,
                Etiqueta = entidad.Etiqueta,
                EtiquetaPlural = entidad.EtiquetaPlural
            };
        }
    }
}