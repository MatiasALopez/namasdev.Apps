﻿using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.Aplicaciones
{
    public class AplicacionItemModel
    {
        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionMetadata.Nombre.ETIQUETA)]
        public string Nombre { get; set; }
    }
}