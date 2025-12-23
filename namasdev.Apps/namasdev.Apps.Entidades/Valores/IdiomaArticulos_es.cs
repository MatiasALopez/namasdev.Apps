using System;

namespace namasdev.Apps.Entidades.Valores
{
    public class IdiomaArticulos_es
    {
        public const string EL = "es1";
        public const string LA = "es2";
        public const string LOS = "es3";
        public const string LAS = "es4";

        public static string SufijoGenero(string id)
        {
            switch (id)
            {
                case EL:
                    return "o";

                case LA:
                    return "a";

                case LOS:
                    return "os";

                case LAS:
                    return "as";

                default:
                    throw new ArgumentException($"Artículo no contemplado ({id}).");
            }
        }

        public static string TextoPudo(string id)
        {
            switch (id)
            {
                case EL:
                case LA:
                    return "pudo";

                case LOS:
                case LAS:
                    return "pudieron";

                default:
                    throw new ArgumentException($"Artículo no contemplado ({id}).");
            }
        }
    }
}
