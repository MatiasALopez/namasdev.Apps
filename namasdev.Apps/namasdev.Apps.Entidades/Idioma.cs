using System.Collections.Generic;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class Idioma : Entidad<string>
    {
        public string Nombre { get; set; }
        public bool UsaArticulos { get; set; }

        public virtual List<IdiomaArticulo> Articulos { get; set; }
        public virtual IdiomaTextos Textos { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
