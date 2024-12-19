using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using namasdev.Core.Validation;
using namasdev.Web.Models;

namespace namasdev.Apps.Web.Portal.ViewModels.Aplicaciones
{
    public class AplicacionViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid? AplicacionId { get; set; }

        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !AplicacionId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido("Aplicación"));
            }
        }
    }
}