using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Entidades
{
    public class EntidadViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid? Id { get; set; }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionMetadata.ETIQUETA)]
        public string AplicacionNombre { get; set; }

        [Display(Name = EntidadMetadata.Propiedades.AplicacionVersionId.ETIQUETA)]
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.Propiedades.AplicacionVersionId.ETIQUETA)]
        public string AplicacionVersionNombre { get; set; }

        [Display(Name = EntidadMetadata.Propiedades.Nombre.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        [Display(Name = EntidadMetadata.Propiedades.NombrePlural.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string NombrePlural { get; set; }

        [Display(Name = EntidadMetadata.Propiedades.Etiqueta.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Etiqueta { get; set; }

        [Display(Name = EntidadMetadata.Propiedades.EtiquetaPlural.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string EtiquetaPlural { get; set; }

        #region Propiedades default

        [Display(Name = EntidadPropiedadesDefaultMetadata.Propiedades.IDPropiedadTipoId.ETIQUETA)]
        public short? PropiedadesDefaultIDPropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadesDefaultMetadata.Propiedades.AuditoriaCreado.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool PropiedadesDefaultAuditoriaCreado { get; set; }

        [Display(Name = EntidadPropiedadesDefaultMetadata.Propiedades.AuditoriaUltimaModificacion.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool PropiedadesDefaultAuditoriaUltimaModificacion { get; set; }

        [Display(Name = EntidadPropiedadesDefaultMetadata.Propiedades.AuditoriaBorrado.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool PropiedadesDefaultAuditoriaBorrado { get; set; }

        #endregion

        public SelectList PropiedadTiposSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !Id.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadMetadata.ETIQUETA));
            }
        }
    }
}