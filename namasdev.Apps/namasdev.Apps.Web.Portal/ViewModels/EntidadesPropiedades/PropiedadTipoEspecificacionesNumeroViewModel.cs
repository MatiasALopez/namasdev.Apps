using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesNumeroViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.ValorMinimo.ETIQUETA)]
        public decimal? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.ValorMaximo.ETIQUETA)]
        public decimal? ValorMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.DigitosEnteros.ETIQUETA)]
        public short? DigitosEnteros { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesNumeroMetadata.DigitosDecimales.ETIQUETA)]
        public short? DigitosDecimales { get; set; }
    }
}