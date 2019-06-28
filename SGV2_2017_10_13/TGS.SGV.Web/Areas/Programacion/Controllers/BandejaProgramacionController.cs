using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Strings;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsGoceVacacionalServicio;
using TGS.SGV.Web.WsSolicitudServicio;
using TGS.SGV.Web.WsTrabajadorServicio;
using TGS.SGV.Web.WsParametroServicio;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Web.Areas.Programacion.Controllers
{
    public class BandejaProgramacionController : BaseController
    {        
        private readonly IGoceVacacionalServicio _IGoceVacacionalServicio;
        private readonly ISolicitudServicio _ISolicitudServicio;
        private readonly IParametroServicio _IParametroServicio;
        private readonly ITrabajadorServicio _ITrabajadorServicio;

        public BandejaProgramacionController(IGoceVacacionalServicio IGoceVacacionalServicio,
            ISolicitudServicio ISolicitudServicio, ITrabajadorServicio ITrabajadorServicio,
            IParametroServicio IParametroServicio
            )
        {
            this._IGoceVacacionalServicio = IGoceVacacionalServicio;
            this._ISolicitudServicio = ISolicitudServicio;
            this._ITrabajadorServicio = ITrabajadorServicio;
            this._IParametroServicio = IParametroServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObtenerDatosTrabajador()
        {
            var ResponseDatosTrabajador = _ITrabajadorServicio.ObtenerDatosTrabajador("000618888");
            return Json(ResponseDatosTrabajador, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerIndicadoresFuturo(string CodigoUsuario, string FechaInicio)
        {
            var indicadoresRequest = new IndicadorTrabajadorRequest()
            {
                CodigoUsuario = "000618888",
                Fecha_Inicio = FechaInicio,
            };
            var ResponseDatosIndicadores = _IParametroServicio.ObtenerIndicadoresFuturo(indicadoresRequest.CodigoUsuario, indicadoresRequest.Fecha_Inicio);
            return Json(ResponseDatosIndicadores, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFechaDerecho()
        {
            var FechaDerechoRequest = new FechaDerechoRequest()
            {
                CodigoUsuario = "000618888",
            };            
            var ResponseDatosFechaDerecho = _ISolicitudServicio.ObtenerFechaDerecho(FechaDerechoRequest);
            return Json(ResponseDatosFechaDerecho, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarObtenerGoce(GoceVacacionalRequest goceVacacional)
        {
            var goceVacacionalRequest = new GoceVacacionalRequest()
            {
                CodigoUsuario = "000618888",
                FechaDerecho = goceVacacional.FechaDerecho,
                FechaRol = goceVacacional.FechaRol,
                Periodo = 11,
                Reprogramado = 1,
                TipoEmpleado = "EMP",
                Indice = 0,
                Tamanio = 10,
            };
            var ResponseObtenerGoce = _IGoceVacacionalServicio.ListarObtenerGoce(goceVacacionalRequest);
            return Json(ResponseObtenerGoce, JsonRequestBehavior.AllowGet);
        }
    }
}

