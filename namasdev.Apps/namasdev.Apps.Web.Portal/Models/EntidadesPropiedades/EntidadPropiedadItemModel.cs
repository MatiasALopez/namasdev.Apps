using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesPropiedades
{
    public class EntidadPropiedadItemModel
    {
        public Guid EntidadPropiedadId { get; set; }
        public Guid EntidadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Nombre.ETIQUETA)]
        public string Nombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PropiedadTipoId.ETIQUETA)]
        public short PropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PropiedadTipoId.ETIQUETA)]
        public string PropiedadTipoNombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PermiteNull.ETIQUETA)]
        public bool PermiteNull { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Orden.ETIQUETA)]
        public short? Orden { get; set; }
    }
}