using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesPropiedades
{
    public class EntidadPropiedadItemModel
    {
        public Guid Id { get; set; }
        public Guid EntidadId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.Nombre.ETIQUETA)]
        public string Nombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.PropiedadTipoId.ETIQUETA)]
        public short PropiedadTipoId { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.PropiedadTipoId.ETIQUETA)]
        public string TipoNombre { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.PermiteNull.ETIQUETA)]
        public bool PermiteNull { get; set; }

        [Display(Name = EntidadPropiedadMetadata.Propiedades.Orden.ETIQUETA)]
        public short? Orden { get; set; }

        public bool EsClave { get; set; }
        public bool Seleccionado { get; set; }
    }
}