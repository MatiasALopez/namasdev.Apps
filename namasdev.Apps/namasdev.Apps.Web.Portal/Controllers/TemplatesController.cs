﻿using System;
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
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Datos.Entity;

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
            var model = new GenerarArchivosViewModel { Id = id };
            model.MarcarTodos();

            CargarGenerarArchivosViewModel(model, modoAplicacion: true);
            return View(TemplatesViews.GenerarArchivos, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult _GenerarArchivosAplicacion(GenerarArchivosViewModel model)
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

            CargarGenerarArchivosViewModel(model, modoAplicacion: true);
            return View(TemplatesViews.GenerarArchivos, model);
        }

        public ActionResult _GenerarArchivosEntidad(Guid id,
            bool debug = false)
        {
            var model = new GenerarArchivosViewModel { Id = id, ModoDebug = debug };
            model.MarcarTodos();

            CargarGenerarArchivosViewModel(model);
            return View(TemplatesViews.GenerarArchivos, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult _GenerarArchivosEntidad(GenerarArchivosViewModel model)
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

            CargarGenerarArchivosViewModel(model);
            return View(TemplatesViews.GenerarArchivos, model);
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

        private void CargarGenerarArchivosViewModel(GenerarArchivosViewModel model, 
            bool modoAplicacion = false)
        {
            model.ModoAplicacion = modoAplicacion;
            
            if (model.ModoAplicacion)
            {
                model.VersionesSelectList = ListasHelper.ObtenerVersionesSelectList(
                    versiones: _aplicacionesVersionesRepositorio.ObtenerPorAplicacion(model.Id));
            }
        }

        private IEnumerable<Entidad> ObtenerEntidadesDeAplicacion(Guid aplicacionVersionId, 
            bool ordenarPropiedades = true)
        {
            var entidades = _entidadesRepositorio.ObtenerPorVersion(aplicacionVersionId, cargarPropiedades: EntidadCargaPropiedades.CrearTodas());
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
            var entidad = _entidadesRepositorio.Obtener(id, cargarPropiedades: EntidadCargaPropiedades.CrearTodas());
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