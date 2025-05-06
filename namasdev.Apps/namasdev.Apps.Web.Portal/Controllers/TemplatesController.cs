using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.IO;
using namasdev.Core.Validation;

using namasdev.Apps.Entidades;
using namasdev.Apps.Datos;
using namasdev.Apps.Negocio;
using namasdev.Apps.Negocio.DTO.GeneradorArchivos;
using namasdev.Apps.Web.Portal.ViewModels.Templates;
using namasdev.Apps.Web.Portal.Helpers;

namespace namasdev.Apps.Web.Portal.Controllers
{
    public class TemplatesController : ControllerBase
    {
        public const string NAME = "Templates";

        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;
        private readonly IGeneradorArchivos _generadorArchivos;

        public TemplatesController(
            IEntidadesRepositorio entidadesRepositorio, 
            IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio, 
            IGeneradorArchivos generadorArchivos, 
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(generadorArchivos, nameof(generadorArchivos));

            _entidadesRepositorio = entidadesRepositorio;
            _aplicacionesVersionesRepositorio = aplicacionesVersionesRepositorio;
            _generadorArchivos = generadorArchivos;
        }

        #region Actions

        public ActionResult _GenerarArchivosAplicacion(Guid id)
        {
            var model = new GenerarArchivosAplicacionViewModel { Id = id };
            model.MarcarTodos();

            CargarGenerarArchivosAplicacionViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult _GenerarArchivosAplicacion(GenerarArchivosAplicacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var parametros = Mapear<GenerarArchivosDeEntidadesParametros>(model);
                    parametros.Entidades = ObtenerEntidadesDeAplicacion(model.AplicacionVersionId.Value);

                    return ControllerHelper.CrearActionResultArchivo(
                        _generadorArchivos.GenerarArchivosDeEntidades(parametros));
                }
                catch (Exception ex)
                {
                    ControllerHelper.CargarMensajesError(ex.Message);
                }
            }

            CargarGenerarArchivosAplicacionViewModel(model);
            return View(model);
        }

        public ActionResult _GenerarArchivosEntidad(Guid id)
        {
            var model = new GenerarArchivosEntidadViewModel { Id = id };
            model.MarcarTodos();

            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult _GenerarArchivosEntidad(GenerarArchivosEntidadViewModel model)
        {
            try
            {
                var parametros = Mapear<GenerarArchivosDeEntidadParametros>(model);
                parametros.Entidad = ObtenerEntidad(model.Id);

                return ControllerHelper.CrearActionResultArchivo(
                    _generadorArchivos.GenerarArchivosDeEntidad(parametros));
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            return View(model);
        }

        #region Debug

        public ActionResult Debug(Guid id, string template)
        {
            var entidad = ObtenerEntidad(id);
            var contenido = RenderViewToString(template, entidad);
            return Content(contenido, ArchivoContentTypes.Text.TXT);
        }

        #endregion

        #endregion

        #region Metodos

        private void CargarGenerarArchivosAplicacionViewModel(GenerarArchivosAplicacionViewModel model)
        {
            model.VersionesSelectList = ListasHelper.ObtenerVersionesSelectList(
                versiones: _aplicacionesVersionesRepositorio.ObtenerLista(model.Id));
        }

        private IEnumerable<Entidad> ObtenerEntidadesDeAplicacion(Guid aplicacionVersionId, 
            bool ordenarPropiedades = true)
        {
            var entidades = _entidadesRepositorio.ObtenerLista(aplicacionVersionId, cargarDatosAdicionales: true);
            if (ordenarPropiedades)
            {
                foreach (var e in entidades)
                {
                    e.Propiedades = e.Propiedades.OrderBy(p => p.Orden).ToList();
                }
            }
            return entidades;
        }

        private Entidad ObtenerEntidad(Guid id, 
            bool ordenarPropiedades = true)
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