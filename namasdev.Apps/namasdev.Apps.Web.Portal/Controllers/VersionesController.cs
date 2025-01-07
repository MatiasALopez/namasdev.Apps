using System;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.ViewModels.Versiones;
using namasdev.Apps.Web.Portal.Helpers;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class VersionesController : ControllerBase
    {
        private const string VERSION_VIEW_NAME = "Version";

        private IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;
        private IAplicacionesVersionesNegocio _aplicacionesVersionesNegocio;
        private IAplicacionesRepositorio _aplicacionesRepositorio;

        public VersionesController(
            IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio, 
            IAplicacionesVersionesNegocio aplicacionesVersionesNegocio,
            IAplicacionesRepositorio aplicacionesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesNegocio, nameof(aplicacionesVersionesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesRepositorio, nameof(aplicacionesRepositorio));

            _aplicacionesVersionesRepositorio = aplicacionesVersionesRepositorio;
            _aplicacionesVersionesNegocio = aplicacionesVersionesNegocio;
            _aplicacionesRepositorio = aplicacionesRepositorio;
        }

        #region Acciones

        public ActionResult Index(Guid aplicacionId,
            string busqueda = null,
            string orden = null,
            int pagina = 1)
        {
            var modelo = new VersionesViewModel
            {
                AplicacionId = aplicacionId,
                Busqueda = busqueda,
                Pagina = pagina,
                Orden = orden,
            };

            CargarVersionesViewModel(modelo);
            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(
            Guid id)
        {
            var aplicacion = _aplicacionesVersionesRepositorio.Obtener(id);
            if (aplicacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(AplicacionVersionMetadata.ETIQUETA, id) });
            }

            try
            {
                _aplicacionesVersionesNegocio.MarcarComoBorrado(id, UsuarioId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "No se pudo eliminar la versión." });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid aplicacionId)
        {
            var modelo = new VersionViewModel 
            {
                AplicacionId = aplicacionId
            };
            CargarVersionViewModel(modelo, PaginaModo.Agregar);

            return View(VERSION_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(VersionViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _aplicacionesVersionesNegocio.Agregar(modelo.AplicacionId, modelo.Nombre, UsuarioId, 
                        aplicacionVersionIdBase: modelo.AplicacionVersionIdBase.Value);

                    return RedirectToAction(nameof(Index), new { aplicacionId = modelo.AplicacionId });
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarVersionViewModel(modelo, PaginaModo.Agregar);
            return View(VERSION_VIEW_NAME, modelo);
        }

        public ActionResult Editar(Guid id, Guid aplicacionId)
        {
            var aplicacionVersion = _aplicacionesVersionesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            if (aplicacionVersion == null)
            {
                return RedirectToAction(nameof(Index), new { aplicacionId });
            }

            var modelo = VersionesMapper.MapearEntidadAAplicacionViewModel(aplicacionVersion);
            CargarVersionViewModel(modelo, PaginaModo.Editar);

            return View(VERSION_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(VersionViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidad = VersionesMapper.MapearAplicacionVersionViewModelAEntidad(modelo);
                    _aplicacionesVersionesNegocio.Actualizar(entidad, UsuarioId);

                    ControllerHelper.CargarMensajeResultadoOk("Versión actualizada correctamente.");
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarVersionViewModel(modelo, PaginaModo.Editar);
            return View(VERSION_VIEW_NAME, modelo);
        }

        #endregion Acciones

        #region Metodos

        private void CargarVersionesViewModel(VersionesViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            var aplicacion = _aplicacionesRepositorio.Obtener(modelo.AplicacionId);
            if (aplicacion == null)
            {
                return;
            }

            modelo.AplicacionNombre = aplicacion.Nombre;

            var op = modelo.CrearOrdenYPaginacionParametros();

            modelo.Items = VersionesMapper.MapearEntidadesAModelos(
                entidades: _aplicacionesVersionesRepositorio.ObtenerLista(
                    aplicacionId: aplicacion.Id,
                    busqueda: modelo.Busqueda,
                    op: op));

            modelo.CargarPaginacion(op);
        }

        private void CargarVersionViewModel(VersionViewModel modelo, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.PaginaModo = paginaModo;

            if (string.IsNullOrWhiteSpace(modelo.AplicacionNombre))
            {
                var aplicacion = _aplicacionesRepositorio.Obtener(modelo.AplicacionId);
                if (aplicacion != null)
                {
                    modelo.AplicacionNombre = aplicacion.Nombre;
                }
            }

            if (paginaModo == PaginaModo.Agregar)
            {
                modelo.VersionesSelectList = ListasHelper.ObtenerVersionesSelectList(_aplicacionesVersionesRepositorio.ObtenerLista(aplicacionId: modelo.AplicacionId));
            }
        }

        #endregion Metodos
    }
}