using System;

namespace namasdev.Apps.Negocio.DTO.Entidades
{
    public class ActualizarParametros : ParametrosEntidadBase<Guid>
    {
        public string Nombre { get; set; }
        public string NombrePlural { get; set; }
        public string Etiqueta { get; set; }
        public string EtiquetaPlural { get; set; }
    }
}
