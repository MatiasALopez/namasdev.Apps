using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.Models.AplicacionesVersiones;
using namasdev.Apps.Web.Portal.ViewModels.AplicacionesVersiones;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class AplicacionesVersionesMapper
    {
        public static List<AplicacionVersionItemModel> MapearEntidadesAModelos(IEnumerable<AplicacionVersion> entidades)
        {
            Validador.ValidarArgumentListaRequeridaYThrow(entidades, nameof(entidades), validarNoVacia: false);

            return entidades
                .Select(e => new AplicacionVersionItemModel
                {
                    AplicacionVersionId = e.Id,
                    AplicacionId = e.AplicacionId,
                    Nombre = e.Nombre,
                })
                .ToList();
        }

        public static AplicacionVersion MapearAplicacionVersionViewModelAEntidad(AplicacionVersionViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            return new AplicacionVersion
            {
                Id = modelo.AplicacionVersionId ?? Guid.Empty,
                AplicacionId = modelo.AplicacionId.Value,
                Nombre = modelo.Nombre,
            };
        }

        public static AplicacionVersionViewModel MapearEntidadAAplicacionViewModel(AplicacionVersion entidad)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            return new AplicacionVersionViewModel
            {
                AplicacionVersionId = entidad.Id,
                AplicacionId = entidad.AplicacionId,
                Nombre = entidad.Nombre,
            };
        }
    }
}