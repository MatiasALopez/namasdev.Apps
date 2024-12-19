using namasdev.Apps.Entidades;
using namasdev.Core.IO;
using System.Collections.Generic;

namespace namasdev.Apps.Negocio
{
    public interface ICorreosNegocio
    {
        void EnviarCorreoActivarCuenta(string email, string nombreYApellido, string activarCuentaUrl);
        void EnviarCorreoResetearPassword(string email, string nombreYApellido, string resetearPasswordUrl);
        void EnviarCorreo(string destinatario, string asunto, string cuerpo, IDictionary<string, string> correoVariables = null, string copiaOculta = null, IEnumerable<Archivo> adjuntos = null);
        void EnviarCorreo(string destinatario, short correoParametrosId, IDictionary<string, string> correoVariables = null, string copiaOculta = null, IEnumerable<Archivo> adjuntos = null);
        void EnviarCorreo(string destinatario, CorreoParametros correoParametros, IDictionary<string, string> correoVariables = null, string copiaOculta = null, IEnumerable<Archivo> adjuntos = null);
    }
}