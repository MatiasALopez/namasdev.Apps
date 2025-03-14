using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesAsociaciones
{
    public class EntidadAsociacionViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid EntidadId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }

        public Guid? EntidadAsociacionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !EntidadAsociacionId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadAsociacionMetadata.ETIQUETA));
            }
        }
    }
}