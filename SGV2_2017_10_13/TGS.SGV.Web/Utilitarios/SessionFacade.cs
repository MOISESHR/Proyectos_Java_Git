using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Web.Models;

namespace TGS.SGV.Web.Utilitarios
{
    public static class SessionFacade
    {                  
        public static void EliminarSesion()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.User = null;
            } 
        }

        public static void CrearSesion(UsuarioDto usuario)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.FirstName = usuario.Nombres;
            serializeModel.Login = usuario.Login;
            serializeModel.UserId = Convert.ToInt32(usuario.Login);
            serializeModel.TipoEmpleado = usuario.TipoEmpleado;
            serializeModel.Roles = usuario.PerfilDtoLista.Select(i => i.NombrePerfil).FirstOrDefault();
            serializeModel.TipoPerfil = usuario.PerfilDtoLista.Select(i => i.TipoPerfil).FirstOrDefault();
            serializeModel.Ambito = usuario.PerfilDtoLista.Select(i => i.Ambito).FirstOrDefault();
            serializeModel.CodigoEmpresa = usuario.CodigoEmpresa;
            serializeModel.PerfilAdicional = usuario.PerfilAdicional; 
            serializeModel.CantidadPerfil = usuario.CantidadPerfil;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, usuario.Login, DateTime.Now, DateTime.Now.AddMinutes(40), false, userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

    }
}