using System;
using System.Linq;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.Helpers;

// TODO (ML): mensaje "operacion no valida" a namasdev.core?

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesPropiedadesController : ControllerBase
    {
        private const string PROPIEDAD_VIEW_NAME = "Propiedad";

        private IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;
        private IEntidadesPropiedadesNegocio _entidadesPropiedadesNegocio;
        private IEntidadesRepositorio _entidadesRepositorio;

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

                            ControllerHelper.CargarMensajeResultadoOk("Clave establecida correctamente.");
                            ModelState.Clear();

                            break;

                        default:
                            throw new Exception($"Operación no válida ({model.Operacion}).");
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
                return Json(new { success = false, message = "No se pudo eliminar la propiedad." });
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
            return View(PROPIEDAD_VIEW_NAME, modelo);
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
            return View(PROPIEDAD_VIEW_NAME, modelo);
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
            return View(PROPIEDAD_VIEW_NAME, modelo);
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

                    ControllerHelper.CargarMensajeResultadoOk("Propiedad actualizada correctamente.");
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadPropiedadViewModel(modelo, PaginaModo.Editar);
            return View(PROPIEDAD_VIEW_NAME, modelo);
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