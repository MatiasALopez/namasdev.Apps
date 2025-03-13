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
    public class EntidadesAsociacionesController : ControllerBase
    {
        private const string ASOCIACION_VIEW_NAME = "Asociacion";

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

        
        #endregion Acciones

        #region Metodos


        #endregion Metodos
    }
}