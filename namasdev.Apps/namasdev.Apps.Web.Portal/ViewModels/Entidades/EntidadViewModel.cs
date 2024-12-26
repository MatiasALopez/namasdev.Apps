﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Entidades
{
    public class EntidadViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionMetadata.NOMBRE)]
        public string AplicacionNombre { get; set; }

        [Display(Name = EntidadMetadata.AplicacionVersionId.DISPLAY_NAME)]
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.AplicacionVersionId.DISPLAY_NAME)]
        public string AplicacionVersionNombre { get; set; }

        public Guid? EntidadId { get; set; }

        [Display(Name = EntidadMetadata.Nombre.DISPLAY_NAME),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !EntidadId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadMetadata.NOMBRE));
            }
        }
    }
}