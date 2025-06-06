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
            EspecificacionesTexto = new PropiedadTipoEspecificacionesTextoViewModel();
            EspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel();
            EspecificacionesDecimal = new PropiedadTipoEspecificacionesDecimalViewModel();
        }

        public PaginaModo PaginaModo { get; set; }
        
        public Guid EntidadId { get; set; }
        public Guid? Id { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.Nombre.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.Etiqueta.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Etiqueta { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.PropiedadTipoId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public short? PropiedadTipoId { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.PermiteNull.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool? PermiteNull { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.CalculadaFormula.ETIQUETA)]
        public string CalculadaFormula { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.GeneradaAlCrear.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool? GeneradaAlCrear { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.Editable.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool? Editable { get; set; }

        public PropiedadTipoEspecificacionesTextoViewModel EspecificacionesTexto { get; set; }
        public PropiedadTipoEspecificacionesEnteroViewModel EspecificacionesEntero { get; set; }
        public PropiedadTipoEspecificacionesDecimalViewModel EspecificacionesDecimal { get; set; }

        public bool EsID { get; set; }

        public SelectList TiposSelectList { get; set; }
        public SelectList SiNoSelectList { get; set; }
    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !Id.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadPropiedadMetadata.ETIQUETA));
            }

            if (PropiedadTipoId.HasValue)
            {
                switch (PropiedadTipoId.Value)
                {
                    case PropiedadTipos.DECIMAL:
                    case PropiedadTipos.DECIMAL_FLOTANTE:
                        if (!EspecificacionesDecimal.DigitosEnteros.HasValue)
                        {
                            yield return new ValidationResult(Validador.MensajeRequerido(PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.DigitosEnteros.ETIQUETA), new[] { $"{nameof(EspecificacionesDecimal)}.{nameof(EspecificacionesDecimal.DigitosEnteros)}" });
                        }
                        if (!EspecificacionesDecimal.DigitosDecimales.HasValue)
                        {
                            yield return new ValidationResult(Validador.MensajeRequerido(PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.DigitosDecimales.ETIQUETA), new[] { $"{nameof(EspecificacionesDecimal)}.{nameof(EspecificacionesDecimal.DigitosDecimales)}" });
                        }
                        break;
                }
            }
        }
    }
}
