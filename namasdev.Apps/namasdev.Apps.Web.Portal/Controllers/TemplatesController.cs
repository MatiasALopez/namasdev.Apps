using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.IO;
using namasdev.Core.Validation;
using namasdev.Web.Helpers;

using namasdev.Apps.Entidades;
using namasdev.Apps.Datos;
using namasdev.Apps.Negocio;
using namasdev.Apps.Negocio.DTO.GeneradorArchivos;
using namasdev.Apps.Web.Portal.ViewModels.Templates;

namespace namasdev.Apps.Web.Portal.Controllers
{
    public class TemplatesController : ControllerBase
    {
        public const string NAME = "Templates";

        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IGeneradorArchivos _generadorArchivos;

        public TemplatesController(IEntidadesRepositorio entidadesRepositorio, IGeneradorArchivos generadorArchivos, IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(generadorArchivos, nameof(generadorArchivos));

            _entidadesRepositorio = entidadesRepositorio;
            _generadorArchivos = generadorArchivos;
        }

        #region Actions

        public ActionResult _GenerarArchivos(Guid id)
        {
            var model = new GenerarArchivosViewModel { EntidadId = id };
            model.MarcarTodos();

            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult _GenerarArchivos(GenerarArchivosViewModel model)
        {
            try
            {
                var parametros = Mapear<GenerarZipParametros>(model);
                parametros.Entidad = ObtenerEntidad(model.EntidadId);

                return ControllerHelper.CrearActionResultArchivo(
                    _generadorArchivos.GenerarZip(parametros));
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            return View(model);
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

        public ActionResult Web_Controller(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            var contenido = RenderViewToString("Web_Controller", entidad);
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