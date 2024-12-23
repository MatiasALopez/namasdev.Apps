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
using namasdev.Apps.Web.Portal.ViewModels.Entidades;
using namasdev.Apps.Web.Portal.Helpers;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesController : ControllerBase
    {
        private const string ENTIDAD_VIEW_NAME = "Entidad";

        private IEntidadesRepositorio _entidadesRepositorio;
        private IEntidadesNegocio _entidadesNegocio;
        private IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;

        public EntidadesController(
            IEntidadesRepositorio entidadesRepositorio, 
            IEntidadesNegocio entidadesNegocio, 
            IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesNegocio, nameof(entidadesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));

            _entidadesRepositorio = entidadesRepositorio;
            _entidadesNegocio = entidadesNegocio;
            _aplicacionesVersionesRepositorio = aplicacionesVersionesRepositorio;
        }

        #region Acciones

        public ActionResult Index(
            Guid? aplicacionId = null, Guid? aplicacionVersionId = null,
            string busqueda = null,
            string orden = null,
            int pagina = 1)
        {
            var modelo = new EntidadesViewModel
            {
                AplicacionId = aplicacionId ?? Guid.Empty,
                AplicacionVersionId = aplicacionVersionId,
                Busqueda = busqueda,
                Pagina = pagina,
                Orden = orden,
            };

            CargarEntidadesViewModel(modelo);
            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var entidad = _entidadesRepositorio.Obtener(id);
            if (entidad == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadMetadata.NOMBRE, id) });
            }

            try
            {
                _entidadesNegocio.MarcarComoBorrado(id, UsuarioId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "No se pudo eliminar la entidad." });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid aplicacionVersionId)
        {
            var modelo = new EntidadViewModel 
            {
                AplicacionVersionId = aplicacionVersionId
            };
            
            CargarEntidadViewModel(modelo, PaginaModo.Agregar);
            return View(ENTIDAD_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesNegocio.Agregar(modelo.AplicacionVersionId, modelo.Nombre, UsuarioId);

                    return RedirectToAction(nameof(Index), new { aplicacionId = modelo.AplicacionId, aplicacionVersionId = modelo.AplicacionVersionId });
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadViewModel(modelo, PaginaModo.Agregar);
            return View(ENTIDAD_VIEW_NAME, modelo);
        }

        public ActionResult Editar(Guid id, Guid aplicacionVersionId)
        {
            var entidad = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            if (entidad == null)
            {
                return RedirectToAction(nameof(Index), new { aplicacionVersionId });
            }

            var modelo = EntidadesMapper.MapearEntidadAEntidadViewModel(entidad);
            CargarEntidadViewModel(modelo, PaginaModo.Editar);

            return View(ENTIDAD_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidad = EntidadesMapper.MapearEntidadViewModelAEntidad(modelo);
                    _entidadesNegocio.Actualizar(entidad, UsuarioId);

                    ControllerHelper.CargarMensajeResultadoOk("Entidad actualizada correctamente.");
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadViewModel(modelo, PaginaModo.Editar);
            return View(ENTIDAD_VIEW_NAME, modelo);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadesViewModel(EntidadesViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            AplicacionVersion version = null;
            if (modelo.AplicacionVersionId.HasValue)
            {
                version = _aplicacionesVersionesRepositorio.Obtener(modelo.AplicacionVersionId.Value, cargarDatosAdicionales: true);

                modelo.AplicacionId = version.AplicacionId;
            }

            var versiones = _aplicacionesVersionesRepositorio.ObtenerLista(modelo.AplicacionId);
            if (!versiones.Any())
            {
                return;
            }

            if (!modelo.AplicacionVersionId.HasValue)
            {
                modelo.AplicacionVersionId = versiones.First().Id;

                version = _aplicacionesVersionesRepositorio.Obtener(modelo.AplicacionVersionId.Value, cargarDatosAdicionales: true);
            }
                
            modelo.AplicacionNombre = version.Aplicacion.Nombre;
            modelo.AplicacionVersionNombre = version.Nombre;

            modelo.AplicacionesVersionesSelectList = ListasHelper.ObtenerVersionesSelectList(
                versiones: versiones);

            var op = modelo.CrearOrdenYPaginacionParametros();

            modelo.Items = EntidadesMapper.MapearEntidadesAModelos(
                entidades: _entidadesRepositorio.ObtenerLista(
                    version.Id,
                    busqueda: modelo.Busqueda,
                    op: op));

            modelo.CargarPaginacion(op);
        }

        private void CargarEntidadViewModel(EntidadViewModel modelo, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.PaginaModo = paginaModo;

            var aplicacionVersion = _aplicacionesVersionesRepositorio.Obtener(modelo.AplicacionVersionId, cargarDatosAdicionales: true);
            if (aplicacionVersion != null)
            {
                modelo.AplicacionId = aplicacionVersion.AplicacionId;
                modelo.AplicacionNombre = aplicacionVersion.Aplicacion.Nombre;
                modelo.AplicacionVersionNombre = aplicacionVersion.Nombre;
            }
        }

        #endregion Metodos
    }
}