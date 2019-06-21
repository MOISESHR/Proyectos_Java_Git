using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using TGS.SGV.Strings;
using TGS.SGV.Comun;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsSolicitudServicio;
using TGS.SGV.Web.WsParametroServicio;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Web.Areas.Solicitud.Controllers
{ 
    public class RegistrarSolicitudController : BaseController
    {

        private readonly ISolicitudServicio _ISolicitudServicio;
        private readonly IParametroServicio _IParametroServicio;
        public RegistrarSolicitudController(ISolicitudServicio ISolicitudServicio,
            IParametroServicio IParametroServicio
            )
        {
            this._ISolicitudServicio = ISolicitudServicio;
            this._IParametroServicio = IParametroServicio;
        }

        [CustomAuthorize]
        public ActionResult ModalRegistrarSolicitud()
        {
            return PartialView("_RegistrarSolicitud");
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult FuncionRestarFechas(string FechaInicio, string FechaFinal)
        {
            var respfnfecha = Comun.Funciones.Funciones.RestarFechas(Convert.ToDateTime(FechaInicio), Convert.ToDateTime(FechaFinal));
            return Json(respfnfecha, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult ObtieneIndicadores(string CodigoUsuario, string FechaInicio)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var indicadoresRequest = new IndicadorTrabajadorRequest()
            {
                CodigoUsuario= user.Login,
                Fecha_Inicio=FechaInicio,
            };
            var ResponseIndicadores = _IParametroServicio.ObtenerIndicadoresFuturo(indicadoresRequest.CodigoUsuario, indicadoresRequest.Fecha_Inicio);
            return Json(ResponseIndicadores, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult ObtenerGoceFuturo(string CodigoUsuario)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var ResponseGoceFuturo = _IParametroServicio.ObtenerGoceFuturo(user.Login);
            return Json(ResponseGoceFuturo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult RegistrarSolicitud(Modelo.Modelo.Solicitud solicitud)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;            
            var solicitudRequest = new Modelo.Modelo.Solicitud()
            {
                CodigoEmpleado = user.Login,
                FechaInicio = solicitud.FechaInicio,
                FechaFinal = solicitud.FechaFinal,
                CalculoDias = solicitud.CalculoDias,
                TipoPerfil= user.TipoPerfil,
            };
            var ResponseSolicitud = _ISolicitudServicio.RegistraSolicitudTrabajador(solicitudRequest);
            return Json(ResponseSolicitud, JsonRequestBehavior.AllowGet);
        }               
    }
}
