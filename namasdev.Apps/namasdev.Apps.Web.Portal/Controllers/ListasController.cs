using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Web.Portal.Helpers;
using static namasdev.Apps.Entidades.Metadata.EntidadMetadata.Propiedades;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize]
    public class ListasController : ControllerBase
    {
        public const string NAME = "Listas";

        private readonly IEntidadesRepositorio _entidadesRepositorio;
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;

        public ListasController(IEntidadesRepositorio entidadesRepositorio, IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio, IMapper mapper)
            : base(mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesRepositorio, nameof(entidadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));

            _entidadesRepositorio = entidadesRepositorio;
            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
        }

        #region Actions

        public ActionResult Entidades(Guid aplicacionVersionId)
        {
            try
            {
                var items = ListasHelper.ObtenerEntidadesSelectListItems(_entidadesRepositorio.ObtenerLista(aplicacionVersionId));
                return Json(new { items }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return CrearJsonResultError(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EntidadesPropiedades(Guid entidadId)
        {
            try
            {
                var items = ListasHelper.ObtenerEntidadesPropiedadesSelectListItems(_entidadesPropiedadesRepositorio.ObtenerLista(entidadId));
                return Json(new { items }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return CrearJsonResultError(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Metodos

        #endregion
    }
}