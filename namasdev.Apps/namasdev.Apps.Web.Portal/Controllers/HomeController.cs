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
            return RedirectToAction(nameof(AplicacionesController.Index), AplicacionesController.NAME);
        }

        #endregion

        #region Metodos

        #endregion
    }
}