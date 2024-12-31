using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Core.Validation;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesNumeroViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.ValorMinimo.DISPLAY_NAME)]
        public decimal? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.ValorMaximo.DISPLAY_NAME)]
        public decimal? ValorMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.CantDecimales.DISPLAY_NAME),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public int? CantDecimales { get; set; }
    }
}