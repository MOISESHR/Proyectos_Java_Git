using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Strings;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsModuloSistemaServicio;
using TGS.SGV.Web.WsPerfilUsuarioServicio;
using TGS.SGV.Web.WsTrabajadorServicio;
using TGS.SGV.Web.WsUsuarioServicio;

namespace TGS.SGV.Web.Areas.Seguridad.Controllers
{
    public class LoginController : BaseController
    {
        public readonly IUsuarioServicio _IIUsuarioServicio;
        private readonly IPerfilUsuarioServicio _IPerfilUsuarioServicio;
        private readonly ITrabajadorServicio _ITrabajadorServicio;
        private readonly IModuloSistemaServicio _IModuloSistemaServicio;

        public LoginController(IUsuarioServicio usuarioServicio, 
            ITrabajadorServicio trabajadorServicio, 
            IPerfilUsuarioServicio perfilUsuarioServicio,
            IModuloSistemaServicio moduloSistemaServicio)
        {
            this._IIUsuarioServicio = usuarioServicio;
            this._ITrabajadorServicio = trabajadorServicio;
            this._IPerfilUsuarioServicio = perfilUsuarioServicio;
            this._IModuloSistemaServicio = moduloSistemaServicio;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryToken]
        public ActionResult Ingresar(UsuarioDto usuario)
        {
            var resultado = string.Empty;
            var tipoResultado = string.Empty;

            var usuarioLogin  = _IIUsuarioServicio.ValidarIngreso(usuario.Login, usuario.Password);

            tipoResultado = usuarioLogin.Tipo.ToString();

            if (usuarioLogin.Tipo.ToString().Equals(General.Invalido))
            {
                resultado = usuarioLogin.Errores.FirstOrDefault();                
            }
            else
            {
                var usuarioDato = RecuperarDatosUsuario(usuario.Login); 

                SessionFacade.CrearSesion(usuarioDato); 

                resultado = Url.Action("PerfilUsuario", "Login", new { area = "Seguridad" });
            }

            return Json(new { Respuesta = new {  Mensaje = resultado , Tipo= tipoResultado } });
        }

        [CustomAuthorize]
        public ActionResult PerfilUsuario()
        {
            var perfiles = ObtenerPerfiles(User.Login, User.TipoEmpleado, User.CodigoEmpresa);

            if (perfiles.Count.Equals(1))
            { 
                var paginas = ObtenerPagina(User.Login, User.CodigoEmpresa, User.TipoPerfil);

                if (paginas == null)
                {
                    return RedirectToAction("Index", "Principal", new { area = "" });
                }
                else
                {
                    return RedirectToAction(paginas[3], paginas[2], new { area = paginas[1] });
                }
            }

            ViewBag.Perfiles = perfiles.Select(i => new { Codigo = i.TipoPerfil, Descripcion = i.NombrePerfil }).OrderBy(x => x.Descripcion).ToList();

            return View();
        }

        [HttpPost]
        [CustomAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult IngresarSistema(PerfilDto perfil)
        {
            var usuarioDato = RecuperarDatosUsuario(User.Login);

            usuarioDato.PerfilDtoLista = usuarioDato.PerfilDtoLista
                .Where(x => x.TipoPerfil.Equals(perfil.TipoPerfil)).ToList();

            UsuarioRequest usuarioRequest = new UsuarioRequest
            {
                TipoPerfil  = perfil.TipoPerfil,
                TipoEmpleado = usuarioDato.TipoEmpleado,
                CodigoEmpresa = usuarioDato.CodigoEmpresa
            };

            usuarioDato.PerfilAdicional = _IPerfilUsuarioServicio.ObtenerPerfilAdicional(usuarioRequest);
            
            SessionFacade.EliminarSesion();

            SessionFacade.CrearSesion(usuarioDato);
             
            var paginas = ObtenerPagina(usuarioDato.Login, usuarioDato.CodigoEmpresa, perfil.TipoPerfil);

            var resultado = string.Empty;

            if (paginas == null)
            {
                resultado= Url.Action("Index", "Principal", new { area = "" });
            }
            else
            {
                resultado= Url.Action(paginas[3], paginas[2], new { area = paginas[1] });
            }

            return Json(new { Respuesta = new { Mensaje = resultado } });
        }
               

        public UsuarioDto RecuperarDatosUsuario(string usuario)
        {         
            var trabajador = _ITrabajadorServicio.ObtenerDatosTrabajador(usuario).TrabajadorDto;

            var perfiles = ObtenerPerfiles(trabajador.CodigoTrabajador, trabajador.TipoEmpleado, trabajador.CodigoEmpresa);

            string perfilAdicional = string.Empty;
            

            if (perfiles.Count.Equals(1))
            {
                UsuarioRequest usuarioRequest = new UsuarioRequest
                {
                    TipoPerfil = perfiles[0].TipoPerfil,
                    TipoEmpleado = trabajador.TipoEmpleado,
                    CodigoEmpresa = trabajador.CodigoEmpresa
                }; 

                perfilAdicional = _IPerfilUsuarioServicio.ObtenerPerfilAdicional(usuarioRequest);
            } 

            UsuarioDto usuarioDto = new UsuarioDto
            {    
                Nombres = trabajador.NombreTrabajador,              
                Login = trabajador.CodigoTrabajador,
                TipoEmpleado = trabajador.TipoEmpleado,
                CodigoEmpresa = trabajador.CodigoEmpresa,
                PerfilDtoLista = perfiles,
                PerfilAdicional = perfilAdicional,
                CantidadPerfil = perfiles.Count
            };

            return usuarioDto; 
        }

        private List<PerfilDto> ObtenerPerfiles(string cip, string tipoEmpleado, string codigoEmpresa) {

            UsuarioRequest usuario = new UsuarioRequest
            {
                Cip = cip,
                TipoEmpleado = tipoEmpleado,
                CodigoEmpresa = codigoEmpresa
            };

            return _IPerfilUsuarioServicio.ListarPerfilUsuario(usuario).PerfilDtoLista;
        }
        
        private string[] ObtenerPagina(string cip, string codigoEmpresa, string tipoPerfil)
        {
            UsuarioRequest usuario = new UsuarioRequest
            {
                Cip = cip,
                CodigoEmpresa = codigoEmpresa,
                TipoPerfil = tipoPerfil
            };

            var menu = _IModuloSistemaServicio.ListarModuloUsuario(usuario).ModuloSistemaDtoLista;
            
            var menuPadre = menu.Where(x=>x.CodigoPadre.Equals(ConstantesUI.ModuloPadre)).OrderBy(x => x.Orden).FirstOrDefault();

            var menuHijo = menu.Where(p => p.CodigoPadre.Equals(menuPadre.CodigoModulo)).Select(x=>x.RutaPagina).FirstOrDefault();
            
            if (!string.IsNullOrEmpty(menuHijo))
            {
                return menuHijo.Split('/');
            }
            else
            {
                return null;
            } 
        }    

        public ActionResult CambioClave()
        {
            return View();
        }

        public ActionResult OlvidoClave()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EnviarClave(UsuarioDto usuario)
        {
            var respuesta = _IIUsuarioServicio.EnvioClaveUsuario(usuario.Login);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CambiarClave(UsuarioDto usuario)
        {
           var respuesta =  _IIUsuarioServicio.CambiarCLave(usuario.Login, usuario.Password, usuario.NuevoPassword);

            return Json(respuesta);
        }

        public ActionResult CerrarSesion()
        {
            SessionFacade.EliminarSesion();

            return RedirectToAction("Index", "Login", new { area = "Seguridad" });
        }


    }
}