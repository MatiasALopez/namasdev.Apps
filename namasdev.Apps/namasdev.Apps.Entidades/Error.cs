using System;
using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class Error : Entidad<Guid>
    {
        public string Mensaje { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Argumentos { get; set; }
        public DateTime FechaHora { get; set; }
        public string UserId { get; set; }

        public override string ToString()
        {
            return Mensaje;
        }
    }
}
