
using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class CorreoParametros : Entidad<short>
    {
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public string CopiaOculta { get; set; }

        public override string ToString()
        {
            return Asunto;
        }
    }
}
