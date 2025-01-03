using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Versiones
{
    public class VersionViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.AplicacionId.ETIQUETA)]
        public string AplicacionNombre { get; set; }

        public Guid? AplicacionVersionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.Nombre.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !AplicacionVersionId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(AplicacionVersionMetadata.ETIQUETA));
            }
        }
    }
}