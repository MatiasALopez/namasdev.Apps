using System;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.ViewModels.Versiones;
using namasdev.Apps.Web.Portal.Helpers;
using namasdev.Apps.Web.Portal.Metadata.Views;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class VersionesController : ControllerBase
    {
        public const string NAME = "Versiones";

        private readonly IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;
        private readonly IAplicacionesVersionesNegocio _aplicacionesVersionesNegocio;
        private readonly IAplicacionesRepositorio _aplicacionesRepositorio;

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
                return Json(new { success = false, message = AplicacionVersionMetadata.Mensajes.ELIMINAR_ERROR });
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

            return View(VersionesViews.VERSION, modelo);
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
                        aplicacionVersionIdBase: modelo.AplicacionVersionIdBase);

                    return RedirectToAction(nameof(Index), new { aplicacionId = modelo.AplicacionId });
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarVersionViewModel(modelo, PaginaModo.Agregar);
            return View(VersionesViews.VERSION, modelo);
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

            return View(VersionesViews.VERSION, modelo);
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

                    ControllerHelper.CargarMensajeResultadoOk(AplicacionVersionMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarVersionViewModel(modelo, PaginaModo.Editar);
            return View(VersionesViews.VERSION, modelo);
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