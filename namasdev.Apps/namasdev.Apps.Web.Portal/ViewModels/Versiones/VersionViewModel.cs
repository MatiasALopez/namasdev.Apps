using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Versiones
{
    public class VersionViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.Propiedades.AplicacionId.ETIQUETA)]
        public string AplicacionNombre { get; set; }

        public Guid? AplicacionVersionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.Propiedades.Nombre.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        [Display(Name = AplicacionVersionMetadata.Propiedades.AplicacionVersionIdBase.ETIQUETA)]
        public Guid? AplicacionVersionIdBase { get; set; }

        public SelectList VersionesSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (PaginaModo)
            {
                case PaginaModo.Editar:
                    if (!AplicacionVersionId.HasValue)
                    {
                        yield return new ValidationResult(Validador.MensajeRequerido(AplicacionVersionMetadata.ETIQUETA));
                    }
                    break;
            }
            
        }
    }
}