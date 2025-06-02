namespace namasdev.Apps.Entidades.Valores
{
    public class Articulos
    {
        public const short EL = 1;
        public const short LA = 2;
        public const short LOS = 3;
        public const short LAS = 4;

        public static short ObtenerIdParaPalabra(string palabra)
        {
            if (palabra.EndsWith("as"))
            {
                return LAS;
            }
            
            if (palabra.EndsWith("s"))
            {
                return LOS;
            }
            
            if (palabra.EndsWith("a"))
            {
                return LA;
            }

            return EL;
        }
    }
}
