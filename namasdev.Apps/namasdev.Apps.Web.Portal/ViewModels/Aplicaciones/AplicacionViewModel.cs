using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Aplicaciones
{
    public class AplicacionViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid? AplicacionId { get; set; }

        [Display(Name = AplicacionMetadata.Propiedades.Nombre.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !AplicacionId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(AplicacionMetadata.ETIQUETA));
            }
        }
    }
}