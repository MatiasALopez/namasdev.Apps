using System;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Helpers;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.ViewModels.AplicacionesVersiones;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class AplicacionesVersionesController : ControllerBase
    {
        private const string APLICACION_VERSION_VIEW_NAME = "AplicacionVersion";

        private IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;
        private IAplicacionesVersionesNegocio _aplicacionesVersionesNegocio;

        public AplicacionesVersionesController(IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio, IAplicacionesVersionesNegocio aplicacionesVersionesNegocio)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesNegocio, nameof(aplicacionesVersionesNegocio));

            _aplicacionesVersionesRepositorio = aplicacionesVersionesRepositorio;
            _aplicacionesVersionesNegocio = aplicacionesVersionesNegocio;
        }

        #region Acciones

        public ActionResult Index(Guid aplicacionId,
            string busqueda = null,
            string orden = null,
            int pagina = 1)
        {
            var modelo = new AplicacionesVersionesViewModel
            {
                AplicacionId = aplicacionId,
                Busqueda = busqueda,
                Pagina = pagina,
                Orden = orden,
            };

            var op = modelo.CrearOrdenYPaginacionParametros();

            modelo.Items = AplicacionesVersionesMapper.MapearEntidadesAModelos(
                entidades: _aplicacionesVersionesRepositorio.ObtenerLista(
                    busqueda: modelo.Busqueda,
                    op: op));

            modelo.CargarPaginacion(op);

            CargarAplicacionesVersionesViewModel(modelo);
            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var aplicacion = _aplicacionesVersionesRepositorio.Obtener(id);
            if (aplicacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(AplicacionVersionMetadata.NOMBRE, id) });
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
            var modelo = new AplicacionVersionViewModel 
            {
                AplicacionId = aplicacionId
            };
            CargarAplicacionVersionViewModel(modelo, PaginaModo.Agregar);

            return View(APLICACION_VERSION_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(AplicacionVersionViewModel modelo)
        {
            if (!modelo.AplicacionId.HasValue)
            {
                return RedirectToAction("Index", "Aplicaciones");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _aplicacionesVersionesNegocio.Agregar(modelo.AplicacionId.Value, modelo.Nombre, UsuarioId);

                    return RedirectToAction(nameof(Index), new { aplicacionId = modelo.AplicacionId });
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarAplicacionVersionViewModel(modelo, PaginaModo.Agregar);
            return View(APLICACION_VERSION_VIEW_NAME, modelo);
        }

        public ActionResult Editar(Guid id)
        {
            var aplicacionVersion = _aplicacionesVersionesRepositorio.Obtener(id);
            if (aplicacionVersion == null)
            {
                return RedirectToAction("Index", "Aplicaciones");
            }

            var modelo = AplicacionesVersionesMapper.MapearEntidadAAplicacionViewModel(aplicacionVersion);
            CargarAplicacionVersionViewModel(modelo, PaginaModo.Editar);

            return View(APLICACION_VERSION_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(AplicacionVersionViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidad = AplicacionesVersionesMapper.MapearAplicacionVersionViewModelAEntidad(modelo);
                    _aplicacionesVersionesNegocio.Actualizar(entidad, UsuarioId);

                    ControllerHelper.CargarMensajeResultadoOk("Versión actualizada correctamente.");
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarAplicacionVersionViewModel(modelo, PaginaModo.Editar);
            return View(APLICACION_VERSION_VIEW_NAME, modelo);
        }

        #endregion Acciones

        #region Metodos

        private void CargarAplicacionesVersionesViewModel(AplicacionesVersionesViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.AplicacionNombre = _aplicacionesVersionesRepositorio.Obtener(modelo.AplicacionId)?.Nombre;
        }

        private void CargarAplicacionVersionViewModel(AplicacionVersionViewModel modelo, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.PaginaModo = paginaModo;

            modelo.AplicacionNombre = _aplicacionesVersionesRepositorio.Obtener(modelo.AplicacionId.Value)?.Nombre;
        }

        #endregion Metodos
    }
}