using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.Entidades
{
    public class EntidadItemModel
    {
        public Guid AplicacionVersionId { get; set; }
        public Guid EntidadId { get; set; }

        [Display(Name = EntidadMetadata.Nombre.ETIQUETA)]
        public string Nombre { get; set; }

        [Display(Name = EntidadMetadata.Etiqueta.ETIQUETA)]
        public string Etiqueta { get; set; }
    }
}