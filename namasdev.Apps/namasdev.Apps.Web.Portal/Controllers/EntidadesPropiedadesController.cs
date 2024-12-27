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
            string orden = null,
            int pagina = 1)
        {
            var modelo = new EntidadesPropiedadesViewModel
            {
                EntidadId = entidadId,
                Busqueda = busqueda,
                Pagina = pagina,
                Orden = orden,
            };

            CargarEntidadesPropiedadesViewModel(modelo);
            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var propiedad = _entidadesPropiedadesRepositorio.Obtener(id);
            if (propiedad == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadPropiedadMetadata.NOMBRE, id) });
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
                PropiedadTipoEspecificacionesTexto = new PropiedadTipoEspecificacionesTextoViewModel(),
                PropiedadTipoEspecificacionesEntero = new PropiedadTipoEspecificacionesEnteroViewModel(),
                PropiedadTipoEspecificacionesNumero = new PropiedadTipoEspecificacionesNumeroViewModel(),
            };
            
            CargarEntidadPropiedadViewModel(modelo, PaginaModo.Agregar);
            return View(PROPIEDAD_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadPropiedadViewModel modelo)
        {
            // TODO (ML) resolver binding de props de ViewModel dentro de ViewModel
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
                    var propiedad = EntidadesPropiedadesMapper.MapearEntidadPropiedadViewModelAEntidad(modelo);
                    _entidadesPropiedadesNegocio.Actualizar(propiedad, UsuarioId);

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

            var entidad = _entidadesRepositorio.Obtener(modelo.EntidadId);
            modelo.EntidadNombre = entidad.Nombre;

            var op = modelo.CrearOrdenYPaginacionParametros();

            modelo.Items = EntidadesPropiedadesMapper.MapearEntidadesAModelos(
                entidades: _entidadesPropiedadesRepositorio.ObtenerLista(
                    entidad.Id,
                    busqueda: modelo.Busqueda,
                    op: op,
                    cargarDatosAdicionales: true));

            modelo.CargarPaginacion(op);
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