using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio;
using namasdev.Apps.Negocio.DTO.EntidadesEspecificaciones;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesEspecificaciones;
using namasdev.Apps.Web.Portal.Helpers;
using namasdev.Apps.Web.Portal.Metadata.Views;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesEspecificacionesController : ControllerBase
    {
        public const string NAME = "EntidadesEspecificaciones";

        private readonly IEntidadesEspecificacionesRepositorio _entidadesEspecificacionesRepositorio;
        private readonly IEntidadesEspecificacionesNegocio _entidadesEspecificacionesNegocio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;
        private readonly IAplicacionesVersionesRepositorio _aplicacionesVersionesRepositorio;

        public EntidadesEspecificacionesController(
            IEntidadesEspecificacionesRepositorio entidadesEspecificacionesRepositorio,
            IEntidadesEspecificacionesNegocio entidadesEspecificacionesNegocio,
            IEntidadesRepositorio entidadesRepositorio,
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IAplicacionesVersionesRepositorio aplicacionesVersionesRepositorio,
            IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesEspecificacionesRepositorio, nameof(entidadesEspecificacionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesEspecificacionesNegocio, nameof(entidadesEspecificacionesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(aplicacionesVersionesRepositorio, nameof(aplicacionesVersionesRepositorio));

            _entidadesEspecificacionesRepositorio = entidadesEspecificacionesRepositorio;
            _entidadesEspecificacionesNegocio = entidadesEspecificacionesNegocio;
            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
            _aplicacionesVersionesRepositorio = aplicacionesVersionesRepositorio;
        }

        #region Acciones

        public ActionResult Editar(Guid id, Guid? aplicacionVersionId = null)
        {
            var entidad = _entidadesEspecificacionesRepositorio.Obtener(id, cargarEntidadConPropiedadesYClavesYIndices: true);
            if (entidad == null)
            {
                return RedirectToAction(nameof(EntidadesController.Index), EntidadesController.NAME, new { aplicacionVersionId });
            }

            var modelo = Mapear<EntidadEspecificacionesViewModel>(entidad);
            modelo.AplicacionVersionId = entidad.Entidad.AplicacionVersionId;
            modelo.EntidadNombre = entidad.Entidad.Nombre;
            CargarEntidadEspecificacionesViewModel(modelo);

            return View(EntidadesEspecificacionesViews.EntidadEspecificaciones, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(EntidadEspecificacionesViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parametros = Mapear<ActualizarParametros>(modelo);
                    if (!parametros.IDPropiedadTipoId.HasValue)
                    {
                        modelo.AuditoriaCreado = modelo.AuditoriaUltimaModificacion =
                        parametros.AuditoriaCreado = parametros.AuditoriaUltimaModificacion = 
                            false;
                    }
                    if (parametros.EsSoloLectura)
                    {
                        modelo.BajaTipoId = parametros.BajaTipoId = 
                            BajaTipos.NINGUNA;

                        modelo.AuditoriaCreado = modelo.AuditoriaUltimaModificacion =
                        parametros.AuditoriaCreado = parametros.AuditoriaUltimaModificacion = 
                            false;
                    }

                    _entidadesEspecificacionesNegocio.Actualizar(parametros);

                    ControllerHelper.CargarMensajeResultadoOk(EntidadEspecificacionesMetadata.Mensajes.EDITAR_OK);

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarEntidadEspecificacionesViewModel(modelo);
            return View(EntidadesEspecificacionesViews.EntidadEspecificaciones, modelo);
        }

        #endregion Acciones

        #region Metodos

        private void CargarEntidadEspecificacionesViewModel(EntidadEspecificacionesViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.ArticulosSelectList = ListasHelper.ObtenerArticulosSelectList(_entidadesRepositorio.ObtenerArticulos());
            modelo.BajaTiposSelectList = ListasHelper.ObtenerBajaTiposSelectList(_entidadesRepositorio.ObtenerBajaTipos());
            modelo.PropiedadTiposSelectList = ListasHelper.ObtenerPropiedadTiposSelectList(_entidadesPropiedadesRepositorio.ObtenerTipos());
        }

        #endregion Metodos
    }
}