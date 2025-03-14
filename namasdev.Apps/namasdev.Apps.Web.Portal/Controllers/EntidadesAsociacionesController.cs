using System;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class EntidadesAsociacionesController : ControllerBase
    {
        public const string NAME = "EntidadesAsociaciones";

        private readonly IEntidadesAsociacionesRepositorio _entidadesAsociacionesRepositorio;
        private readonly IEntidadesAsociacionesNegocio _entidadesAsociacionesNegocio;
        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public EntidadesAsociacionesController(
            IEntidadesAsociacionesRepositorio entidadesAsociacionesRepositorio, 
            IEntidadesAsociacionesNegocio entidadesAsociacionesNegocio, 
            IEntidadesRepositorio entidadesRepositorio,
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesAsociacionesRepositorio, nameof(entidadesAsociacionesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesAsociacionesNegocio, nameof(entidadesAsociacionesNegocio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesAsociacionesRepositorio = entidadesAsociacionesRepositorio;
            _entidadesAsociacionesNegocio = entidadesAsociacionesNegocio;
            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
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
        public ActionResult Eliminar(Guid id)
        {
            var asociacion = _entidadesAsociacionesRepositorio.Obtener(id);
            if (asociacion == null)
            {
                return Json(new { success = false, message = Validador.MensajeEntidadInexistente(EntidadAsociacionMetadata.ETIQUETA, id) });
            }

            try
            {
                _entidadesAsociacionesNegocio.Eliminar(id, UsuarioId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = EntidadAsociacionMetadata.Mensajes.ELIMINAR_ERROR });
            }

            return Json(new { success = true });
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

        #endregion Metodos
    }
}