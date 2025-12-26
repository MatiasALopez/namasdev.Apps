using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class IdiomaArticulo : Entidad<string>
    {
        public string IdiomaId { get; set; }
        public string Nombre { get; set; }

        public virtual Idioma Idioma { get; set; }

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
