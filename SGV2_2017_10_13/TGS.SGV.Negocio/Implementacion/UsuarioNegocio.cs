using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Agente.Contrato;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Strings;

namespace TGS.SGV.Negocio.Implementacion
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private readonly IUsuarioAgente _IUsuarioAgente;

        public UsuarioNegocio(IUsuarioAgente usuarioAgente)          
        {
            _IUsuarioAgente = usuarioAgente;
        } 

        public Resultado<string> ValidarIngreso(string usuarioLogin, string claveUsuario)
        {
            var validarUsuario= _IUsuarioAgente.ValidarIngreso(usuarioLogin, claveUsuario);

            if (validarUsuario)
            {
                return new Resultado<string>(TipoResultado.Ok, string.Empty);                
            }
            else
            {
                return new Resultado<string>(TipoResultado.Invalido, ReglasNegocio.DatosNoValidos);
            }
        }

        public Resultado<string> CambiarCLave(string idUsuario, string claveAnterior, string claveNueva)
        {
            var respuestaUsuario = _IUsuarioAgente.CambiarCLave(idUsuario, claveAnterior, claveNueva);

            if (respuestaUsuario.Equals(General.RespuestaOk))
            {
                return new Resultado<string>(TipoResultado.Ok, string.Empty);                
            }
            else
            {
                return new Resultado<string>(TipoResultado.Invalido, respuestaUsuario);
            }
        }

        public Resultado<string> EnvioClaveUsuario(string nroDocumento)
        {
            var respuestaUsuario = _IUsuarioAgente.EnvioClaveUsuario(nroDocumento);

            if (respuestaUsuario.Contains(ReglasNegocio.CuentaEnviada))
            {
                return new Resultado<string>(TipoResultado.Ok, string.Empty, respuestaUsuario);
            }
            else
            {
                return new Resultado<string>(TipoResultado.Invalido, respuestaUsuario);
            }
        }


        public void Dispose()
        {
            
        }
    }
}
