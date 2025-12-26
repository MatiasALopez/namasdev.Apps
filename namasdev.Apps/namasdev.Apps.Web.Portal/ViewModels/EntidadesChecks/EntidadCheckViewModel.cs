using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;
using namasdev.Core.Validation;
using namasdev.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesChecks
{
    public class EntidadCheckViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid AplicacionVersionId { get; set; }
        public Guid EntidadId { get; set; }
        public string EntidadTablaNombre { get; set; }

        [Display(Name = EntidadCheckMetadata.Propiedades.Id.ETIQUETA)]
        public Guid? Id { get; set; }
        
        [Display(Name = EntidadCheckMetadata.Propiedades.Nombre.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }
        
        [Display(Name = EntidadCheckMetadata.Propiedades.ExpresionNombre.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Expresion { get; set; }

        public List<EntidadPropiedadItemModel> Propiedades { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !Id.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadIndiceMetadata.ETIQUETA));
            }
        }
    }
}
