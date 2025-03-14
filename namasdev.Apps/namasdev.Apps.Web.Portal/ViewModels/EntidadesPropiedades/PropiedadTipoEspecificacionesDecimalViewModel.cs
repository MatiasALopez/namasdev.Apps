using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesDecimalViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.ValorMinimo.ETIQUETA)]
        public decimal? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.ValorMaximo.ETIQUETA)]
        public decimal? ValorMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.DigitosEnteros.ETIQUETA)]
        public short? DigitosEnteros { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.Propiedades.DigitosDecimales.ETIQUETA)]
        public short? DigitosDecimales { get; set; }
    }
}