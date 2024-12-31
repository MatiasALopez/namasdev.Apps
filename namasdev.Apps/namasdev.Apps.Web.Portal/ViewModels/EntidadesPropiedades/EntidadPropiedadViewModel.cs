using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadPropiedadViewModel : IValidatableObject
    {
        public EntidadPropiedadViewModel()
        {
            PropiedadTipoEspecificacionesTexto = new PropiedadTipoEspecificacionesTextoViewModel();
            PropiedadTipoEspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel();
            PropiedadTipoEspecificacionesNumero = new PropiedadTipoEspecificacionesNumeroViewModel();
        }

        public PaginaModo PaginaModo { get; set; }

        public Guid EntidadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.EntidadId.DISPLAY_NAME)]
        public string EntidadNombre { get; set; }

        public Guid? EntidadPropiedadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Nombre.DISPLAY_NAME),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Etiqueta.DISPLAY_NAME),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Etiqueta { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PropiedadTipoId.DISPLAY_NAME)]
        public short? PropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PermiteNull.DISPLAY_NAME)]
        public bool? PermiteNull { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Orden.DISPLAY_NAME)]
        public short? Orden { get; set; }

        [Display(Name = EntidadPropiedadMetadata.GeneradaAlCrear.DISPLAY_NAME)]
        public bool? GeneradaAlCrear { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Editable.DISPLAY_NAME)]
        public bool? Editable { get; set; }

        [Display(Name = EntidadPropiedadMetadata.CalculadaFormula.DISPLAY_NAME)]
        public string CalculadaFormula { get; set; }

        public PropiedadTipoEspecificacionesTextoViewModel PropiedadTipoEspecificacionesTexto { get; set; }
        public PropiedadTipoEspecificacionesEnteroViewModel PropiedadTipoEspecificacionesEntero { get; set; }
        public PropiedadTipoEspecificacionesNumeroViewModel PropiedadTipoEspecificacionesNumero { get; set; }

        public SelectList TiposSelectList { get; set; }
        public SelectList SiNoSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !EntidadPropiedadId.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadPropiedadMetadata.NOMBRE));
            }
        }
    }
}