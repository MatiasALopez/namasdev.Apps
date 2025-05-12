using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class EntidadesPropiedadesMapper
    {
        public static List<EntidadPropiedadItemModel> MapearEntidadesAModelos(IEnumerable<EntidadPropiedad> entidades, IEnumerable<EntidadClave> claves)
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
                    Orden = e.Orden,
                    EsClave = claves != null && claves.Any(c => c.EntidadPropiedadId == e.Id),
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
                        if (modelo.PropiedadTipoEspecificacionesDecimal != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesDecimal
                            {
                                ValorMinimo = modelo.PropiedadTipoEspecificacionesDecimal.ValorMinimo,
                                ValorMaximo = modelo.PropiedadTipoEspecificacionesDecimal.ValorMaximo,
                                DigitosEnteros = modelo.PropiedadTipoEspecificacionesDecimal.DigitosEnteros.Value,
                                DigitosDecimales = modelo.PropiedadTipoEspecificacionesDecimal.DigitosDecimales.Value,
                            };
                        }
                        break;

                    case PropiedadTipos.DECIMAL_FLOTANTE:
                        if (modelo.PropiedadTipoEspecificacionesDecimal != null)
                        {
                            especificaciones = new PropiedadTipoEspecificacionesDecimalFlotante
                            {
                                ValorMinimo = (double?)modelo.PropiedadTipoEspecificacionesDecimal.ValorMinimo,
                                ValorMaximo = (double?)modelo.PropiedadTipoEspecificacionesDecimal.ValorMaximo,
                                DigitosEnteros = modelo.PropiedadTipoEspecificacionesDecimal.DigitosEnteros.Value,
                                DigitosDecimales = modelo.PropiedadTipoEspecificacionesDecimal.DigitosDecimales.Value,
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

            var modelo = new EntidadPropiedadViewModel
            {
                EntidadPropiedadId = entidad.Id,
                EntidadId = entidad.EntidadId,
                Nombre = entidad.Nombre,
                Etiqueta = entidad.Etiqueta,
                PropiedadTipoId = entidad.PropiedadTipoId,
                CalculadaFormula = entidad.CalculadaFormula,
                Orden = entidad.Orden,
                PermiteNull = entidad.PermiteNull,
                GeneradaAlCrear = entidad.GeneradaAlCrear,
                Editable = entidad.Editable,
            };

            var especificacionesTexto = entidad.EspecificacionesTexto;
            if (especificacionesTexto != null)
            {
                modelo.PropiedadTipoEspecificacionesTexto = new PropiedadTipoEspecificacionesTextoViewModel
                {
                    TamañoMinimo = entidad.EspecificacionesTexto.TamañoMinimo,
                    TamañoMaximo = especificacionesTexto.TamañoMaximo,
                    TamañoExacto = especificacionesTexto.TamañoExacto,
                    RegEx = especificacionesTexto.RegEx,
                };
            }

            var especificacionesEntero = entidad.EspecificacionesEntero;
            if (especificacionesEntero != null)
            {
                modelo.PropiedadTipoEspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel
                {
                    ValorMinimo = especificacionesEntero.ValorMinimo,
                    ValorMaximo = especificacionesEntero.ValorMaximo,
                };
            }

            var especificacionesEnteroLargo = entidad.EspecificacionesEnteroLargo;
            if (especificacionesEnteroLargo != null)
            {
                modelo.PropiedadTipoEspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel
                {
                    ValorMinimo = especificacionesEnteroLargo.ValorMinimo,
                    ValorMaximo = especificacionesEnteroLargo.ValorMaximo,
                };
            }

            var especificacionesDecimal = entidad.EspecificacionesDecimal;
            if (especificacionesDecimal != null)
            {
                modelo.PropiedadTipoEspecificacionesDecimal = new PropiedadTipoEspecificacionesDecimalViewModel
                {
                    ValorMinimo = especificacionesDecimal.ValorMinimo,
                    ValorMaximo = especificacionesDecimal.ValorMaximo,
                    DigitosEnteros = especificacionesDecimal.DigitosEnteros,
                    DigitosDecimales = especificacionesDecimal.DigitosDecimales,
                };
            }

            var especificacionesDecimalFlotante = entidad.EspecificacionesDecimalFlotante;
            if (especificacionesDecimalFlotante != null)
            {
                modelo.PropiedadTipoEspecificacionesDecimal = new PropiedadTipoEspecificacionesDecimalViewModel
                {
                    ValorMinimo = (decimal?)especificacionesDecimalFlotante.ValorMinimo,
                    ValorMaximo = (decimal?)especificacionesDecimalFlotante.ValorMaximo,
                    DigitosEnteros = especificacionesDecimalFlotante.DigitosEnteros,
                    DigitosDecimales = especificacionesDecimalFlotante.DigitosDecimales,
                };
            }

            return modelo;
        }
    }
}