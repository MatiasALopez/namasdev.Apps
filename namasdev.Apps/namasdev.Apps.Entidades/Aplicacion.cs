using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class Aplicacion : EntidadCreadoModificadoBorrado<Guid>
    {
        public string Nombre { get; set; }
        public string IdiomaId { get; set; }

        public virtual Idioma Idioma { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
