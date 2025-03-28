
namespace namasdev.Apps.Web.Portal.Helpers
{
    public class FormatoHelper
    {
        public static string FormatoValorConSufijo(decimal? valor)
        {
            return valor.HasValue
                ? FormatoValorConSufijo(valor.Value)
                : null;
        }

        public static string FormatoValorConSufijo(decimal valor)
        {
            return $"{valor}m";
        }
    }
}