using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.Models.Versiones;
using namasdev.Apps.Web.Portal.ViewModels.Versiones;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class VersionesMapper
    {
        public static List<VersionItemModel> MapearEntidadesAModelos(IEnumerable<AplicacionVersion> entidades)
        {
            Validador.ValidarArgumentListaRequeridaYThrow(entidades, nameof(entidades), validarNoVacia: false);

            return entidades
                .Select(e => new VersionItemModel
                {
                    AplicacionVersionId = e.Id,
                    AplicacionId = e.AplicacionId,
                    Nombre = e.Nombre,
                })
                .ToList();
        }

        public static AplicacionVersion MapearAplicacionVersionViewModelAEntidad(VersionViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            return new AplicacionVersion
            {
                Id = modelo.AplicacionVersionId ?? Guid.Empty,
                AplicacionId = modelo.AplicacionId,
                Nombre = modelo.Nombre,
            };
        }

        public static VersionViewModel MapearEntidadAAplicacionViewModel(AplicacionVersion entidad)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            return new VersionViewModel
            {
                AplicacionVersionId = entidad.Id,
                AplicacionId = entidad.AplicacionId,
                AplicacionNombre = entidad.Aplicacion.Nombre,
                Nombre = entidad.Nombre,
            };
        }
    }
}