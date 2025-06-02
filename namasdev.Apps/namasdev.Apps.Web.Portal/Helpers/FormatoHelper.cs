using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Valores;
using namasdev.Core.Types;

namespace namasdev.Apps.Web.Portal.Helpers
{
    public class FormatoHelper
    {
        public static string ExpresionCheckNoNull(string nombre, PropiedadTipo tipo)
        {
            if (tipo.CLRTypeEsNullable)
            {
                return $"!{nombre}.HasValue";
            }
            
            if (tipo.Id == PropiedadTipos.TEXTO)
            {
                return $"!string.IsNullOrWhiteSpace({nombre})";
            }

            return $"{nombre} != null";
        }

        public static string EntreComillas(string texto)
        {
            return $"\"{texto}\"";
        }

        public static string Generic(string nombre, params string[] tiposNombres)
        {
            return $"{nombre}<{Formateador.Lista(tiposNombres, ", ")}>";
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
                ? $"{prefijo}new {{ {Formateador.Lista(atributos.Select(a => $"@{a.Key} = {EntreComillas(a.Value)}"), separador: ", ")} }}"
                : "";
        }
    }
}