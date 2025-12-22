using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class Idioma : Entidad<string>
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
