using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class PropiedadTipoEspecificacionesTextoViewModel
    {
        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.Propiedades.TamañoMinimo.ETIQUETA)]
        public short? TamañoMinimo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.Propiedades.TamañoMaximo.ETIQUETA)]
        public short? TamañoMaximo { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.Propiedades.TamañoExacto.ETIQUETA)]
        public short? TamañoExacto { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.Propiedades.RegEx.ETIQUETA)]
        public string RegEx { get; set; }

        [Display(Name = PropiedadTipoEspecificacionesTextoMetadata.Propiedades.EsMultilinea.ETIQUETA)]
        public bool EsMultilinea { get; set; }
    }
}