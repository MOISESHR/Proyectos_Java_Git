using System.Web.Mvc;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Text;
using System.Web; 
using System.Web.Security;
using TGS.SGV.Web.Models;

namespace TGS.SGV.Web.Utilitarios
{
    public abstract  class BaseController : Controller
    { 
        public Dictionary<string, object> ErrorModelState()
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    errors[key] = ModelState[key].Errors;

                    return errors;
                }
            }

            return errors;
        }
               
       
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
          
    }
}