using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using namasdev.Core.IO;
using namasdev.Core.Validation;
using namasdev.Web.Helpers;
using namasdev.Apps.Entidades;
using namasdev.Apps.Datos;
using namasdev.Apps.Negocio;

namespace namasdev.Apps.Web.Portal.Controllers
{
    public class TemplatesController : ControllerBase
    {
        public const string NAME = "Templates";

        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IGeneradorArchivos _generadorArchivos;

        public TemplatesController(IEntidadesRepositorio entidadesRepositorio, IGeneradorArchivos generadorArchivos)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(generadorArchivos, nameof(generadorArchivos));

            _entidadesRepositorio = entidadesRepositorio;
            _generadorArchivos = generadorArchivos;
        }

        #region Actions

        public ActionResult DescargarTodos(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarZipTodos(entidad));
        }

        public ActionResult Tabla(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarSQLTabla(entidad));
        }

        public ActionResult Entidad(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarEntidadesEntidad(entidad));
        }

        public ActionResult EntidadMetadata(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarEntidadesEntidadMetadata(entidad));
        }

        public ActionResult SqlConfig(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarDatosSqlConfig(entidad));
        }

        public ActionResult Repositorio(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarDatosRepositorio(entidad));
        }

        public ActionResult Negocio(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarNegocio(entidad));
        }

        public ActionResult Mapper(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebMapper(entidad));
        }

        public ActionResult ItemModel(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebItemModel(entidad));
        }

        public ActionResult ListaViewModel(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebListaViewModel(entidad));
        }

        public ActionResult EntidadViewModel(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebEntidadViewModel(entidad));
        }

        public ActionResult ViewsMetadata(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebViewsMetadata(entidad));
        }

        public ActionResult Controller(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebController(entidad));
        }

        public ActionResult IndexView(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebIndexView(entidad));
        }

        public ActionResult EntidadView(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return ControllerHelper.CrearActionResultArchivo(
                _generadorArchivos.GenerarWebEntidadView(entidad));
        }

        #region Debug

        public ActionResult Entidades_Entidad(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            var contenido = RenderViewToString("Entidades_Entidad", entidad);
            return Content(contenido, ArchivoContentTypes.Text.TXT);
        }

        public ActionResult Entidades_EntidadMetadata(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            var contenido = RenderViewToString("Entidades_EntidadMetadata", entidad);
            return Content(contenido, ArchivoContentTypes.Text.TXT);
        }

        public ActionResult Web_IndexView(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            var contenido = RenderViewToString("Web_IndexView", entidad);
            return Content(contenido, ArchivoContentTypes.Text.TXT);
        }

        public ActionResult Web_EntidadView(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            var contenido = RenderViewToString("Web_EntidadView", entidad);
            return Content(contenido, ArchivoContentTypes.Text.TXT);
        }

        #endregion

        #endregion

        #region Metodos

        private Entidad ObtenerEntidad(Guid id, bool ordenarPropiedades = true)
        {
            var entidad = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            if (ordenarPropiedades)
            {
                entidad.Propiedades = entidad.Propiedades.OrderBy(p => p.Orden).ToList();
            }
            return entidad;
        }

        public string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}