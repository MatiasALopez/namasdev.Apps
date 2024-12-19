using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class Aplicacion : EntidadCreadoModificadoBorrado<Guid>
    {
        public const string DISPLAY_NAME_NOMBRE = "Nombre";

        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
