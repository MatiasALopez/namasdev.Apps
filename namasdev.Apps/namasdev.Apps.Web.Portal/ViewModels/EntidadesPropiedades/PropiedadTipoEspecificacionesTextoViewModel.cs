using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesTextoViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.TamañoMinimo.ETIQUETA)]
        public short? TamañoMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.TamañoMaximo.ETIQUETA)]
        public short? TamañoMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.TamañoExacto.ETIQUETA)]
        public short? TamañoExacto { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.RegEx.ETIQUETA)]
        public string RegEx { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.EsMultilinea.ETIQUETA)]
        public bool EsMultilinea { get; set; }
    }
}