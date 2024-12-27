using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesTextoViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.TamañoMinimo.DISPLAY_NAME)]
        public short TamañoMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.TamañoMaximo.DISPLAY_NAME)]
        public short TamañoMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.TamañoExacto.DISPLAY_NAME)]
        public short TamañoExacto { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.RegEx.DISPLAY_NAME)]
        public string RegEx { get; set; }
    }
}