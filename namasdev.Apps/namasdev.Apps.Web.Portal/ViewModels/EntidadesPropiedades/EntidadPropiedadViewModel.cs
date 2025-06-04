using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadPropiedadViewModel : IValidatableObject
    {
        public EntidadPropiedadViewModel()
        {
            PropiedadTipoEspecificacionesTexto = new PropiedadTipoEspecificacionesTextoViewModel();
            PropiedadTipoEspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel();
            PropiedadTipoEspecificacionesDecimal = new PropiedadTipoEspecificacionesDecimalViewModel();
        }

        public PaginaModo PaginaModo { get; set; }
        public Guid EntidadId { get; set; }

        public Guid? EntidadPropiedadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.Nombre.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.Etiqueta.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Etiqueta { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.PropiedadTipoId.ETIQUETA)]
        public short? PropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.PermiteNull.ETIQUETA)]
        public bool? PermiteNull { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.GeneradaAlCrear.ETIQUETA)]
        public bool? GeneradaAlCrear { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.Editable.ETIQUETA)]
        public bool? Editable { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.CalculadaFormula.ETIQUETA)]
        public string CalculadaFormula { get; set; }

        public PropiedadTipoEspecificacionesTextoViewModel PropiedadTipoEspecificacionesTexto { get; set; }
        public PropiedadTipoEspecificacionesEnteroViewModel PropiedadTipoEspecificacionesEntero { get; set; }
        public PropiedadTipoEspecificacionesDecimalViewModel PropiedadTipoEspecificacionesDecimal { get; set; }

        public SelectList TiposSelectList { get; set; }
        public SelectList SiNoSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !EntidadPropiedadId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadPropiedadMetadata.ETIQUETA));
            }

            if (PropiedadTipoId.HasValue)
            {
                switch (PropiedadTipoId.Value)
                {
                    case PropiedadTipos.DECIMAL:
                    case PropiedadTipos.DECIMAL_FLOTANTE:
                        if (!PropiedadTipoEspecificacionesDecimal.DigitosEnteros.HasValue)
                        {
                            yield return new ValidationResult(Validador.MensajeRequerido(PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.DigitosEnteros.ETIQUETA), new[] { $"{nameof(PropiedadTipoEspecificacionesDecimal)}.{nameof(PropiedadTipoEspecificacionesDecimal.DigitosEnteros)}" });
                        }
                        if (!PropiedadTipoEspecificacionesDecimal.DigitosDecimales.HasValue)
                        {
                            yield return new ValidationResult(Validador.MensajeRequerido(PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.DigitosDecimales.ETIQUETA), new[] { $"{nameof(PropiedadTipoEspecificacionesDecimal)}.{nameof(PropiedadTipoEspecificacionesDecimal.DigitosDecimales)}" });
                        }
                        break;
                }
            }
        }
    }
}