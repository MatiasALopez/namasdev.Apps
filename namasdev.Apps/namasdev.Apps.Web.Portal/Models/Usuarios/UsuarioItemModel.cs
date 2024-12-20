using namasdev.Apps.Entidades.Metadata;
using System.ComponentModel.DataAnnotations;

namespace namasdev.Apps.Web.Portal.Models.Usuarios
{
    public class UsuarioItemModel
    {
        public string UsuarioId { get; set; }

        [Display(Name = UsuarioMetadata.Nombres.DISPLAY_NAME)]
        public string Nombres { get; set; }

        [Display(Name = UsuarioMetadata.Apellidos.DISPLAY_NAME)]
        public string Apellidos { get; set; }

        [Display(Name = UsuarioMetadata.Email.DISPLAY_NAME)]
        public string Email { get; set; }

        [Display(Name = AspNetRoleMetadata.NOMBRE)]
        public string Rol { get; set; }

        public bool Activado { get; set; }
    }
}