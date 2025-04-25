using System;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.Validation;
using namasdev.Web.Helpers;
using namasdev.Web.Models;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.ViewModels.Aplicaciones;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class AplicacionesController : ControllerBase
    {
        public const string NAME = "Aplicaciones";

        private readonly IAplicacionesRepositorio _aplicacionesRepositorio;
        private readonly IAplicacionesNegocio _aplicacionesNegocio;

        public AplicacionesController(IAplicacionesRepositorio aplicacionesRepositorio, IAplicacionesNegocio aplicacionesNegocio, IMapper mapper)
            : base(mapper)
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
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(AplicacionMetadata.ETIQUETA, id) });
            }

            try
            {
                _aplicacionesNegocio.MarcarComoBorrado(id, UsuarioId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = AplicacionMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar()
        {
            var modelo = new AplicacionViewModel();
            CargarAplicacionViewModel(modelo, PaginaModo.Agregar);

            return View(AplicacionesViews.APLICACION, modelo);
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
            return View(AplicacionesViews.APLICACION, modelo);
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

            return View(AplicacionesViews.APLICACION, modelo);
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

                    ControllerHelper.CargarMensajeResultadoOk(AplicacionMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarAplicacionViewModel(modelo, PaginaModo.Editar);
            return View(AplicacionesViews.APLICACION, modelo);
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