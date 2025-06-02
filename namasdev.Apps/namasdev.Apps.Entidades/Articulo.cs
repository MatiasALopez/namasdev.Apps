using System;

using namasdev.Core.Entity;

using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Entidades
{
    public partial class Articulo : Entidad<short>
    {
        public string Nombre { get; set; }

        public string Texto
        {
            get { return Nombre.ToLower(); }
        }

        public string SufijoGenero
        {
            get 
            { 
                switch (Id)
                {
                    case Articulos.EL:
                        return "o";

                    case Articulos.LA:
                        return "a";

                    case Articulos.LOS:
                        return "os";

                    case Articulos.LAS:
                        return "as";

                    default:
                        throw new ArgumentException($"Artículo no contemplado ({Id}).");
                }
            }
        }

        public string TextoPudo
        {
            get
            {
                switch (Id)
                {
                    case Articulos.EL:
                    case Articulos.LA:
                        return "pudo";

                    case Articulos.LOS:
                    case Articulos.LAS:
                        return "pudieron";

                    default:
                        throw new ArgumentException($"Artículo no contemplado ({Id}).");
                }
            }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
