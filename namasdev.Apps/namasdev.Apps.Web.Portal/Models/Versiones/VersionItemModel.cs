using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.Versiones
{
    public class VersionItemModel
    {
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.AplicacionId.DISPLAY_NAME)]
        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.Nombre.DISPLAY_NAME)]
        public string Nombre { get; set; }
    }
}