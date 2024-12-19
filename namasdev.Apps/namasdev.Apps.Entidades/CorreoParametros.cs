
using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class CorreoParametros : Entidad<short>
    {
        public const short ID_ACTIVAR_USUARIO = 1;
        public const short ID_RESETEAR_PASSWORD = 2;
        public const short ID_EMPLEADOS_PUESTOS_DIARIOS_ASIGNACION_FALTANTE = 3;

        public string Contenido { get; set; }
        public string Asunto { get; set; }
        public string CopiaOculta { get; set; }

        public override string ToString()
        {
            return Asunto;
        }
    }
}
