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
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Web.Areas.Solicitud.Controllers
{
    public class RegistrarController : Controller
    {
        private readonly ISolicitudServicio _ISolicitudServicio;
        public RegistrarController(ISolicitudServicio ISolicitudServicio)
        {
            this._ISolicitudServicio = ISolicitudServicio;
        }

        public ActionResult Index(int id)
        {                        
            if (id == 0)
            {
                ViewBag.vbDatoSolicitud = "";
            }
            else
            {
                var CodigoSolicitud = string.Format("{0:0000000000}", id);
                var DatoSolicitud = _ISolicitudServicio.ObtenerSolicitudPorCodigo(CodigoSolicitud);
                ViewBag.vbDatoSolicitud = DatoSolicitud;
            }
            return View();
        }

        [HttpPost]
        public JsonResult FuncionRestarFechas(string FechaInicio, string FechaFinal)
        {
            var respfnfecha = Comun.Funciones.Funciones.RestarFechas(Convert.ToDateTime(FechaInicio), Convert.ToDateTime(FechaFinal));
            return Json(respfnfecha, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult ObtieneIndicadores(string CodigoUsuario, string FechaInicio)
        //{
        //    var ResponseIndicadores = _ISolicitudServicio.ObtenerIndicadoresFuturo(CodigoUsuario,FechaInicio);
        //    return Json(ResponseIndicadores, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ObtenerGoceFuturo(string CodigoUsuario)
        //{
        //    var ResponseGoceFuturo = _ISolicitudServicio.ObtenerGoceFuturo(CodigoUsuario);
        //    return Json(ResponseGoceFuturo, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult RegistrarSolicitud(Modelo.Modelo.Solicitud solicitud)
        {
            var ResponseSolicitud = _ISolicitudServicio.RegistraSolicitudTrabajador(solicitud);           
            return Json(ResponseSolicitud, JsonRequestBehavior.AllowGet);
        }

    }
}
