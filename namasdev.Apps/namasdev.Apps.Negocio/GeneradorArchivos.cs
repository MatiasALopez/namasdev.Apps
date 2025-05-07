using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

using RazorEngine;
using RazorEngine.Templating;

using namasdev.Core.IO;
using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.GeneradorArchivos;
using namasdev.Core.Exceptions;

namespace namasdev.Apps.Negocio
{
    public interface IGeneradorArchivos
    {
        string GenerarArchivosDeEntidad(GenerarArchivosDeEntidadParametros parametros);
        string GenerarArchivosDeEntidades(GenerarArchivosDeEntidadesParametros parametros);
    }

    public class GeneradorArchivos : IGeneradorArchivos
    {
        private string _templatesDirectorio;

        public GeneradorArchivos(string templatesDirectorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(templatesDirectorio, nameof(templatesDirectorio));

            _templatesDirectorio = templatesDirectorio;
        }

        public string GenerarArchivosDeEntidades(GenerarArchivosDeEntidadesParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));
            Validador.ValidarArgumentListaRequeridaYThrow(parametros.Entidades, nameof(parametros.Entidades));

            if (!parametros.GenerarAlMenosUnArchivo)
            {
                throw new ExcepcionMensajeAlUsuario("Debe seleccionar al menos un archivo para generar.");
            }
            
            var grupoId = Guid.NewGuid();

            foreach (var e in parametros.Entidades)
            {
                GenerarArchivosDeEntidad(e, parametros, grupoId);
            }

            return GenerarYGuardarZip(parametros.Entidades.First().AplicacionVersion.Aplicacion.Nombre, grupoId);
        }

        public string GenerarArchivosDeEntidad(GenerarArchivosDeEntidadParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));
            Validador.ValidarArgumentRequeridoYThrow(parametros.Entidad, nameof(parametros.Entidad));

            if (!parametros.GenerarAlMenosUnArchivo)
            {
                throw new ExcepcionMensajeAlUsuario("Debe seleccionar al menos un archivo para generar.");
            }

            var grupoId = Guid.NewGuid();

            GenerarArchivosDeEntidad(parametros.Entidad, parametros, grupoId);

            return GenerarYGuardarZip(parametros.Entidad.AplicacionVersion.Aplicacion.Nombre, grupoId);
        }

        private void GenerarArchivosDeEntidad(Entidad entidad, GenerarArchivosParametrosBase parametros, Guid grupoId)
        {
            if (parametros.GenerarBDTabla)
            {
                GenerarBDTabla(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarEntidadesEntidad)
            {
                GenerarEntidadesEntidad(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarEntidadesMetadataEntidadMetadata)
            {
                GenerarEntidadesMetadataEntidadMetadata(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarDatosSqlConfig)
            {
                GenerarDatosSqlConfig(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarDatosRepositorio)
            {
                GenerarDatosRepositorio(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarNegocio)
            {
                GenerarNegocio(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarNegocioAutomapperProfile)
            {
                GenerarNegocioAutomapperProfile(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarNegocioDTOAgregarParametros)
            {
                GenerarNegocioDTOAgregarParametros(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarNegocioDTOActualizarParametros)
            {
                GenerarNegocioDTOActualizarParametros(entidad, grupoId: grupoId);
            }

            if (entidad.PropiedadesDefault.AuditoriaBorrado)
            {
                if (parametros.GenerarNegocioDTOMarcarComoBorradoParametros)
                {
                    GenerarNegocioDTOMarcarComoBorradoParametros(entidad, grupoId: grupoId);
                }

                if (parametros.GenerarNegocioDTODesmarcarComoBorradoParametros)
                {
                    GenerarNegocioDTODesmarcarComoBorradoParametros(entidad, grupoId: grupoId);
                }
            }

            if (parametros.GenerarWebModelsItemModel)
            {
                GenerarWebModelsItemModel(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebViewModelsEntidadViewModel)
            {
                GenerarWebViewModelsEntidadViewModel(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebViewModelsListaViewModel)
            {
                GenerarWebViewModelsListaViewModel(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebAutomapperProfile)
            {
                GenerarWebAutomapperProfile(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebViewsMetadata)
            {
                GenerarWebMetadataViews(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebController)
            {
                GenerarWebController(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebViewsIndex)
            {
                GenerarWebViewsIndex(entidad, grupoId: grupoId);
            }

            if (parametros.GenerarWebViewsEntidad)
            {
                GenerarWebViewsEntidad(entidad, grupoId: grupoId);
            }
        }

        private string GenerarYGuardarZip(string aplicacionNombre, Guid grupoId)
        {
            var zipPath = Path.Combine(GenerarPathDirectorioBase(grupoId), $"{aplicacionNombre}{ArchivoExtensiones.Application.ZIP}");
            ZipFile.CreateFromDirectory(
                GenerarPathDirectorioArchivos(grupoId),
                zipPath);

            return zipPath;
        }

        private string GenerarBDTabla(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "BD_Tabla.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.BD", "dbo", "Tables"),
                $"{entidad.NombrePlural}.sql",
                grupoId);
        }

        private string GenerarEntidadesEntidad(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Entidades_Entidad.cshtml",
                entidad,
                $"{entidad.AplicacionVersion.Aplicacion.Nombre}.Entidades",
                $"{entidad.Nombre}.cs",
                grupoId);
        }

        private string GenerarEntidadesMetadataEntidadMetadata(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Entidades_Metadata_EntidadMetadata.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Entidades", "Metadata"),
                $"{entidad.Nombre}Metadata.cs",
                grupoId);
        }

        private string GenerarDatosSqlConfig(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Datos_SqlConfig.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Datos", "Sql", "Config"),
                $"{entidad.Nombre}Config.cs",
                grupoId);
        }

        private string GenerarDatosRepositorio(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Datos_Repositorio.cshtml",
                entidad,
                $"{entidad.AplicacionVersion.Aplicacion.Nombre}.Datos",
                $"{entidad.NombrePlural}Repositorio.cs",
                grupoId);
        }

        private string GenerarNegocio(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_Negocio.cshtml",
                entidad,
                $"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio",
                $"{entidad.NombrePlural}Negocio.cs",
                grupoId);
        }

        private string GenerarNegocioAutomapperProfile(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_Automapper_Profile.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio", "Automapper"),
                $"{entidad.NombrePlural}Profile.cs",
                grupoId);
        }

        private string GenerarNegocioDTOAgregarParametros(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_DTO_AgregarParametros.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio", "DTO", entidad.NombrePlural),
                "AgregarParametros.cs",
                grupoId);
        }

        private string GenerarNegocioDTOActualizarParametros(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_DTO_ActualizarParametros.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio", "DTO", entidad.NombrePlural),
                "ActualizarParametros.cs",
                grupoId);
        }

        private string GenerarNegocioDTOMarcarComoBorradoParametros(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_DTO_MarcarComoBorradoParametros.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio", "DTO", entidad.NombrePlural),
                "MarcarComoBorradoParametros.cs",
                grupoId);
        }

        private string GenerarNegocioDTODesmarcarComoBorradoParametros(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Negocio_DTO_DesmarcarComoBorradoParametros.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Negocio", "DTO", entidad.NombrePlural),
                "DesmarcarComoBorradoParametros.cs",
                grupoId);
        }

        private string GenerarWebModelsItemModel(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Models_ItemModel.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Models", entidad.NombrePlural),
                $"{entidad.Nombre}ItemModel.cs",
                grupoId);
        }

        private string GenerarWebViewModelsEntidadViewModel(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_ViewModels_EntidadViewModel.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "ViewModels", entidad.NombrePlural),
                $"{entidad.Nombre}ViewModel.cs",
                grupoId);
        }

        private string GenerarWebViewModelsListaViewModel(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_ViewModels_ListaViewModel.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "ViewModels", entidad.NombrePlural),
                $"{entidad.NombrePlural}ViewModel.cs",
                grupoId);
        }

        private string GenerarWebAutomapperProfile(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Automapper_Profile.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Automapper"),
                $"{entidad.NombrePlural}Profile.cs",
                grupoId);
        }

        private string GenerarWebMetadataViews(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Metadata_Views.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Metadata", "Views"),
                $"{entidad.NombrePlural}Views.cs",
                grupoId);
        }

        private string GenerarWebController(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Controller.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Controllers"),
                $"{entidad.NombrePlural}Controller.cs",
                grupoId);
        }

        private string GenerarWebViewsIndex(Entidad entidad,
            Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Views_Index.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Views", entidad.NombrePlural),
                "Index.cshtml",
                grupoId,
                esHtmlView: true);
        }

        private string GenerarWebViewsEntidad(Entidad entidad,
           Guid? grupoId = null)
        {
            return GenerarDesdeTemplate(
                "Web_Views_Entidad.cshtml",
                entidad,
                Path.Combine($"{entidad.AplicacionVersion.Aplicacion.Nombre}.Web.Portal", "Views", entidad.NombrePlural),
                $"{entidad.Nombre}.cshtml",
                grupoId,
                esHtmlView: true);
        }

        private string GenerarDesdeTemplate(string template, object modelo, string destinoSubdirectorio, string destinoArchivo,
            Guid? grupoId = null,
            bool esHtmlView = true,
            IEnumerable<string> partialViews = null)
        {
            var razor = Engine.Razor;

            var templateNames = new List<string>
            {
                AgregarTemplateARazorYObtenerNombre(template, razor, esHtmlView)
            };

            if (partialViews != null)
            {
                foreach (var pv in partialViews)
                {
                    templateNames.Add(AgregarTemplateARazorYObtenerNombre(pv, razor, esHtmlView));
                }
            }

            foreach (var tn in templateNames)
            {
                razor.Compile(tn);
            }
            
            string directorioGenerados = Path.Combine(GenerarPathDirectorioArchivos(grupoId), destinoSubdirectorio);
            Directory.CreateDirectory(directorioGenerados);

            string pathArchivo = Path.Combine(directorioGenerados, destinoArchivo);
            File.WriteAllText(
                pathArchivo,
                razor.Run(
                    templateNames[0],
                    null,
                    modelo));

            return pathArchivo;
        }

        private string AgregarTemplateARazorYObtenerNombre(string template, IRazorEngineService razor,
            bool esHtmlView = false)
        { 
            string templateName = Path.GetFileNameWithoutExtension(template);
            razor.AddTemplate(templateName, ObtenerTemplate(template, esHtmlView));
            return templateName;
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
            bool esHtmlView = false)
        {
            var contenido = File.ReadAllText(Path.Combine(_templatesDirectorio, template));
            if (esHtmlView)
            {
                contenido = contenido
                    .Replace("Html.Raw", "Raw")
                    .Replace("<text>@Html.Partial", "<text>@Include");
            }
            return contenido;
        }
    }
}
