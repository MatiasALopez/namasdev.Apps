using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Web.ViewModels;
using namasdev.Apps.Web.Portal.Models.Usuarios;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Usuarios
{
    public class UsuariosViewModel : ListadoConPaginacionViewModel<UsuarioItemModel>
    {
        public UsuariosViewModel()
        {
            ItemsPorPagina = 10;        
        }

        [Display(Name = "Búsqueda (Nombre y apellido, Correo electrónico)")]
        public string Busqueda { get; set; }

        [Display(Name = AspNetRoleMetadata.NOMBRE)]
        public string Rol { get; set; }

        public bool? Activado { get; set; }

        public SelectList RolesSelectList { get; set; }
    }
}