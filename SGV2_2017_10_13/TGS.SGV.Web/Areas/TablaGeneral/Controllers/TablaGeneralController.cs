using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Web.WsTablaGeneralServicio;
namespace TGS.SGV.Web.Areas.TablaGeneral.Controllers
{
    public class TablaGeneralController : Controller
    {
        private readonly ITablaGeneralServicio _ITablaGeneralServicio;
        public TablaGeneralController(ITablaGeneralServicio ITablaGeneralServicio)
        {
            this._ITablaGeneralServicio = ITablaGeneralServicio;
        }
        public JsonResult ListarEmpresas()
        {
            var response = _ITablaGeneralServicio.ListarTablaGeneral("025");
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
