using System;
using System.Linq;
using System.Web.Mvc;

using namasdev.Apps.Datos;
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

        public ActionResult Entidad(Guid id)
        {
            var entidad = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            entidad.Propiedades = entidad.Propiedades.OrderBy(p => p.Orden).ToList();
            return View(entidad);
        }

        public ActionResult Tabla(Guid id)
        {
            var entidad = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            entidad.Propiedades = entidad.Propiedades.OrderBy(p => p.Orden).ToList();
            return View(entidad);
        }

        #endregion

        #region Metodos


        #endregion
    }
}