using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class AuditoriaTipo : Entidad<short>
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
