using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio
{
    public class NegocioBase
    {
        protected string SerializarPropiedadTipoEspecificaciones(IPropiedadTipoEspecificaciones especificaciones)
        {
            return JsonConvert.SerializeObject(especificaciones);
        }
    }
}
