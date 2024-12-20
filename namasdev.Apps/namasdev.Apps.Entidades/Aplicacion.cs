using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class Aplicacion : EntidadCreadoModificadoBorrado<Guid>
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
