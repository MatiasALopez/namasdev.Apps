using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.Models.Aplicaciones;
using namasdev.Apps.Web.Portal.ViewModels.Aplicaciones;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class AplicacionesMapper
    {
        public static List<AplicacionItemModel> MapearEntidadesAModelos(IEnumerable<Aplicacion> entidades)
        {
            Validador.ValidarArgumentListaRequeridaYThrow(entidades, nameof(entidades), validarNoVacia: false);

            return entidades
                .Select(e => new AplicacionItemModel
                {
                    AplicacionId = e.Id,
                    Nombre = e.Nombre,
                })
                .ToList();
        }

        public static Aplicacion MapearAplicacionViewModelAEntidad(AplicacionViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            return new Aplicacion
            {
                Id = modelo.AplicacionId ?? default,
                Nombre = modelo.Nombre,
                IdiomaId = modelo.IdiomaId,
            };
        }

        public static AplicacionViewModel MapearEntidadAAplicacionViewModel(Aplicacion entidad)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            return new AplicacionViewModel
            {
                AplicacionId = entidad.Id,
                Nombre = entidad.Nombre,
                IdiomaId = entidad.IdiomaId,
            };
        }
    }
}