using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesImporteViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesImporteMetadata.Propiedades.ValorMinimo.ETIQUETA)]
        public decimal? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesImporteMetadata.Propiedades.ValorMaximo.ETIQUETA)]
        public decimal? ValorMaximo { get; set; }
    }
}