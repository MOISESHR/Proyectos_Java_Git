using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Security.Principal; 

namespace TGS.SGV.Web.Models 
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string TipoPerfil { get; set; } 
        public string Roles { get; set; }    
        public string Login { get; set; } 
        public string CodigoEmpresa { get; set; }
        public string TipoEmpleado { get; set; }
        public string Ambito { get; set; }
        public string PerfilAdicional { get; set; } 
        public int? CantidadPerfil { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string TipoPerfil { get; set; }
        public string PerfilAdicional { get; set; }
        public string Roles { get; set; } 
        public string Login { get; set; } 
        public string CodigoEmpresa { get; set; }
        public string TipoEmpleado { get; set; }
        public string Ambito { get; set; } 
        public int? CantidadPerfil { get; set; }
    }
}