using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Agente.Contrato;
using TGS.SGV.Agente.WsAutenticacion;

namespace TGS.SGV.Agente.Implementacion
{
    public class UsuarioAgente : IUsuarioAgente
    {
        public bool ValidarIngreso(string usuarioLogin, string claveUsuario)
        {
            using (var proxy = new ServicioAutenticacionClient())
            {
                return proxy.Ingresar(usuarioLogin, claveUsuario);
            }
        }

        public string CambiarCLave(string idUsuario, string claveAnterior, string claveNueva)
        {
            try
            {
                using (var proxy = new ServicioAutenticacionClient())
                {
                    proxy.CambiarContrasenia(idUsuario, claveAnterior, claveNueva);

                    return "Ok";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }

        public string EnvioClaveUsuario(string nroDocumento)
        { 
            try
            {
                using (var proxy = new ServicioAutenticacionClient())
                {
                    return proxy.OlvidarContrasenia(nroDocumento);                    
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }

    }
}
