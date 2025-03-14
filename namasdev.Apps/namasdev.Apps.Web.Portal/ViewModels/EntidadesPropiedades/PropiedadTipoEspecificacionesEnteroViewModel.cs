using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesEnteroViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesEnteroMetadata.Propiedades.ValorMinimo.ETIQUETA)]
        public long? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesEnteroMetadata.Propiedades.ValorMaximo.ETIQUETA)]
        public long? ValorMaximo { get; set; }
    }
}