using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.Validation;
using namasdev.Web.Models;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Datos;
using namasdev.Apps.Negocio;
using namasdev.Apps.Negocio.DTO.EntidadesAsociaciones;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.Models.EntidadesAsociaciones;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesAsociaciones;
using namasdev.Apps.Web.Portal.Helpers;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesAsociacionesController : ControllerBase
    {
        public const string NAME = "EntidadesAsociaciones";

        private readonly IEntidadesAsociacionesRepositorio _entidadesAsociacionesRepositorio;
        private readonly IEntidadesAsociacionesNegocio _entidadesAsociacionesNegocio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesAsociacionesController(
            IEntidadesAsociacionesRepositorio entidadesAsociacionesRepositorio, 
            IEntidadesAsociacionesNegocio entidadesAsociacionesNegocio,
            IEntidadesRepositorio entidadesRepositorio,
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesAsociacionesRepositorio, nameof(entidadesAsociacionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesAsociacionesNegocio, nameof(entidadesAsociacionesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesAsociacionesRepositorio = entidadesAsociacionesRepositorio;
            _entidadesAsociacionesNegocio = entidadesAsociacionesNegocio;
            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        #region Acciones

        public ActionResult Index(Guid id)
        {
            var model = new EntidadesAsociacionesViewModel
            {
                Id = id,
            };

            CargarEntidadesAsociacionesViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var asociacion = _entidadesAsociacionesRepositorio.Obtener(id);
            if (asociacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadAsociacionMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesAsociacionesRepositorio.EliminarPorId(id);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadAsociacionMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid entidadId, Guid aplicacionVersionId)
        {
            var model = new EntidadAsociacionViewModel 
            { 
                OrigenEntidadId = entidadId, 
                AplicacionVersionId = aplicacionVersionId 
            };
            CargarEntidadAsociacionViewModel(model, PaginaModo.Agregar);

            return View(EntidadesAsociacionesViews.EntidadAsociacion, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadAsociacionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesAsociacionesNegocio.Agregar(Mapear<AgregarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadAsociacionMetadata.Mensajes.AGREGAR_OK);

                    model = new EntidadAsociacionViewModel
                    {
                        OrigenEntidadId = model.OrigenEntidadId,
                        AplicacionVersionId = model.AplicacionVersionId
                    };
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadAsociacionViewModel(model, PaginaModo.Agregar);
            return View(EntidadesAsociacionesViews.EntidadAsociacion, model);
        }

        public ActionResult Editar(Guid id, Guid aplicacionVersionId)
        {
            var entidadAsociacion = _entidadesAsociacionesRepositorio.Obtener(id);
            if (entidadAsociacion == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = Mapear<EntidadAsociacionViewModel>(entidadAsociacion);
            model.AplicacionVersionId = aplicacionVersionId;

            CargarEntidadAsociacionViewModel(model, PaginaModo.Editar);

            return View(EntidadesAsociacionesViews.EntidadAsociacion, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadAsociacionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesAsociacionesNegocio.Actualizar(Mapear<ActualizarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadAsociacionMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadAsociacionViewModel(model, PaginaModo.Editar);
        
            return View(EntidadesAsociacionesViews.EntidadAsociacion, model);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadesAsociacionesViewModel(EntidadesAsociacionesViewModel model)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            var entidad = _entidadesRepositorio.Obtener(model.Id);
            model.EntidadNombre = entidad.Nombre;
            model.AplicacionVersionId = entidad.AplicacionVersionId;

            model.Items = Mapear<List<EntidadAsociacionItemModel>>(
                _entidadesAsociacionesRepositorio.ObtenerPorEntidad(model.Id, cargarDatosAdicionales: true));
        }

        private void CargarEntidadAsociacionViewModel(EntidadAsociacionViewModel model, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            model.PaginaModo = paginaModo;

            if (string.IsNullOrWhiteSpace(model.OrigenEntidadTablaNombre))
            {
                model.OrigenEntidadTablaNombre = _entidadesRepositorio.Obtener(model.OrigenEntidadId).NombrePlural;
            }

            model.OrigenPropiedadesSelectList = ListasHelper.ObtenerEntidadesPropiedadesSelectList(_entidadesPropiedadesRepositorio.ObtenerPorEntidad(model.OrigenEntidadId));

            model.EntidadesSelectList = ListasHelper.ObtenerEntidadesSelectList(_entidadesRepositorio.ObtenerPorVersion(model.AplicacionVersionId));
            model.DestinoPropiedadesSelectList = 
                model.DestinoEntidadId.HasValue
                ? ListasHelper.ObtenerEntidadesPropiedadesSelectList(_entidadesPropiedadesRepositorio.ObtenerPorEntidad(model.DestinoEntidadId.Value))
                : namasdev.Web.Helpers.ListasHelper.ObtenerSelectListVacio();

            model.MultiplicidadesSelectList = ListasHelper.ObtenerAsociacionMultiplicidadesSelectList(_entidadesAsociacionesRepositorio.ObtenerMultiplicidades());
            model.ReglasSelectList = ListasHelper.ObtenerAsociacionReglasSelectList(_entidadesAsociacionesRepositorio.ObtenerReglas());
        }

        #endregion Metodos
    }
}
