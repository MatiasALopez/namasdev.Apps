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
            };
        }
    }
}