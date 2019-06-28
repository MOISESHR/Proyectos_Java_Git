using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsUnidadServicio;

namespace TGS.SGV.Web.Areas.Unidad.Controllers
{
    public class UnidadController : BaseController
    {
        private readonly IUnidadServicio _IUnidadServicio;

        public UnidadController(IUnidadServicio IUnidadServicio)
        { 
            this._IUnidadServicio = IUnidadServicio;
        }

        [HttpPost]
        public JsonResult ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest)
        {
            unidadDtoRequest.Cip = User.Login;
            unidadDtoRequest.TipoPerfil = User.TipoPerfil; 

            var respuesta = _IUnidadServicio.ListarCCRPerfil(unidadDtoRequest);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
    }
}