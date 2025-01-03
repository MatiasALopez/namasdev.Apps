using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadPropiedadViewModel : IValidatableObject
    {
        public EntidadPropiedadViewModel()
        {
            PropiedadTipoEspecificacionesTexto = new PropiedadTipoEspecificacionesTextoViewModel();
            PropiedadTipoEspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel();
            PropiedadTipoEspecificacionesNumero = new PropiedadTipoEspecificacionesNumeroViewModel();
        }

        public PaginaModo PaginaModo { get; set; }

        public Guid EntidadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.EntidadId.ETIQUETA)]
        public string EntidadNombre { get; set; }

        public Guid? EntidadPropiedadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Nombre.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Etiqueta.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Etiqueta { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PropiedadTipoId.ETIQUETA)]
        public short? PropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PermiteNull.ETIQUETA)]
        public bool? PermiteNull { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Orden.ETIQUETA)]
        public short? Orden { get; set; }

        [Display(Name = EntidadPropiedadMetadata.GeneradaAlCrear.ETIQUETA)]
        public bool? GeneradaAlCrear { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Editable.ETIQUETA)]
        public bool? Editable { get; set; }

        [Display(Name = EntidadPropiedadMetadata.CalculadaFormula.ETIQUETA)]
        public string CalculadaFormula { get; set; }

        public PropiedadTipoEspecificacionesTextoViewModel PropiedadTipoEspecificacionesTexto { get; set; }
        public PropiedadTipoEspecificacionesEnteroViewModel PropiedadTipoEspecificacionesEntero { get; set; }
        public PropiedadTipoEspecificacionesNumeroViewModel PropiedadTipoEspecificacionesNumero { get; set; }

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
                    case PropiedadTipos.DECIMAL_LARGO:
                        if (!PropiedadTipoEspecificacionesNumero.DigitosEnteros.HasValue)
                        {
                            yield return new ValidationResult(Validador.MensajeRequerido(PropiedadTipoEspecificacionesNumeroMetadata.DigitosEnteros.ETIQUETA), new[] { $"{nameof(PropiedadTipoEspecificacionesNumero)}.{nameof(PropiedadTipoEspecificacionesNumero.DigitosEnteros)}" });
                        }
                        if (!PropiedadTipoEspecificacionesNumero.DigitosDecimales.HasValue)
                        {
                            yield return new ValidationResult(Validador.MensajeRequerido(PropiedadTipoEspecificacionesNumeroMetadata.DigitosDecimales.ETIQUETA), new[] { $"{nameof(PropiedadTipoEspecificacionesNumero)}.{nameof(PropiedadTipoEspecificacionesNumero.DigitosDecimales)}" });
                        }
                        break;
                }
            }
        }
    }
}