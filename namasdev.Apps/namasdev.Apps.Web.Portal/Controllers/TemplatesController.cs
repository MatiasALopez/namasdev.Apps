using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Core.Validation;

namespace namasdev.Apps.Web.Portal.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly IEntidadesRepositorio _entidadesRepositorio;

        public TemplatesController(IEntidadesRepositorio entidadesRepositorio)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            
            _entidadesRepositorio = entidadesRepositorio;
        }

        #region Actions

        public ActionResult Entidad(Guid id)
        {
            var usuario = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            return View(usuario);
        }

        public ActionResult Tabla(Guid id)
        {
            var usuario = _entidadesRepositorio.Obtener(id, cargarDatosAdicionales: true);
            return View(usuario);
        }

        #endregion

        #region Metodos


        #endregion
    }
}