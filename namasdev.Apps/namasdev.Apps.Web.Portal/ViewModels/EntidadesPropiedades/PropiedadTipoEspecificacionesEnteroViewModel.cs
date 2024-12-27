using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesEnteroViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesEnteroMetadata.ValorMinimo.DISPLAY_NAME)]
        public long? ValorMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesEnteroMetadata.ValorMaximo.DISPLAY_NAME)]
        public long? ValorMaximo { get; set; }
    }
}