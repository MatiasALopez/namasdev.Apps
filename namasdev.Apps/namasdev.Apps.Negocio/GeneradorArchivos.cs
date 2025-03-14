using System.IO;

using RazorEngine;
using RazorEngine.Templating;

namespace namasdev.Apps.Negocio
{
    public class GeneradorArchivos
    {
        public void GenerarArchivoDesdeTemplate(string pathTemplate, object modelo, string pathDestino)
        {
            string template = File.ReadAllText(pathTemplate);
            string contenido = Engine.Razor.RunCompile(template, Path.GetFileName(pathTemplate), null, modelo);
            File.WriteAllText(pathDestino, contenido);
        }
    }
}
