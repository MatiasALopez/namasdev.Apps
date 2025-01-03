using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.Versiones
{
    public class VersionItemModel
    {
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.AplicacionId.ETIQUETA)]
        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.Nombre.ETIQUETA)]
        public string Nombre { get; set; }
    }
}