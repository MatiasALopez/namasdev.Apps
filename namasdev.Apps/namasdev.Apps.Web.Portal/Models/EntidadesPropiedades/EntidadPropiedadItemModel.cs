using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesPropiedades
{
    public class EntidadPropiedadItemModel
    {
        public Guid EntidadPropiedadId { get; set; }
        public Guid EntidadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Nombre.DISPLAY_NAME)]
        public string Nombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PropiedadTipoId.DISPLAY_NAME)]
        public short PropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PropiedadTipoId.DISPLAY_NAME)]
        public string PropiedadTipoNombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.PermiteNull.DISPLAY_NAME)]
        public bool PermiteNull { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Orden.DISPLAY_NAME)]
        public short? Orden { get; set; }
    }
}