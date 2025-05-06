using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Types;

namespace namasdev.Apps.Web.Portal.Helpers
{
    public class FormatoHelper
    {
        public static string ClaseGenerica(string claseNombre, params string[] tiposNombres)
        {
            return $"{claseNombre}<{Formateador.Lista(tiposNombres, ",")}>";
        }

        public static string ListaParametros(Dictionary<string, string> lista,
            bool excluirVacios = true)
        {
            if (lista == null || lista.Count == 0)
            {
                return string.Empty;
            }

            var res = new List<string>();
            foreach (var item in lista)
            {
                if (excluirVacios && string.IsNullOrWhiteSpace(item.Value))
                {
                    continue;
                }
                res.Add($"{item.Key}: {item.Value}");
            }

            return Formateador.Lista(res, separador: ", ");
        }

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