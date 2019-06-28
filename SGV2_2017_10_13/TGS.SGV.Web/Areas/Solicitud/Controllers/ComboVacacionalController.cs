using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Strings;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsSolicitudServicio;
using TGS.SGV.Web.WsTablaGeneralServicio;
using TGS.SGV.Web.WsTrabajadorServicio;

namespace TGS.SGV.Web.Areas.Solicitud.Controllers
{
    public class ComboVacacionalController : BaseController
    {
        private readonly ISolicitudServicio _ISolicitudServicio;
        private readonly ITablaGeneralServicio _ITablaGeneralServicio;
        private readonly ITrabajadorServicio _ITrabajadorServicio;
        public ComboVacacionalController(ISolicitudServicio ISolicitudServicio, ITablaGeneralServicio ITablaGeneralServicio,
            ITrabajadorServicio ITrabajadorServicio)
        {
            this._ISolicitudServicio = ISolicitudServicio;
            this._ITablaGeneralServicio = ITablaGeneralServicio;
            this._ITrabajadorServicio = ITrabajadorServicio;
        }

        [CustomAuthorize]
        public ActionResult ModalComboVacacional()
        {
            return PartialView("_RegistroComboVacacional");
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult ListarComboFeriado(ComboFeriadoRequest solicitud)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var solicitud2 = new ComboFeriadoRequest()
            {
                CodigoUsuario = user.Login,
                FechaInicio = Convert.ToString(DateTime.Now.ToShortDateString()),
                TipoEmpresa = user.CodigoEmpresa,
            };
            var ResponseCombo = _ISolicitudServicio.ListarComboFeriado(solicitud2);
            return Json(ResponseCombo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult RegistrarCombo(Modelo.Modelo.Solicitud solicitud)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var respfnfecha = Funciones.RestarFechas(Convert.ToDateTime(solicitud.FechaInicio), Convert.ToDateTime(solicitud.FechaFinal));
            var solicitud2 = new Modelo.Modelo.Solicitud()
            {
                CodigoEmpleado = user.Login,
                FechaInicio = solicitud.FechaInicio,
                FechaFinal = solicitud.FechaFinal,
                CalculoDias = Convert.ToInt32(respfnfecha),
            };
            var ResponseSolicitud = _ISolicitudServicio.RegistraSolicitudTrabajador(solicitud2);
            return Json(ResponseSolicitud, JsonRequestBehavior.AllowGet);
        }
         
       
    }
}
