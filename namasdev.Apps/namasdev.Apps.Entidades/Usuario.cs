using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class Usuario : EntidadCreadoModificadoBorrado<string>
    {
        public const string DISPLAY_NAME_EMAIL = "Email";
        public const string DISPLAY_NAME_NOMBRES = "Nombres";
        public const string DISPLAY_NAME_APELLIDOS = "Apellidos";
        public const string DISPLAY_NAME_NOMBRECOMPLETO = "NombreCompleto";

        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
       
        public virtual List<AspNetRole> Roles { get; set; }

        public bool PerteneceAlRol(string rolNombre)
        {
            return Roles != null
                && Roles.Any(r => string.Equals(r.Name, rolNombre, StringComparison.CurrentCultureIgnoreCase));
        }

        public override string ToString()
        {
            return NombreCompleto;
        }
    }
}
