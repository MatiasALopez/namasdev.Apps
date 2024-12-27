using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class EntidadesPropiedadesMapper
    {
        public static List<EntidadPropiedadItemModel> MapearEntidadesAModelos(IEnumerable<EntidadPropiedad> entidades)
        {
            Validador.ValidarArgumentListaRequeridaYThrow(entidades, nameof(entidades), validarNoVacia: false);

            return entidades
                .Select(e => new EntidadPropiedadItemModel
                {
                    EntidadPropiedadId = e.Id,
                    EntidadId = e.EntidadId,
                    Nombre = e.Nombre,
                    PropiedadTipoId = e.PropiedadTipoId,
                    PropiedadTipoNombre = e.Tipo.Nombre,
                    PermiteNull = e.PermiteNull,
                    Orden = e.Orden
                })
                .ToList();
        }

        public static EntidadPropiedad MapearEntidadPropiedadViewModelAEntidad(EntidadPropiedadViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            return new EntidadPropiedad
            {
                Id = modelo.EntidadPropiedadId ?? Guid.Empty,
                EntidadId = modelo.EntidadId,
                Nombre = modelo.Nombre,
                Etiqueta = modelo.Etiqueta,
                PropiedadTipoId = modelo.PropiedadTipoId.Value,
                CalculadaFormula = modelo.CalculadaFormula,
                Orden = modelo.Orden.Value,
                PermiteNull = modelo.PermiteNull.Value,
                GeneradaAlCrear = modelo.GeneradaAlCrear.Value,
                Editable = modelo.Editable.Value,
            };
        }

        public static IPropiedadTipoEspecificaciones MapearEntidadPropiedadViewModelAPropiedadTipoEspecificacionesEntidad(EntidadPropiedadViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            throw new NotImplementedException();
            // TODO (ML): completar
            //switch (modelo.PropiedadTipoId)
            //{

            //}
        }

        public static EntidadPropiedadViewModel MapearEntidadAEntidadPropiedadViewModel(EntidadPropiedad entidad)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidad, nameof(entidad));

            return new EntidadPropiedadViewModel
            {
                EntidadPropiedadId = entidad.Id,
                EntidadId = entidad.EntidadId,
                EntidadNombre = entidad.Entidad.Nombre,
                Nombre = entidad.Nombre,
                Etiqueta = entidad.Etiqueta,
                PropiedadTipoId = entidad.PropiedadTipoId,
                CalculadaFormula = entidad.CalculadaFormula,
                Orden = entidad.Orden,
                PermiteNull = entidad.PermiteNull,
                GeneradaAlCrear = entidad.GeneradaAlCrear,
                Editable = entidad.Editable,
            };
        }
    }
}