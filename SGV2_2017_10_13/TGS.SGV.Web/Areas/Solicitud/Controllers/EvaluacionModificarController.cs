using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Web.WsSolicitudServicio;
using TGS.SGV.Web.WsGoceVacacionalServicio;
using TGS.SGV.Web.WsParametroServicio;

namespace TGS.SGV.Web.Areas.Solicitud.Controllers
{
    public class EvaluacionModificarController : BaseController
    {
        private readonly ISolicitudServicio _ISolicitudServicio;
        private readonly IGoceVacacionalServicio _IGoceVacacionalServicio;
        private readonly IParametroServicio _IParametroServicio;
        public EvaluacionModificarController(ISolicitudServicio ISolicitudServicio,
            IGoceVacacionalServicio IGoceVacacionalServicio,
            IParametroServicio IParametroServicio
            )
        {
            this._ISolicitudServicio = ISolicitudServicio;
            this._IGoceVacacionalServicio = IGoceVacacionalServicio;
            this._IParametroServicio = IParametroServicio;
        }

        [CustomAuthorize]
        public ActionResult ModalEvaluacionModificar(EvaluacionSolicitudRequest evaluacion)
        {
            //Obtiene parametro de fecha fin para pintar en el calendario
            var objFechaGoce = _IParametroServicio.ObtenerGoceFuturo(evaluacion.CipEmpleado);
            var strFecFinCalendar = objFechaGoce.FechaGoceDto.FechaFinalCalendario;
            var strSigFechaDerecho = objFechaGoce.FechaGoceDto.FechadeDerecho;

            var Respuesta = _IParametroServicio.ObtenerIndicadoresFuturo(evaluacion.CipEmpleado, evaluacion.FechaInicio);

            var sss1 = Respuesta.IndicadorTrabajadorDto.Pendientes;
            var sss2 = Respuesta.IndicadorTrabajadorDto.Truncos;
            var sss3 = Respuesta.IndicadorTrabajadorDto.Vencidos;
            var sss4 = Respuesta.IndicadorTrabajadorDto.DiasPendienteAprobacion;
            var sss5 = Respuesta.IndicadorTrabajadorDto.DiasPendienteGoce;
            var sss6 = Respuesta.IndicadorTrabajadorDto.GoceFuturo;
            var sss7 = Respuesta.IndicadorTrabajadorDto.FechaFinalFuturo;

            ViewBag.vbFechaGoceDto = objFechaGoce.FechaGoceDto;
            ViewBag.vbDatosModifica = Respuesta.IndicadorTrabajadorDto;

            var ddd1 = evaluacion.FechaSolicitudInicio;
            var ddd2 = evaluacion.FechaSolicitudFin;
            var ddd3 = evaluacion.Dias;
            var ddd4 = evaluacion.Adelanto;
            var ddd5 = evaluacion.TipoSolicitud;

            ViewBag.vbDatosEvaluacion = evaluacion;

            ViewBag.vbCipEmpleado = evaluacion.CipEmpleado;

            return PartialView("_ModificarEvaluacion", (string) @ViewBag.vbCipEmpleado);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult ModificarEval(string solicitud)
        {
            var ResponseCombo = new List<string>();
            return Json(ResponseCombo, JsonRequestBehavior.AllowGet);
        }
    }
}
