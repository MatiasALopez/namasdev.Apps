using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.Validation;
using namasdev.Web.Models;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio;
using namasdev.Apps.Negocio.DTO.Entidades;
using namasdev.Apps.Web.Portal.ViewModels.Entidades;
using namasdev.Apps.Web.Portal.Helpers;
using namasdev.Apps.Web.Portal.Metadata.Views;
using namasdev.Apps.Web.Portal.Models.Entidades;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesController : ControllerBase
    {
        public const string NAME = "Entidades";

        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesNegocio _entidadesNegocio;
        private readonly IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;

        public EntidadesController(
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IEntidadesRepositorio entidadesRepositorio, 
            IEntidadesNegocio entidadesNegocio, 
            IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio,
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesNegocio, nameof(entidadesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));

            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
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

            if (modelo.AplicacionesVersionesSelectList == null
                || !modelo.AplicacionesVersionesSelectList.Any())
            {
                return RedirectToAction(nameof(VersionesController.Index), VersionesController.NAME, new { modelo.AplicacionId });
            }

            return View(modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(Guid id)
        {
            var entidad = _entidadesRepositorio.Obtener(id);
            if (entidad == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesNegocio.MarcarComoBorrado(
                    new MarcarComoBorradoParametros 
                    { 
                        Id = id, 
                        UsuarioLogueadoId = UsuarioId 
                    });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
        }

        public ActionResult Agregar(Guid aplicacionVersionId)
        {
            var modelo = new EntidadViewModel 
            {
                AplicacionVersionId = aplicacionVersionId,
                PropiedadesDefaultIDPropiedadTipoId = PropiedadTipos.GUID,
            };
            
            CargarEntidadViewModel(modelo, PaginaModo.Agregar);
            return View(EntidadesViews.ENTIDAD, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(EntidadViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidad = _entidadesNegocio.Agregar(Mapear<AgregarParametros>(modelo));
                    return RedirectToAction(nameof(EntidadesPropiedadesController.Index), EntidadesPropiedadesController.NAME, new { entidadId = entidad.Id });
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadViewModel(modelo, PaginaModo.Agregar);
            return View(EntidadesViews.ENTIDAD, modelo);
        }

        public ActionResult Editar(Guid id, Guid? aplicacionVersionId = null)
        {
            var entidad = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            if (entidad == null)
            {
                return RedirectToAction(nameof(Index), new { aplicacionVersionId });
            }

            var modelo = Mapear<EntidadViewModel>(entidad);
            CargarEntidadViewModel(modelo, PaginaModo.Editar);

            return View(EntidadesViews.ENTIDAD, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entidadesNegocio.Actualizar(Mapear<ActualizarParametros>(modelo));

                    ControllerHelper.CargarMensajeResultadoOk(EntidadMetadata.Mensajes.EDITAR_OK);
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadViewModel(modelo, PaginaModo.Editar);
            return View(EntidadesViews.ENTIDAD, modelo);
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

            modelo.Items = Mapear<List<EntidadItemModel>>(_entidadesRepositorio.ObtenerLista(
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

                modelo.PropiedadTiposSelectList = ListasHelper.ObtenerPropiedadTiposSelectList(_entidadesPropiedadesRepositorio.ObtenerTipos());
            }
        }

        #endregion Metodos
    }
}