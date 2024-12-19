using System.Collections.Generic;

using Microsoft.WindowsAzure.Storage;

namespace namasdev.Apps.Datos
{
    public interface IParametrosRepositorio
    {
        string Obtener(string nombre);
        Dictionary<string, string> Obtener(params string[] nombres);
        void Guardar(string nombre, string valor);
        void Guardar(Dictionary<string, string> parametros);
        CloudStorageAccount ObtenerCloudStorageAccount();
    }
}
