using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Strings;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsEmpresaServicio;
using TGS.SGV.Web.WsSolicitudServicio;
using TGS.SGV.Web.WsTablaGeneralServicio;
using TGS.SGV.Web.WsTrabajadorServicio;
using TGS.SGV.Web.WsParametroServicio;
using TGS.SGV.Web.WsPerfilUsuarioServicio;
using TGS.SGV.Web.WsUnidadServicio;
namespace TGS.SGV.Web.Areas.Solicitud.Controllers
{
    public class BandejaEvaluacionController : BaseController
    {
        private readonly IEmpresaServicio _IEmpresaServicio;
        private readonly ISolicitudServicio _ISolicitudServicio;
        private readonly ITablaGeneralServicio _ITablaGeneralServicio;
        private readonly ITrabajadorServicio _ITrabajadorServicio;
        private readonly IParametroServicio _IParametroServicio;
        private readonly IPerfilUsuarioServicio _IPerfilUsuarioServicio;
        private readonly IUnidadServicio _IUnidadServicio;
        public BandejaEvaluacionController(ISolicitudServicio ISolicitudServicio,
            IEmpresaServicio IEmpresaServicio,
            ITablaGeneralServicio ITablaGeneralServicio,
            IParametroServicio IParametroServicio,
            IPerfilUsuarioServicio IPerfilUsuarioServicio,
            IUnidadServicio IUnidadServicio,
            ITrabajadorServicio ITrabajadorServicio)
        {
            this._IEmpresaServicio = IEmpresaServicio;
            this._ISolicitudServicio = ISolicitudServicio;
            this._ITablaGeneralServicio = ITablaGeneralServicio;
            this._ITrabajadorServicio = ITrabajadorServicio;
            this._IParametroServicio = IParametroServicio;
            this._IPerfilUsuarioServicio = IPerfilUsuarioServicio;
            this._IUnidadServicio = IUnidadServicio;
        }
        public ActionResult Index()
        {
            var codigoUsuario = User.Login;
            var tipoPerfil = User.TipoPerfil;

            var DatosTrabajador = _ITrabajadorServicio.ObtenerDatosTrabajador(codigoUsuario);
            ViewBag.vbDatosTrabajador = DatosTrabajador;
            var codigoTipoPago = DatosTrabajador.TrabajadorDto.CodigoTipoPago;

            var empresaRequest = new EmpresaRequest
            {
                CodigoEmpleado = User.Login,
                TipoPerfil = User.TipoPerfil,
                CodigoEmpresa = string.Empty
            };

            var ListarEmpresas = _IEmpresaServicio.ListarEmpresa(empresaRequest);
            ViewBag.vbListaEmpresas = ListarEmpresas;

            ParametroRequest oEmpFut = new ParametroRequest()
            {
                Codigo = Constantes.ParametrosInternos.GoceFuturo
            };
            var EmpFut = _IParametroServicio.ObtenerParametroInterno(oEmpFut).ParametroDto;
            ParametroRequest bePerfilFut = new ParametroRequest()
            {
                Codigo = Constantes.ParametrosInternos.PerfilGoceFuturo
            };
            var PerfilFut = _IParametroServicio.ObtenerParametroInterno(bePerfilFut).ParametroDto;
            var Valores = EmpFut.ValorParametro;
            var VerificaSiExiste = Valores.Contains(DatosTrabajador.TrabajadorDto.CodigoEmpresa);
            var strEstGoceFut = string.Empty;
            if (EmpFut.ValorParametro.Contains(DatosTrabajador.TrabajadorDto.CodigoEmpresa) && PerfilFut.ValorParametro.Contains(User.TipoPerfil))
            {
                strEstGoceFut = Constantes.Generales.ValorPositivo;
            }
            else
            {
                strEstGoceFut = Constantes.Generales.ValorNegativo;
            }

            ViewBag.vbEstGoceFut = strEstGoceFut;

            var diaCorte1 = string.Empty;
            var diaCorte2 = string.Empty;
            var fechaCorteEvaluacion = _IEmpresaServicio.ObtenerFechaCorteEvaluacion(codigoUsuario);
            
            ViewBag.vbDiaHoy = Funciones.FormatoFechaAnioMesDia(DateTime.Today);
            ViewBag.vbDiaCorte1 = fechaCorteEvaluacion.EmpresaDto.DiaCorte1;
            ViewBag.vbFechaCorte1 = fechaCorteEvaluacion.EmpresaDto.FechaCorte;
            ViewBag.vbSinFechaCorte = fechaCorteEvaluacion.EmpresaDto.SinFechaCorte;

            var ListaTipos = _ITablaGeneralServicio.ListarTablaGeneral(Constantes.Generales.CodigoTipos);

            ViewBag.vbListaEmpresas = ListarEmpresas;
            
            ViewBag.vbListaTipos = ListaTipos;

            var ListEstados = _ITablaGeneralServicio.ListarTablaGeneral(Constantes.Generales.CodigoTipoEstado);
            ViewBag.vbListEstados = ListEstados;

            return View();
        }

        [HttpPost]
        public JsonResult ListarEvaluaciones(EvaluacionSolicitudRequest evaluacion)
        {
            evaluacion.CodigoUsuario = User.Login;
            evaluacion.CodigoPerfil = User.TipoPerfil;

            if (evaluacion.CodigoTipo == Constantes.Generales.ValorCero)
            {
                evaluacion.CodigoTipo = string.Empty;
            } 

            var Respuesta = new EvaluacionSolicitudResponse();
            if (User.TipoPerfil == Constantes.TipoPerfil.AprobadorDirectivos)
            {
                Respuesta = _ISolicitudServicio.ListarEvaluacionSolicitudDPZ(evaluacion);
            }
            else
            {
                Respuesta = _ISolicitudServicio.ListarEvaluacionSolicitud(evaluacion);
            }            
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult AprobarEvaluacionFuturo(EvaluacionSolicitudRequest evaluacion)
        {
            var sCodigoEmpresa = evaluacion.CodigoEmpresa;
            var sTipoSel = evaluacion.TipoSolicitud;
            var sCadenaSeleccion = evaluacion.CodigoSolicitud;

            evaluacion.CodigoPerfil = User.TipoPerfil;
            evaluacion.CodigoUsuario = User.Login;

            var Respuesta = _ISolicitudServicio.ProcesoAprobarEvaluacionSolicitudFutura(evaluacion);
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
    }
    
}
