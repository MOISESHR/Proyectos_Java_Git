using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.Negocio.Contrato
{
    public interface IUsuarioNegocio : IDisposable
    {
        Resultado<string> ValidarIngreso(string usuarioLogin, string claveUsuario);

        Resultado<string> CambiarCLave(string idUsuario, string claveAnterior, string claveNueva);

        Resultado<string> EnvioClaveUsuario(string nroDocumento);
    }
}
