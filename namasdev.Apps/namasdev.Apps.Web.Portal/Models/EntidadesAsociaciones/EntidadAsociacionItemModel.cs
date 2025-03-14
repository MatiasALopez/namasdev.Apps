using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesAsociaciones
{
    public class EntidadAsociacionItemModel
    {
        public Guid EntidadAsociacionId { get; set; }
        public Guid EntidadId { get; set; }

        
    }
}