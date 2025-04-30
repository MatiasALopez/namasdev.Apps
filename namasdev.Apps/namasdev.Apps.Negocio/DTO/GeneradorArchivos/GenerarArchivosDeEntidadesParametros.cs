using System.Collections.Generic;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio.DTO.GeneradorArchivos
{
    public class GenerarArchivosDeEntidadesParametros : GenerarArchivosParametrosBase
    {
        public IEnumerable<Entidad> Entidades { get; set; }
    }
}
