using namasdev.Core.Entity;
using System;

namespace namasdev.Apps.Entidades
{
    public partial class Auditoria : Entidad<Guid>
    {
        public string Tabla { get; set; }
        public short AuditoriaTipoId { get; set; }
        public string Detalle { get; set; }
        public string UserId { get; set; }
        public DateTime FechaHora { get; set; }

        public AuditoriaTipo Tipo { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
