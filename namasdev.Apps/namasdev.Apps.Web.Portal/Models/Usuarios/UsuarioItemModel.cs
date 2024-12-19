using System.ComponentModel.DataAnnotations;

namespace namasdev.Apps.Web.Portal.Models.Usuarios
{
    public class UsuarioItemModel
    {
        public string UsuarioId { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }

        public bool Activado { get; set; }
    }
}