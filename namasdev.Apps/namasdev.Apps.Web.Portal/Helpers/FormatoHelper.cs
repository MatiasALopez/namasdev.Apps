using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Types;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Web.Portal.Helpers
{
    public class FormatoHelper
    {
        public static string MetadataPropiedadAtributo(EntidadPropiedad propiedad, string atributo,
            bool incluirNamespace = false)
        {
            return $"{(incluirNamespace ? "namasdev.Apps.Entidades.Metadata." : "")}{propiedad.Entidad.Nombre}Metadata.Propiedades.{propiedad.Nombre}.{atributo}";
        }

        public static string TipoYNombre(string tipo, string nombre)
        {
            return $"{tipo} {nombre}";
        }

        public static string DefinicionAsociacionRegla(string operacion, AsociacionRegla regla)
        {
            if (regla.Id == AsociacionReglas.NO_ACTION)
            {
                return string.Empty;
            }

            string accionExpresion = null;
            switch (regla.Id)
            {
                case AsociacionReglas.CASCADE:
                    accionExpresion = "cascade";
                    break;

                case AsociacionReglas.SET_NULL:
                    accionExpresion = "set null";
                    break;

                case AsociacionReglas.SET_DEFAULT:
                    accionExpresion = "set default";
                    break;
            }

            return $" on {operacion} {accionExpresion}";
        }

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

        public static string ListaParametrosDefiniciones(IEnumerable<EntidadPropiedad> propiedades)
        {
            return Lista(
                propiedades.ToDictionary(p => p.Tipo.CLRType, p => p.NombreCamelCase));
        }

        public static string ListaParametrosValores(Dictionary<string, string> lista,
            bool excluirVacios = true)
        {
            return Lista(lista, 
                separadorKeyValue: ": ", 
                excluirVacios: excluirVacios);
        }

        public static string Lista(Dictionary<string, string> lista, 
            string separadorKeyValue = " ",
            string separadorItems = ", ",
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
                res.Add($"{item.Key}{separadorKeyValue}{item.Value}");
            }

            return Formateador.Lista(res, separador: separadorItems);
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