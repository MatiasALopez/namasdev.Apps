using System.Web.Mvc;

using AutoMapper;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        public const string NAME = "Home";

        public HomeController(IMapper mapper)
            : base(mapper)
        {
        }

        #region Actions

        public ActionResult Index()
        {
            //if (UsuarioHelper.PerteneceAlRol(AspNetRoles.ADMINISTRADOR))
            //{
            //    return RedirectToAction("Ventas", "Reportes");
            //}
            return View();
        }

        #endregion

        #region Metodos

        #endregion
    }
}