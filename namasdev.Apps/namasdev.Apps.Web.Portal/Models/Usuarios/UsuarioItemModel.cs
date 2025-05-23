﻿using namasdev.Apps.Entidades.Metadata;
using System.ComponentModel.DataAnnotations;

namespace namasdev.Apps.Web.Portal.Models.Usuarios
{
    public class UsuarioItemModel
    {
        public string Id { get; set; }

        [Display(Name = UsuarioMetadata.Propiedades.Nombres.ETIQUETA)]
        public string Nombres { get; set; }

        [Display(Name = UsuarioMetadata.Propiedades.Apellidos.ETIQUETA)]
        public string Apellidos { get; set; }

        [Display(Name = UsuarioMetadata.Propiedades.Email.ETIQUETA)]
        public string Email { get; set; }

        [Display(Name = AspNetRoleMetadata.ETIQUETA)]
        public string Rol { get; set; }

        public bool Activado { get; set; }
    }
}