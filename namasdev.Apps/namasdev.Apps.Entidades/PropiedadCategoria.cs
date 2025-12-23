using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class PropiedadCategoria : Entidad<short>
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
