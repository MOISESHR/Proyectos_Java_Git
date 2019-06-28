using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Web.Utilitarios;

namespace TGS.SGV.Web.Areas.Programacion.Controllers
{
    public class ReporteCuadroVacacionalController : Controller
    {

        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}