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
using namasdev.Apps.Negocio.DTO.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.Helpers;
using namasdev.Apps.Web.Portal.Metadata;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesPropiedadesController : ControllerBase
    {
        public const string NAME = "EntidadesPropiedades";

        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;
        private readonly IEntidadesPropiedadesNegocio _entidadesPropiedadesNegocio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;

        public EntidadesPropiedadesController(
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio, 
            IEntidadesPropiedadesNegocio entidadesPropiedadesNegocio,
            IEntidadesRepositorio entidadesRepositorio,
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesNegocio, nameof(entidadesPropiedadesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));

            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
            _entidadesPropiedadesNegocio = entidadesPropiedadesNegocio;
            _entidadesRepositorio = entidadesRepositorio;
        }

        #region Acciones

        public ActionResult Index(Guid id)
        {
            var model = new EntidadesPropiedadesViewModel
            {
                Id = id,
            };

            CargarEntidadesPropiedadesViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Index(EntidadesPropiedadesViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Operacion)
                {
                    case EntidadesPropiedadesViewModel.OPERACION_ESTABLECER_CLAVE:
                        try
                        {
                            _entidadesPropiedadesNegocio.EstablecerComoClave(
                                new EstablecerComoClaveParametros 
                                { 
                                    EntidadId = model.Id, 
                                    PropiedadesIds = model.ItemsSeleccionadosIds 
                                });

                            ControllerHelper.CargarMensajeResultadoOk(EntidadPropiedadMetadata.Mensajes.ESTABLECER_CLAVE_OK);
                            ModelState.Clear();
                        }
                        catch (Exception ex)
                        {
                            ControllerHelper.CargarMensajesError(EntidadPropiedadMetadata.Mensajes.ACTUALIZAR_ORDEN_ERROR + Environment.NewLine + ex.Message);
                        }

                        break;

                    case EntidadesPropiedadesViewModel.OPERACION_ACTUALIZAR_ORDEN:
                        try
                        {
                            _entidadesPropiedadesNegocio.ActualizarOrden(
                                new ActualizarOrdenParametros 
                                {
                                    EntidadId = model.Id, 
                                    PropiedadesIdsYOrdenes = model.ItemsConOrdenesModificados
                                });

                            ControllerHelper.CargarMensajeResultadoOk(EntidadPropiedadMetadata.Mensajes.ACTUALIZAR_ORDEN_OK);
                            ModelState.Clear();
                        }
                        catch (Exception ex)
                        {
                            ControllerHelper.CargarMensajesError(EntidadPropiedadMetadata.Mensajes.ACTUALIZAR_ORDEN_ERROR + Environment.NewLine + ex.Message);
                        }

                        break;

                    default:
                        ControllerHelper.CargarMensajesError(Textos.OperacionInvalida(model.Operacion));
                        break;
                }
            }

            CargarEntidadesPropiedadesViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var entidadPropiedad = _entidadesPropiedadesRepositorio.Obtener(id);
            if (entidadPropiedad == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadPropiedadMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesPropiedadesNegocio.Eliminar(
                    new EliminarParametros 
                    { 
                        Id = id, 
                        UsuarioLogueadoId = UsuarioId 
                    });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadPropiedadMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }
                
        public ActionResult Agregar(Guid entidadId)
        {
            var model = new EntidadPropiedadViewModel
            {
                EntidadId = entidadId,
            };

            CargarEntidadPropiedadViewModel(model, PaginaModo.Agregar);
            return View(EntidadesPropiedadesViews.EntidadPropiedad, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadPropiedadViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    _entidadesPropiedadesNegocio.Agregar(Mapear<AgregarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadPropiedadMetadata.Mensajes.AGREGAR_OK);

                    model = new EntidadPropiedadViewModel
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

            CargarEntidadPropiedadViewModel(model, PaginaModo.Agregar);
            return View(EntidadesPropiedadesViews.EntidadPropiedad, model);
        }

        public ActionResult Editar(Guid id)
        {
            var entidadPropiedad = _entidadesPropiedadesRepositorio.Obtener(id, cargarDatosAdicionales: true);

            var model = Mapear<EntidadPropiedadViewModel>(entidadPropiedad);
            
            CargarEntidadPropiedadViewModel(model, PaginaModo.Editar);
            return View(EntidadesPropiedadesViews.EntidadPropiedad, model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadPropiedadViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesPropiedadesNegocio.Actualizar(Mapear<ActualizarParametros>(model));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadPropiedadMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadPropiedadViewModel(model, PaginaModo.Editar);
            return View(EntidadesPropiedadesViews.EntidadPropiedad, model);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadesPropiedadesViewModel(EntidadesPropiedadesViewModel model)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            var entidad = _entidadesRepositorio.Obtener(model.Id);
            model.EntidadNombre = entidad.Nombre;
            model.AplicacionVersionId = entidad.AplicacionVersionId;

            model.Items = Mapear<List<EntidadPropiedadItemModel>>(
                _entidadesPropiedadesRepositorio.ObtenerPorEntidad(
                    entidad.Id,
                    cargarDatosAdicionales: true));
        }
        
        private void CargarEntidadPropiedadViewModel(EntidadPropiedadViewModel model, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(model, nameof(model));

            model.PaginaModo = paginaModo;

            model.GeneradaAlCrear = model.GeneradaAlCrear ?? false;
            model.Editable = model.Editable ?? true;

            model.TiposSelectList = ListasHelper.ObtenerPropiedadTiposSelectList(_entidadesPropiedadesRepositorio.ObtenerTipos());
            model.SiNoSelectList = namasdev.Web.Helpers.ListasHelper.ObtenerSiNoSelectList();
        }

        #endregion Metodos
    }
}
