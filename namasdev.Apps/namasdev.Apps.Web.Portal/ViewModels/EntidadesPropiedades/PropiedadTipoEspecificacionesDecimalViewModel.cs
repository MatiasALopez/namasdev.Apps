using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesDecimalViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.ValorMinimo.ETIQUETA)]
        public decimal? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.ValorMaximo.ETIQUETA)]
        public decimal? ValorMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.DigitosEnteros.ETIQUETA)]
        public short? DigitosEnteros { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesDecimalMetadata.DigitosDecimales.ETIQUETA)]
        public short? DigitosDecimales { get; set; }
    }
}