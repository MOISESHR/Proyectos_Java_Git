using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsModuloSistemaServicio;
using TGS.SGV.Web.WsPerfilUsuarioServicio;
using TGS.SGV.Web.WsUsuarioServicio;

namespace TGS.SGV.Web.Controllers
{
    public class PrincipalController : BaseController
    {
        private readonly IModuloSistemaServicio _IModuloSistemaServicio;
        private readonly IPerfilUsuarioServicio _IPerfilUsuarioServicio;

        public PrincipalController(IModuloSistemaServicio moduloSistemaServicio, IPerfilUsuarioServicio perfilUsuarioServicio)
        {
            this._IModuloSistemaServicio = moduloSistemaServicio;
            this._IPerfilUsuarioServicio = perfilUsuarioServicio;
        }

        [CustomAuthorize]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermisoDenegado()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Menu()
        {
            UsuarioRequest usuario = new UsuarioRequest {
                Cip= User.Login,
                CodigoEmpresa= User.CodigoEmpresa,
                TipoPerfil= User.TipoPerfil
            };

            List<ModuloSistemaDto> menu = new List<ModuloSistemaDto>();
             
            menu = _IModuloSistemaServicio.ListarModuloUsuario(usuario).ModuloSistemaDtoLista;

            return PartialView("_Menu", menu);            
        }
 

    }
}