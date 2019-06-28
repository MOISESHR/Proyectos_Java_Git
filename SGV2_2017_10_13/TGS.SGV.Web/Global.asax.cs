using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TGS.SGV.Web.Models;

namespace TGS.SGV.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.Initialise();
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserId = serializeModel.UserId;
                newUser.FirstName = serializeModel.FirstName;
                newUser.Roles = serializeModel.Roles;
                newUser.TipoPerfil = serializeModel.TipoPerfil;
                newUser.Login = serializeModel.Login;
                newUser.TipoEmpleado = serializeModel.TipoEmpleado;
                newUser.Ambito = serializeModel.Ambito;
                newUser.CodigoEmpresa  = serializeModel.CodigoEmpresa;
                newUser.PerfilAdicional = serializeModel.PerfilAdicional; 
                newUser.CantidadPerfil = serializeModel.CantidadPerfil;
                HttpContext.Current.User = newUser;
            }
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
        }
    }
}
