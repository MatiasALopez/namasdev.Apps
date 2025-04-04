using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Types;

namespace namasdev.Apps.Web.Portal.Helpers
{
    public class FormatoHelper
    {
        public static string ValorConSufijo(decimal? valor)
        {
            return valor.HasValue
                ? ValorConSufijo(valor.Value)
                : null;
        }

        public static string ValorConSufijo(decimal valor)
        {
            return $"{valor}m";
        }

        public static string AtributosHtmlString(Dictionary<string, string> atributos,
            string prefijo = ", ")
        {
            return atributos != null && atributos.Count > 0
                ? $"{prefijo}new {{ {Formateador.Lista(atributos.Select(a => $"@{a.Key} = \"{a.Value}\""), separador: ", ")} }}"
                : "";
        }
    }
}