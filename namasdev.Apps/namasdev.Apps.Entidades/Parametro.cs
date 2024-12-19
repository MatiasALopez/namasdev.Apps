
namespace namasdev.Apps.Entidades
{
    public class Parametro
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
