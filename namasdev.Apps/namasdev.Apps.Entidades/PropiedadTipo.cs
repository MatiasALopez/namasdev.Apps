using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class PropiedadTipo : Entidad<short>
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
