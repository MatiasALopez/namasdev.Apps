using System.Web.Mvc;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController()
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