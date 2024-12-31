using Newtonsoft.Json;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio
{
    public class NegocioBase
    {
        protected string SerializarPropiedadTipoEspecificaciones(IPropiedadTipoEspecificaciones especificaciones)
        {
            return especificaciones != null
                ? JsonConvert.SerializeObject(especificaciones)
                : null;
        }
    }
}
