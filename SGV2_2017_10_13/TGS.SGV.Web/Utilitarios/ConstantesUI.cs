using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web; 
using TGS.SGV.Web.Models;

namespace TGS.SGV.Web.Utilitarios
{
    public static class ConstantesUI
    {
        private static readonly string ConfServerReport = Convert.ToString(ConfigurationManager.AppSettings["ReportServer"]);
        private static readonly string ConfModuloPadre = "00000";
       

        public static string ModuloPadre
        {
            get { return ConfModuloPadre; }
        }

        public static string ServerReport {
            get { return ConfServerReport; }
        }
         

        public static CustomPrincipal UsuarioLogin
        {
            get
            {
                return (HttpContext.Current.Request.IsAuthenticated) ? (CustomPrincipal)HttpContext.Current.User: null;
            }
        }
    }
}