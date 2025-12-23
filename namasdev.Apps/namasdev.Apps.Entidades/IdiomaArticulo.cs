using System;

using namasdev.Core.Entity;

using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Entidades
{
    public partial class IdiomaArticulo : Entidad<string>
    {
        public string IdiomaId { get; set; }
        public string Nombre { get; set; }

        public string Texto
        {
            get { return Nombre.ToLower(); }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
