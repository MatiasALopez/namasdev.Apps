using System;
using System.IO;
using System.IO.Compression;

using RazorEngine;
using RazorEngine.Templating;

using namasdev.Core.IO;
using namasdev.Core.Validation;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio
{
    public interface IGeneradorArchivos
    {
        string GenerarDatosRepositorio(Entidad entidad, Guid? grupoId = null);
        string GenerarDatosSqlConfig(Entidad entidad, Guid? grupoId = null);
        string GenerarEntidadesEntidad(Entidad entidad, Guid? grupoId = null);
        string GenerarEntidadesEntidadMetadata(Entidad entidad, Guid? grupoId = null);
        string GenerarNegocio(Entidad entidad, Guid? grupoId = null);
        string GenerarSQLTabla(Entidad entidad, Guid? grupoId = null);
        string GenerarWebController(Entidad entidad, Guid? grupoId = null);
        string GenerarWebEntidadViewModel(Entidad entidad, Guid? grupoId = null);
        string GenerarWebIndexView(Entidad entidad, Guid? grupoId = null);
        string GenerarWebItemModel(Entidad entidad, Guid? grupoId = null);
        string GenerarWebListaViewModel(Entidad entidad, Guid? grupoId = null);
        string GenerarWebMapper(Entidad entidad, Guid? grupoId = null);
        string GenerarWebViewsMetadata(Entidad entidad, Guid? grupoId = null);
        string GenerarZipTodos(Entidad entidad);
    }

    public class GeneradorArchivos : IGeneradorArchivos
    {
        private string _templatesDirectorio;

        public GeneradorArchivos(string templatesDirectorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(templatesDirectorio, nameof(templatesDirectorio));

            _templatesDirectorio = templatesDirectorio;
        }

        public string GenerarZipTodos(Entidad entidad)
        {
            var grupoId = Guid.NewGuid();

            GenerarSQLTabla(entidad, grupoId: grupoId);
            GenerarEntidadesEntidad(entidad, grupoId: grupoId);
            GenerarEntidadesEntidadMetadata(entidad, grupoId: grupoId);
            GenerarDatosSqlConfig(entidad, grupoId: grupoId);
            GenerarDatosRepositorio(entidad, grupoId: grupoId);
            GenerarNegocio(entidad, grupoId: grupoId);
            GenerarWebItemModel(entidad, grupoId: grupoId);
            GenerarWebEntidadViewModel(entidad, grupoId: grupoId);
            GenerarWebListaViewModel(entidad, grupoId: grupoId);
            GenerarWebMapper(entidad, grupoId: grupoId);
            GenerarWebViewsMetadata(entidad, grupoId: grupoId);
            GenerarWebController(entidad, grupoId: grupoId);
            GenerarWebIndexView(entidad, grupoId: grupoId);

            var zipPath = Path.Combine(GenerarPathDirectorioBase(grupoId), $"{entidad.AplicacionVersion.Aplicacion.Nombre}{ArchivoExtensiones.Application.ZIP}");
            ZipFile.CreateFromDirectory(
                GenerarPathDirectorioArchivos(grupoId),
                zipPath);

            return zipPath;
        }

        public string GenerarSQLTabla(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "SQL_Tabla.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Database", "dbo", "Tables"),
                $"{entidad.NombrePlural}.sql",
                grupoId);
        }

        public string GenerarEntidadesEntidad(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Entidades_Entidad.cshtml",
                entidad,
                $"{entidad.AplicacionVersion.Aplicacion.Nombre}.Entidades",
                $"{entidad.Nombre}.cs",
                grupoId);
        }

        public string GenerarEntidadesEntidadMetadata(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Entidades_EntidadMetadata.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Entidades", "Metadata"),
                $"{entidad.Nombre}Metadata.cs",
                grupoId);
        }

        public string GenerarDatosSqlConfig(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Datos_SqlConfig.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Datos", "Sql", "Config"),
                $"{entidad.Nombre}Config.cs",
                grupoId);
        }

        public string GenerarDatosRepositorio(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Datos_Repositorio.cshtml",
                entidad,
                $"{entidad.AplicacionVersion.Aplicacion.Nombre}.Datos",
                $"{entidad.NombrePlural}Repositorio.cs",
                grupoId);
        }

        public string GenerarNegocio(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_Negocio.cshtml",
                entidad,
                $"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio",
                $"{entidad.NombrePlural}Negocio.cs",
                grupoId);
        }

        public string GenerarWebItemModel(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_ItemModel.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Models", entidad.NombrePlural),
                $"{entidad.Nombre}ItemModel.cs",
                grupoId);
        }

        public string GenerarWebEntidadViewModel(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_EntidadViewModel.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "ViewModels", entidad.NombrePlural),
                $"{entidad.Nombre}ViewModel.cs",
                grupoId);
        }

        public string GenerarWebListaViewModel(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_ListaViewModel.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "ViewModels", entidad.NombrePlural),
                $"{entidad.NombrePlural}ViewModel.cs",
                grupoId);
        }

        public string GenerarWebMapper(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Mapper.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Mappers"),
                $"{entidad.NombrePlural}Mapper.cs",
                grupoId);
        }

        public string GenerarWebViewsMetadata(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_ViewsMetadata.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Metadata", "Views"),
                $"{entidad.NombrePlural}Views.cs",
                grupoId);
        }

        public string GenerarWebController(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Controller.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Controllers"),
                $"{entidad.NombrePlural}Controller.cs",
                grupoId);
        }

        public string GenerarWebIndexView(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_IndexView.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Views", entidad.NombrePlural),
                $"Index.cshtml",
                grupoId,
                reemplazarHtmlRaw: false);
        }

        private string GenerarDesdeTemplate(string template, object modelo, string destinoSubdirectorio, string destinoArchivo,
            Guid? grupoId = null,
            bool reemplazarHtmlRaw = true)
        {
            string directorioGenerados = Path.Combine(GenerarPathDirectorioArchivos(grupoId), destinoSubdirectorio);
            Directory.CreateDirectory(directorioGenerados);

            string pathArchivo = Path.Combine(directorioGenerados, destinoArchivo);

            File.WriteAllText(
                pathArchivo,
                Engine.Razor.RunCompile(
                    ObtenerTemplate(template, reemplazarHtmlRaw),
                    Path.GetFileName(template),
                    null,
                    modelo));

            return pathArchivo;
        }

        private string GenerarPathDirectorioBase(Guid? grupoId)
        {
            return Path.Combine(Path.GetTempPath(), "namasdev.Apps", "Generados", (grupoId ?? Guid.NewGuid()).ToString());
        }

        private string GenerarPathDirectorioArchivos(Guid? grupoId)
        {
            return Path.Combine(GenerarPathDirectorioBase(grupoId), "Archivos");
        }

        private string ObtenerTemplate(string template,
            bool reemplazarHtmlRaw = true)
        {
            var contenido = File.ReadAllText(Path.Combine(_templatesDirectorio, template));
            if (reemplazarHtmlRaw)
            {
                contenido = contenido.Replace("Html.Raw", "Raw");
            }
            return contenido;
        }
    }
}
