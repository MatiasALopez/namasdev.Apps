using System;

namespace namasdev.Apps.Negocio.DTO.Entidades
{
    public class AgregarParametros : ParametrosConUsuarioBase
    {
        public Guid AplicacionVersionId { get; set; }
        public string Nombre { get; set; }
        public string NombrePlural { get; set; }
        public string Etiqueta { get; set; }
        public string EtiquetaPlural { get; set; }
    }
}
