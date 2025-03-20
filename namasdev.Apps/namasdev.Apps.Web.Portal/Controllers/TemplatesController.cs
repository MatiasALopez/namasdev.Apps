using System;
using System.Linq;
using System.Web.Mvc;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Core.Validation;

namespace namasdev.Apps.Web.Portal.Controllers
{
    public class TemplatesController : Controller
    {
        public const string NAME = "Templates";

        private readonly IEntidadesRepositorio _entidadesRepositorio;

        public TemplatesController(IEntidadesRepositorio entidadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            
            _entidadesRepositorio = entidadesRepositorio;
        }

        #region Actions

        public ActionResult Tabla(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        public ActionResult Entidad(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        public ActionResult EntidadMetadata(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        public ActionResult SqlConfig(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        public ActionResult Repositorio(Guid id)
        {
            var entidad = ObtenerEntidad(id, ordenarPropiedades: false);
            return View(entidad);
        }

        public ActionResult Negocio(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        public ActionResult Mapper(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        public ActionResult Controller(Guid id)
        {
            var entidad = ObtenerEntidad(id);
            return View(entidad);
        }

        #endregion

        #region Metodos

        private Entidad ObtenerEntidad(Guid id, bool ordenarPropiedades = true)
        {
            var entidad = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            if (ordenarPropiedades)
            {
                entidad.Propiedades = entidad.Propiedades.OrderBy(p => p.Orden).ToList();
            }
            return entidad;
        }

        #endregion
    }
}