using System;
using System.Linq;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Helpers;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.Metadata;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;

// TODO (ML): mensaje "operacion no valida" a namasdev.core?

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
            IEntidadesRepositorio entidadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesNegocio, nameof(entidadesPropiedadesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));

            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
            _entidadesPropiedadesNegocio = entidadesPropiedadesNegocio;
            _entidadesRepositorio = entidadesRepositorio;
        }

        #region Acciones

        public ActionResult Index(
            Guid entidadId,
            string busqueda = null,
            string orden = null)
        {
            var modelo = new EntidadesPropiedadesViewModel
            {
                EntidadId = entidadId,
                Busqueda = busqueda,
                Orden = orden,
            };

            CargarEntidadesPropiedadesViewModel(modelo);
            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Index(EntidadesPropiedadesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    switch (model.Operacion)
                    {
                        case EntidadesPropiedadesViewModel.OPERACION_ESTABLECER_CLAVE:
                            _entidadesPropiedadesNegocio.EstablecerComoClave(model.EntidadId, model.ItemsSeleccionados.Select(i => i.EntidadPropiedadId));

                            ControllerHelper.CargarMensajeResultadoOk(EntidadPropiedadMetadata.Mensajes.ESTABLECER_CLAVE_OK);
                            ModelState.Clear();

                            break;

                        default:
                            throw new Exception(Textos.OperacionInvalida(model.Operacion));
                    }
                }
                catch (Exception ex)
                {
                    ControllerHelper.CargarMensajesError(ex.Message);
                }
            }

            CargarEntidadesPropiedadesViewModel(model);
            return View(model);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var propiedad = _entidadesPropiedadesRepositorio.Obtener(id);
            if (propiedad == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadPropiedadMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesPropiedadesNegocio.MarcarComoBorrado(id, UsuarioId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadPropiedadMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid entidadId)
        {
            var modelo = new EntidadPropiedadViewModel
            {
                EntidadId = entidadId,
            };
            
            CargarEntidadPropiedadViewModel(modelo, PaginaModo.Agregar);
            return View(EntidadesPropiedadesViews.PROPIEDAD, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadPropiedadViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var propiedadTipoEspecificaciones = EntidadesPropiedadesMapper.MapearEntidadPropiedadViewModelAPropiedadTipoEspecificacionesEntidad(modelo);
                    _entidadesPropiedadesNegocio.Agregar(modelo.EntidadId, modelo.Nombre, modelo.Etiqueta, modelo.PropiedadTipoId.Value, propiedadTipoEspecificaciones, modelo.Orden.Value, modelo.PermiteNull.Value, modelo.GeneradaAlCrear.Value, modelo.Editable.Value, modelo.CalculadaFormula, UsuarioId);

                    return RedirectToAction(nameof(Index), new { modelo.EntidadId });
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadPropiedadViewModel(modelo, PaginaModo.Agregar);
            return View(EntidadesPropiedadesViews.PROPIEDAD, modelo);
        }

        public ActionResult Editar(Guid id, Guid entidadId)
        {
            var propiedad = _entidadesPropiedadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            if (propiedad == null)
            {
                return RedirectToAction(nameof(Index), new { entidadId });
            }

            var modelo = EntidadesPropiedadesMapper.MapearEntidadAEntidadPropiedadViewModel(propiedad);

            CargarEntidadPropiedadViewModel(modelo, PaginaModo.Editar);
            return View(EntidadesPropiedadesViews.PROPIEDAD, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadPropiedadViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesPropiedadesNegocio.Actualizar(
                        propiedad: EntidadesPropiedadesMapper.MapearEntidadPropiedadViewModelAEntidad(modelo), 
                        propiedadTipoEspecificaciones: EntidadesPropiedadesMapper.MapearEntidadPropiedadViewModelAPropiedadTipoEspecificacionesEntidad(modelo), 
                        usuarioId: UsuarioId);

                    ControllerHelper.CargarMensajeResultadoOk(EntidadPropiedadMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadPropiedadViewModel(modelo, PaginaModo.Editar);
            return View(EntidadesPropiedadesViews.PROPIEDAD, modelo);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadesPropiedadesViewModel(EntidadesPropiedadesViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            var entidad = _entidadesRepositorio.Obtener(modelo.EntidadId, cargarDatosAdicionales: true);
            modelo.EntidadNombre = entidad.Nombre;
            modelo.AplicacionVersionId = entidad.AplicacionVersionId;

            modelo.Items = EntidadesPropiedadesMapper.MapearEntidadesAModelos(
                entidades: _entidadesPropiedadesRepositorio.ObtenerLista(
                    entidad.Id,
                    busqueda: modelo.Busqueda,
                    cargarDatosAdicionales: true),
                claves: entidad.Claves);

            modelo.OrdenarItems();
        }

        private void CargarEntidadPropiedadViewModel(EntidadPropiedadViewModel modelo, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.PaginaModo = paginaModo;

            if (string.IsNullOrWhiteSpace(modelo.EntidadNombre))
            {
                modelo.EntidadNombre = _entidadesRepositorio.Obtener(modelo.EntidadId).Nombre;
            }

            modelo.GeneradaAlCrear = modelo.GeneradaAlCrear ?? false;
            modelo.Editable = modelo.Editable ?? true;

            modelo.TiposSelectList = ListasHelper.ObtenerPropiedadTiposSelectList(_entidadesPropiedadesRepositorio.ObtenerTipos());
            modelo.SiNoSelectList = namasdev.Web.Helpers.ListasHelper.ObtenerSiNoSelectList();
        }

        #endregion Metodos
    }
}