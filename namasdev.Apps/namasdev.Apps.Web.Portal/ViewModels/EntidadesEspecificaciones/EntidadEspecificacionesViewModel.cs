using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesEspecificaciones
{
    public class EntidadEspecificacionesViewModel : IValidatableObject
    {
        public Guid? Id { get; set; }

        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }

        [Display(Name = EntidadEspecificacionesMetadata.Propiedades.ArticuloId.ETIQUETA)]
        public short? ArticuloId { get; set; }

        [Display(Name = EntidadEspecificacionesMetadata.Propiedades.IDPropiedadTipoId.ETIQUETA)]
        public short? IDPropiedadTipoId { get; set; }

        [Display(Name = EntidadEspecificacionesMetadata.Propiedades.EsSoloLectura.ETIQUETA)]
        public bool EsSoloLectura { get; set; }

        [Display(Name = EntidadEspecificacionesMetadata.Propiedades.BajaTipoId.ETIQUETA)]
        public short? BajaTipoId { get; set; }

        [Display(Name = EntidadEspecificacionesMetadata.Propiedades.AuditoriaCreado.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool AuditoriaCreado { get; set; }

        [Display(Name = EntidadEspecificacionesMetadata.Propiedades.AuditoriaUltimaModificacion.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool AuditoriaUltimaModificacion { get; set; }

        public SelectList ArticulosSelectList { get; set; }
        public SelectList PropiedadTiposSelectList { get; set; }
        public SelectList BajaTiposSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Id.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadEspecificacionesMetadata.ETIQUETA));
            }
        }
    }
}