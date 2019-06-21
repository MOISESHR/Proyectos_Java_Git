using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Servicio.Error;

namespace TGS.SGV.Servicio.Implementacion
{
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/UsuarioServicio", Name = "UsuarioServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioNegocio _IUsuarioNegocio;

        public UsuarioServicio(IUsuarioNegocio usuarioNegocio)
        {
            _IUsuarioNegocio = usuarioNegocio;
        }
         
        public Resultado<string> ValidarIngreso(string usuarioLogin, string claveUsuario)
        {
            return _IUsuarioNegocio.ValidarIngreso(usuarioLogin, claveUsuario);
        }

        public Resultado<string> CambiarCLave(string idUsuario, string claveAnterior, string claveNueva)
        {
            return _IUsuarioNegocio.CambiarCLave(idUsuario, claveAnterior, claveNueva);
        }

        public Resultado<string> EnvioClaveUsuario(string nroDocumento)
        {
            return _IUsuarioNegocio.EnvioClaveUsuario(nroDocumento);
        }

        public void Dispose()
        {
            _IUsuarioNegocio.Dispose();
        }
    }
}
