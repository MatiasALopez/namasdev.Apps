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

            IPropiedadTipoEspecificaciones especificaciones = null;

            if (modelo.PropiedadTipoId.HasValue)
            {
                switch (modelo.PropiedadTipoId.Value)
                {
                    case PropiedadTipos.TEXTO:
                        if (modelo.PropiedadTipoEspecificacionesTexto != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesTexto 
                            {
                                TamañoMinimo = modelo.PropiedadTipoEspecificacionesTexto.TamañoMinimo,
                                TamañoMaximo = modelo.PropiedadTipoEspecificacionesTexto.TamañoMaximo,
                                TamañoExacto = modelo.PropiedadTipoEspecificacionesTexto.TamañoExacto,
                                RegEx = modelo.PropiedadTipoEspecificacionesTexto.RegEx,
                            };
                        }
                        break;

                    case PropiedadTipos.ENTERO:
                        if (modelo.PropiedadTipoEspecificacionesEntero != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesEntero
                            {
                                ValorMinimo = (int?)modelo.PropiedadTipoEspecificacionesEntero.ValorMinimo,
                                ValorMaximo = (int?)modelo.PropiedadTipoEspecificacionesEntero.ValorMaximo,
                            };
                        }
                        break;

                    case PropiedadTipos.ENTERO_CORTO:
                        if (modelo.PropiedadTipoEspecificacionesEntero != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesEnteroCorto
                            {
                                ValorMinimo = (short?)modelo.PropiedadTipoEspecificacionesEntero.ValorMinimo,
                                ValorMaximo = (short?)modelo.PropiedadTipoEspecificacionesEntero.ValorMaximo,
                            };
                        }
                        break;

                    case PropiedadTipos.ENTERO_LARGO:
                        if (modelo.PropiedadTipoEspecificacionesEntero != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesEnteroLargo
                            {
                                ValorMinimo = modelo.PropiedadTipoEspecificacionesEntero.ValorMinimo,
                                ValorMaximo = modelo.PropiedadTipoEspecificacionesEntero.ValorMaximo,
                            };
                        }
                        break;

                    case PropiedadTipos.DECIMAL:
                        if (modelo.PropiedadTipoEspecificacionesNumero != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesNumero
                            {
                                ValorMinimo = (double?)modelo.PropiedadTipoEspecificacionesNumero.ValorMinimo,
                                ValorMaximo = (double?)modelo.PropiedadTipoEspecificacionesNumero.ValorMaximo,
                                CantDecimales = modelo.PropiedadTipoEspecificacionesNumero.CantDecimales ?? 0,
                            };
                        }
                        break;

                    case PropiedadTipos.DECIMAL_LARGO:
                        if (modelo.PropiedadTipoEspecificacionesNumero != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesNumeroLargo
                            {
                                ValorMinimo = modelo.PropiedadTipoEspecificacionesNumero.ValorMinimo,
                                ValorMaximo = modelo.PropiedadTipoEspecificacionesNumero.ValorMaximo,
                                CantDecimales = modelo.PropiedadTipoEspecificacionesNumero.CantDecimales ?? 0,
                            };
                        }
                        break;
                }
            }

            return especificaciones;
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