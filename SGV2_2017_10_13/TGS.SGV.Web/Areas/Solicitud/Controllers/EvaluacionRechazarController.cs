using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Web.Utilitarios;

namespace TGS.SGV.Web.Areas.Solicitud.Controllers
{
    public class EvaluacionRechazarController : BaseController
    {
        [CustomAuthorize]
        public ActionResult ModalEvaluacionRechazar()
        {
            return PartialView("_RechazarEvaluacion");
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult RechazarEval(string solicitud)
        {
            var ResponseCombo = new List<string>();
            return Json(ResponseCombo, JsonRequestBehavior.AllowGet);
        }
    }
}
