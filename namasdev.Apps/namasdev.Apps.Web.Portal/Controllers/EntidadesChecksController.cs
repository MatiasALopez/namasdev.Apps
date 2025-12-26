using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.Validation;
using namasdev.Web.Helpers;
using namasdev.Web.Models;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Datos;
using namasdev.Apps.Negocio;
using namasdev.Apps.Negocio.DTO.EntidadesChecks;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.Models.EntidadesChecks;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesChecks;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesChecksController : ControllerBase
    {
        public const string NAME = "EntidadesChecks";

        private readonly IEntidadesChecksRepositorio _entidadesChecksRepositorio;
        private readonly IEntidadesChecksNegocio _entidadesChecksNegocio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesChecksController(
            IEntidadesChecksRepositorio entidadesChecksRepositorio, 
            IEntidadesChecksNegocio entidadesChecksNegocio,
            IEntidadesRepositorio entidadesRepositorio,
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesChecksRepositorio, nameof(entidadesChecksRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesChecksNegocio, nameof(entidadesChecksNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesChecksRepositorio = entidadesChecksRepositorio;
            _entidadesChecksNegocio = entidadesChecksNegocio;
            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        #region Acciones

        public ActionResult Index(Guid id)
        {
            var model = new EntidadesChecksViewModel
            {
                Id = id,
            };

            CargarEntidadesChecksViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var asociacion = _entidadesChecksRepositorio.Obtener(id);
            if (asociacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadCheckMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesChecksRepositorio.EliminarPorId(id);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadCheckMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid entidadId)
        {
            var model = new EntidadCheckViewModel { EntidadId = entidadId };
            CargarEntidadCheckViewModel(model, PaginaModo.Agregar);

            return View(EntidadesChecksViews.EntidadCheck, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadCheckViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesChecksNegocio.Agregar(Mapear<AgregarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadCheckMetadata.Mensajes.AGREGAR_OK);

                    model = new EntidadCheckViewModel
                    {
                        EntidadId = model.EntidadId,
                    };
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadCheckViewModel(model, PaginaModo.Agregar);
            return View(EntidadesChecksViews.EntidadCheck, model);
        }

        public ActionResult Editar(Guid id)
        {
            var entidadCheck = _entidadesChecksRepositorio.Obtener(id, cargarDatosAdicionales: true);

            var model = Mapear<EntidadCheckViewModel>(entidadCheck);

            CargarEntidadCheckViewModel(model, PaginaModo.Editar);

            return View(EntidadesChecksViews.EntidadCheck, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadCheckViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesChecksNegocio.Actualizar(Mapear<ActualizarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadCheckMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadCheckViewModel(model, PaginaModo.Editar);
        
            return View(EntidadesChecksViews.EntidadCheck, model);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadesChecksViewModel(EntidadesChecksViewModel model)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            var entidad = _entidadesRepositorio.Obtener(model.Id);
            model.EntidadNombre = entidad.Nombre;
            model.AplicacionVersionId = entidad.AplicacionVersionId;

            model.Items = Mapear<List<EntidadCheckItemModel>>(
                _entidadesChecksRepositorio.ObtenerPorEntidad(model.Id));
        }

        private void CargarEntidadCheckViewModel(EntidadCheckViewModel model, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            model.PaginaModo = paginaModo;

            var entidad = _entidadesRepositorio.Obtener(model.EntidadId);
            model.EntidadTablaNombre = entidad.NombrePlural;
            model.AplicacionVersionId = entidad.AplicacionVersionId;
            
            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                model.Nombre = $"CK_{entidad.NombrePlural}_";
            }

            model.Propiedades = Mapear<List<EntidadPropiedadItemModel>>(_entidadesPropiedadesRepositorio.ObtenerPorEntidad(model.EntidadId, cargarDatosAdicionales: true));
        }

        #endregion Metodos
    }
}
