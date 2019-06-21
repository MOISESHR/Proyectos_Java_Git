using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TGS.SGV.Web.Utilitarios
{
    public class CustomAntiForgeryToken :   FilterAttribute, IAuthorizationFilter
    { 
        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string cookieToken = String.Empty;
            string formToken = String.Empty;
            string tokenValue = request.Headers["__RequestVerificationToken"];

            if (!String.IsNullOrEmpty(tokenValue))
            {
                string[] tokens = tokenValue.Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }

            AntiForgery.Validate(cookieToken, formToken);
        }
        public static string GetAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                ValidateRequestHeader(filterContext.HttpContext.Request);
            }
            catch (HttpAntiForgeryException ex)
            {
                throw new HttpAntiForgeryException("No se encontro el token. Error: " + ex.Message);
            }
        }

    }

}