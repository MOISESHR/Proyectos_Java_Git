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
    public class BandejaController : Controller
    {
        private readonly ISolicitudServicio _ISolicitudServicio;
        private readonly ITablaGeneralServicio _ITablaGeneralServicio;
        private readonly ITrabajadorServicio _ITrabajadorServicio;
        public BandejaController(ISolicitudServicio ISolicitudServicio, ITablaGeneralServicio ITablaGeneralServicio, 
            ITrabajadorServicio ITrabajadorServicio)
        {
            this._ISolicitudServicio = ISolicitudServicio;
            this._ITablaGeneralServicio = ITablaGeneralServicio;
            this._ITrabajadorServicio = ITrabajadorServicio;
        }        
        public ActionResult Index()
        {
            var ListEstados = _ITablaGeneralServicio.ListarTablaGeneral(Constantes.Generales.CodigoTipoEstado);
            ViewBag.vbListEstados = ListEstados;
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var DatosTrabajador = _ITrabajadorServicio.ObtenerDatosTrabajador(user.Login);
            ViewBag.vbDatosTrabajador = DatosTrabajador;

            return View();
        }
        [HttpPost]
        public JsonResult ListarSolicitud(SolicitudRequest solicitud)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var solicitudRequest = new SolicitudRequest()
            {
                CodUsuario = user.Login,
                FechaInicio = solicitud.FechaInicio,
                FechaFinal = solicitud.FechaFinal,
                EstadoSolicitud = solicitud.EstadoSolicitud,
                Indice=solicitud.Indice,
                Tamanio=solicitud.Tamanio,
            };
            var ResponseSolicitud = _ISolicitudServicio.ListarSolicitud(solicitudRequest);
            return Json(ResponseSolicitud, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarSolicitud(string CodigoSolicitud, string CodigoUsuario)
        {
            var user = (TGS.SGV.Web.Models.CustomPrincipal)HttpContext.User;
            var Respuesta = _ISolicitudServicio.ActualizaSolicitudTrabajador(CodigoSolicitud, user.Login);
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
         
    }
}
