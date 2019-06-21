using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Web.WsTrabajadorServicio;

namespace TGS.SGV.Web.Areas.Trabajador.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly ITrabajadorServicio _ITrabajadorServicio;

        public TrabajadorController(ITrabajadorServicio ITrabajadorServicio)
        {
            this._ITrabajadorServicio = ITrabajadorServicio;
        }

        //[HttpPost]
        //public JsonResult ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest)
        //{ 
        //    var respuesta = _IUnidadServicio.ListarCCRPerfil(unidadDtoRequest);

        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}

    }
}