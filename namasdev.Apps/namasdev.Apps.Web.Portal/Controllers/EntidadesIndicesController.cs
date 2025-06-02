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
using namasdev.Apps.Negocio.DTO.EntidadesIndices;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.Models.EntidadesIndices;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesIndices;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesIndicesController : ControllerBase
    {
        public const string NAME = "EntidadesIndices";

        private readonly IEntidadesIndicesRepositorio _entidadesIndicesRepositorio;
        private readonly IEntidadesIndicesNegocio _entidadesIndicesNegocio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesIndicesController(
            IEntidadesIndicesRepositorio entidadesIndicesRepositorio, 
            IEntidadesIndicesNegocio entidadesIndicesNegocio,
            IEntidadesRepositorio entidadesRepositorio,
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesIndicesRepositorio, nameof(entidadesIndicesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesIndicesNegocio, nameof(entidadesIndicesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesIndicesRepositorio = entidadesIndicesRepositorio;
            _entidadesIndicesNegocio = entidadesIndicesNegocio;
            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        #region Acciones

        public ActionResult Index(Guid id)
        {
            var model = new EntidadesIndicesViewModel
            {
                Id = id,
            };

            CargarEntidadesIndicesViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var asociacion = _entidadesIndicesRepositorio.Obtener(id);
            if (asociacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadIndiceMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesIndicesRepositorio.EliminarPorId(id);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadIndiceMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid entidadId)
        {
            var model = new EntidadIndiceViewModel { EntidadId = entidadId };
            CargarEntidadIndiceViewModel(model, PaginaModo.Agregar);

            return View(EntidadesIndicesViews.EntidadIndice, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadIndiceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesIndicesNegocio.Agregar(Mapear<AgregarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadIndiceMetadata.Mensajes.AGREGAR_OK);

                    model = new EntidadIndiceViewModel
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

            CargarEntidadIndiceViewModel(model, PaginaModo.Agregar);
            return View(EntidadesIndicesViews.EntidadIndice, model);
        }

        public ActionResult Editar(Guid id)
        {
            var entidadIndice = _entidadesIndicesRepositorio.Obtener(id, cargarDatosAdicionales: true);

            var model = Mapear<EntidadIndiceViewModel>(entidadIndice);

            CargarEntidadIndiceViewModel(model, PaginaModo.Editar,
                propiedadesIdsSeleccionados: entidadIndice.Propiedades.Select(eip => eip.EntidadPropiedadId).ToArray());

            return View(EntidadesIndicesViews.EntidadIndice, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadIndiceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesIndicesNegocio.Actualizar(Mapear<ActualizarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadIndiceMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadIndiceViewModel(model, PaginaModo.Editar);
        
            return View(EntidadesIndicesViews.EntidadIndice, model);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadesIndicesViewModel(EntidadesIndicesViewModel model)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            var entidad = _entidadesRepositorio.Obtener(model.Id);
            model.EntidadNombre = entidad.Nombre;
            model.AplicacionVersionId = entidad.AplicacionVersionId;

            model.Items = Mapear<List<EntidadIndiceItemModel>>(
                _entidadesIndicesRepositorio.ObtenerPorEntidad(model.Id));
        }

        private void CargarEntidadIndiceViewModel(EntidadIndiceViewModel model, PaginaModo paginaModo,
            IEnumerable<Guid> propiedadesIdsSeleccionados = null)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            model.PaginaModo = paginaModo;

            var entidad = _entidadesRepositorio.Obtener(model.EntidadId);
            model.EntidadTablaNombre = entidad.NombrePlural;
            model.AplicacionVersionId = entidad.AplicacionVersionId;

            model.Propiedades = Mapear<List<EntidadPropiedadItemModel>>(_entidadesPropiedadesRepositorio.ObtenerPorEntidad(model.EntidadId, cargarDatosAdicionales: true));
            
            if (propiedadesIdsSeleccionados != null)
            {
                model.SeleccionarPropiedades(propiedadesIdsSeleccionados);
            }

            model.SiNoSelectList = ListasHelper.ObtenerSiNoSelectList();
        }

        #endregion Metodos
    }
}
