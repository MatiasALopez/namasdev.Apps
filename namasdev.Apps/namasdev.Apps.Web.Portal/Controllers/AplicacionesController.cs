using System;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Helpers;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.ViewModels.Aplicaciones;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class AplicacionesController : ControllerBase
    {
        private const string APLICACION_VIEW_NAME = "Aplicacion";

        private IAplicacionesRepositorio _aplicacionesRepositorio;
        private IAplicacionesNegocio _aplicacionesNegocio;

        public AplicacionesController(IAplicacionesRepositorio aplicacionesRepositorio, IAplicacionesNegocio aplicacionesNegocio)
        {
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesRepositorio, nameof(aplicacionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesNegocio, nameof(aplicacionesNegocio));

            _aplicacionesRepositorio = aplicacionesRepositorio;
            _aplicacionesNegocio = aplicacionesNegocio;
        }

        #region Acciones

        public ActionResult Index(
            string busqueda = null,
            string orden = null,
            int pagina = 1)
        {
            var modelo = new AplicacionesViewModel
            {
                Busqueda = busqueda,
                Pagina = pagina,
                Orden = orden,
            };

            CargarAplicacionesViewModel(modelo);
            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var aplicacion = _aplicacionesRepositorio.Obtener(id);
            if (aplicacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente("Aplicación", id) });
            }

            try
            {
                _aplicacionesNegocio.MarcarComoBorrado(id, UsuarioId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "No se pudo eliminar la aplicación." });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar()
        {
            var modelo = new AplicacionViewModel();
            CargarAplicacionViewModel(modelo, PaginaModo.Agregar);

            return View(APLICACION_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(AplicacionViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _aplicacionesNegocio.Agregar(modelo.Nombre, UsuarioId);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarAplicacionViewModel(modelo, PaginaModo.Agregar);
            return View(APLICACION_VIEW_NAME, modelo);
        }

        public ActionResult Editar(Guid id)
        {
            var aplicacion = _aplicacionesRepositorio.Obtener(id);
            if (aplicacion == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var modelo = AplicacionesMapper.MapearEntidadAAplicacionViewModel(aplicacion);
            CargarAplicacionViewModel(modelo, PaginaModo.Editar);

            return View(APLICACION_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(AplicacionViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidad = AplicacionesMapper.MapearAplicacionViewModelAEntidad(modelo);
                    _aplicacionesNegocio.Actualizar(entidad, UsuarioId);

                    ControllerHelper.CargarMensajeResultadoOk("Aplicación actualizada correctamente.");
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarAplicacionViewModel(modelo, PaginaModo.Editar);
            return View(APLICACION_VIEW_NAME, modelo);
        }

        #endregion Acciones

        #region Metodos

        private void CargarAplicacionesViewModel(AplicacionesViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            var op = modelo.CrearOrdenYPaginacionParametros();

            modelo.Items = AplicacionesMapper.MapearEntidadesAModelos(
                entidades: _aplicacionesRepositorio.ObtenerLista(
                    busqueda: modelo.Busqueda,
                    op: op));

            modelo.CargarPaginacion(op);
        }

        private void CargarAplicacionViewModel(AplicacionViewModel modelo, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.PaginaModo = paginaModo;
        }

        #endregion Metodos
    }
}