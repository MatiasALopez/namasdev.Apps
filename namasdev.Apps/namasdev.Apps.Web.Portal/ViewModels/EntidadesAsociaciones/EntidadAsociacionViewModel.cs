using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesAsociaciones
{
    public class EntidadAsociacionViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public string EntidadNombre { get; set; }
        public Guid OrigenEntidadId { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.Id.ETIQUETA)]
        public Guid? Id { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.Nombre.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public Guid? OrigenEntidadPropiedadId { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.OrigenAsociacionMultiplicidadId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public short? OrigenAsociacionMultiplicidadId { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.DestinoEntidadId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public Guid? DestinoEntidadId { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.DestinoEntidadPropiedadId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public Guid? DestinoEntidadPropiedadId { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.DestinoAsociacionMultiplicidadId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public short? DestinoAsociacionMultiplicidadId { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.TablaAuxiliarNombre.ETIQUETA)]
        public string TablaAuxiliarNombre { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.DeleteAsociacionReglaId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public short? DeleteAsociacionReglaId { get; set; }
        
        [Display(Name = EntidadAsociacionMetadata.Propiedades.UpdateAsociacionReglaId.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public short? UpdateAsociacionReglaId { get; set; }

        public SelectList EntidadesSelectList { get; set; }
        public SelectList OrigenPropiedadesSelectList { get; set; }
        public SelectList DestinoPropiedadesSelectList { get; set; }
        public SelectList MultiplicidadesSelectList { get; set; }
        public SelectList ReglasSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !Id.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadAsociacionMetadata.ETIQUETA));
            }
        }
    }
}
