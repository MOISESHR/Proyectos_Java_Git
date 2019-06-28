using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TGS.SGV.Web.Models;

namespace TGS.SGV.Web.Utilitarios
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string RolesConfigKey { get; set; }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }
                 
        public override void OnAuthorization(AuthorizationContext filterContext)
        {           
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {                
                var authorizedRoles = ConfigurationManager.AppSettings[RolesConfigKey];

                Roles = String.IsNullOrEmpty(Roles) ? authorizedRoles : Roles;
                
                if (!String.IsNullOrEmpty(Roles))
                { 
                    if (!CurrentUser.IsInRole(Roles))
                    {
                        filterContext.Result =   new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Principal", action = "PermisoDenegado", area = "" }));
                    }
                }
            }
            else 
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "Seguridad" }));
            }           
        }
    }
}